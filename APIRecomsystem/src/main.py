from fastapi import FastAPI, File, UploadFile
from fastapi.responses import JSONResponse
from fastapi import Request
from pymongo import MongoClient
from dotenv import load_dotenv
import os
import pandas as pd
from io import BytesIO
from datetime import datetime
import re
import random
from math import floor
from datetime import datetime, timedelta

import shutil
import os
import pickle
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import linear_kernel
from sentence_transformers import SentenceTransformer, util
# from surprise import Dataset, Reader, SVD


# Load biến môi trường từ .env
load_dotenv()

app = FastAPI()

# Kết nối MongoDB từ .env
MONGO_URI = os.getenv("MONGO_URI")
DB_NAME = os.getenv("MONGO_DB_NAME")

client = MongoClient(MONGO_URI)
db = client[DB_NAME]
collection = db["Books"]

# Lấy đường dẫn tuyệt đối đến thư mục cha chứa main.py
BASE_DIR = os.path.dirname(os.path.abspath(__file__))

# Ghép với thư mục models nằm cùng cấp với src
MODEL_DIR = os.path.join(BASE_DIR, "../models")
# Đảm bảo thư mục tồn tại
os.makedirs(MODEL_DIR, exist_ok=True)

STATUSES = [0,1,2,3,4]


# Global variables
books_df = pd.DataFrame()
ratings_df = pd.DataFrame()

tfidf_matrix = None
tfidf_sim = None
bert_model = SentenceTransformer("all-MiniLM-L6-v2")
bert_embeddings = None


def clean_genre_string(raw_genre):
    """
    Làm sạch chuỗi genres như: "['Fantasy'", "'Romance']"
    → Trả về: ["Fantasy", "Romance"]
    """
    return [re.sub(r"[\[\]']+", "", g).strip() for g in str(raw_genre).split(",")]

@app.get("/")
def read_root():
    return {"message": "FastAPI đang chạy!"}
@app.post("/upload-csv/")
async def upload_csv(file: UploadFile = File(...)):
    if not file.filename.endswith(".csv"):
        return JSONResponse(content={"error": "Chỉ hỗ trợ file .csv"}, status_code=400)

    try:
        contents = await file.read()
        df = pd.read_csv(BytesIO(contents))
        total = len(df)
        num_instock = floor(total * 0.5)
        num_outofstock = floor(total * 0.05)
        num_comingsoon = floor(total * 0.2)
        num_onsale = floor(total * 0.2)
        num_unavailable = total - (  # Đảm bảo tổng đúng bằng total
                num_instock + num_outofstock + num_comingsoon + num_onsale
        )
        status_list = (
                ["InStock"] * num_instock +
                ["OutOfStock"] * num_outofstock +
                ["ComingSoon"] * num_comingsoon +
                ["OnSale"] * num_onsale +
                ["Unavailable"] * num_unavailable
        )
        random.shuffle(status_list)

        # Gán vào DataFrame
        df["status"] = status_list
        # Làm sạch genres nếu có
        if "genres" in df.columns:
            df["genres"] = df["genres"].apply(clean_genre_string)

        # Thêm timestamp
        start_time = datetime.utcnow()
        df["createdAt"] = [start_time + timedelta(milliseconds=i*200) for i in range(len(df))]
        df.drop(columns=['content'], inplace=True)
        # Insert vào MongoDB
        records = df.to_dict(orient="records")
        if records:
            collection.insert_many(records)

        return {"message": f"Đã upload {len(records)} bản ghi vào MongoDB."}
    except Exception as e:
        return JSONResponse(content={"error": str(e)}, status_code=500)

@app.on_event("startup")
def load_data_and_models():
    global books_df, ratings_df
    global  tfidf_matrix, tfidf_sim
    global bert_embeddings

    books = list(collection.find({}, {"_id": 1, "title": 1, "description": 1, "genres": 1}))

    books_df = pd.DataFrame(books)
    books_df["_id"] = books_df["_id"].astype(str)
    books_df["text"] = books_df["title"] + " " + books_df["description"] + " " + books_df["genres"].apply(lambda x: " ".join(x))

    # ratings = list(db["Rating"].find({}, {"userId": 1, "bookId": 1, "rating": 1}))
    # ratings_df = pd.DataFrame(ratings)

    # Load TF-IDF model
    with open(os.path.join(MODEL_DIR, "tfidf_matrix.pkl"), "rb") as f:
        tfidf_matrix  = pickle.load(f)
    tfidf_sim = linear_kernel(tfidf_matrix, tfidf_matrix)



    # Load BERT embeddings
    bert_embeddings_path = os.path.join(MODEL_DIR, "bert_embeddings.pkl")
    with open(bert_embeddings_path, "rb") as f:
        bert_embeddings = pickle.load(f)

    print(books_df.head())
    if tfidf_sim is not None:
        print("TF-IDF loaded OK")

    if bert_embeddings is not None:
        print("BERT loaded OK")


    # # Load CF model
    # with open(os.path.join(MODEL_DIR, "cf_model.pkl"), "rb") as f:
    #     cf_model = pickle.load(f)
#
@app.post("/recommend/combined/{book_id}")
def recommend_combined(book_id: str, top_n: int = 5):
    # Check dữ liệu
    # if books_df.empty or tfidf_sim is None or bert_embeddings is None:
    #     return {"error": "Models or data not fully loaded"}

    # Tìm index sách
    # books = list(collection.find({}, {"_id": 1, "title": 1, "description": 1, "genres": 1}))
    # books_df = pd.DataFrame(books)
    #
    # books_df["text"] = books_df["title"] + " " + books_df["description"] + " " + books_df["genres"].apply(
    #     lambda x: " ".join(x))
    print(books_df.head())

    matched_indices = books_df.index[books_df['_id'] == book_id].tolist()
    if not matched_indices:
        return {"error": f"Book ID '{book_id}' not found."}
    idx = matched_indices[0]

    # TF-IDF
    tfidf_scores = list(enumerate(tfidf_sim[idx]))
    tfidf_dict = {
        books_df.iloc[i]["_id"]: score
        for i, score in tfidf_scores
        if books_df.iloc[i]["_id"] != book_id
    }

    # BERT
    query_embedding = bert_embeddings[idx]
    cos_scores = util.cos_sim(query_embedding, bert_embeddings)[0]
    bert_dict = {
        books_df.iloc[i]["_id"]: cos_scores[i].item()
        for i in range(len(books_df))
        if books_df.iloc[i]["_id"] != book_id
    }

    # Combine
    combined_scores = {}
    for book in books_df["_id"]:
        if book == book_id:
            continue
        scores = [
            tfidf_dict.get(book, 0),
            bert_dict.get(book, 0),
            # cf_dict.get(book, 0),  # Dễ dàng thêm sau
        ]
        combined_scores[book] = sum(scores) / len(scores)

    # Trả top N
    top_books = sorted(combined_scores.items(), key=lambda x: x[1], reverse=True)[:top_n]
    return {
        "recommendations": [
            {"bookId": book, "score": round(score, 4)}
            for book, score in top_books
        ]
    }

