# 📚 Bookstore Platform with Recommender System

A modern full-stack **bookstore web application** with an integrated **AI-powered recommendation system** to enhance the user shopping experience.

---

## 🛠️ Tech Stack

### ✅ Frontend
- [Next.js](https://nextjs.org/) with JavaScript
- Redux Toolkit for state management
- Tailwind CSS for styling
- Swiper for carousels

### ✅ Backend
- .NET 8 Web API
- Clean Architecture 
- MongoDB (NoSQL database)
- JWT Authentication & Role-based Authorization

### ✅ AI Recommendation System
- Python + FastAPI
- Hybrid approach:
  - **Content-Based Filtering**: TF-IDF & BERT embeddings
  - 
- REST API between .NET backend and Python service
---

## ⚙️ Features

### 📦 Bookstore Functionality
- 🔍 Browse/search books with filters
- 🛒 Add to cart, wishlist
- 📝 Ratings & comments
- 📚 Book details with author, genre, etc.
- 💳 Checkout process

### 👤 User System
- Register / Login (JWT-based)
- Admin roles & permissions
- View order history

### 🧠 Recommendation System
- Recommend similar books on product page
- Recently viewed & trending books
- Personalized suggestions per user
- Fallback to popular/genre-based books

---

## 🔄 System Architecture

![image](https://github.com/user-attachments/assets/a9a29f4f-70e2-4c57-bfff-8cb5342e35cd)

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/yourname/bookstore-app.git
cd bookstore-app
```
### 2. Frontend Setup
```bash
cd frontend
npm install
npm run dev
```
### 3. Backend Setup (.NET)
```bash
cd frontend
npm install
npm run dev
```
### 4. Recommendation Service (Python)
```bash
cd recommender
pip install -r requirements.txt
uvicorn main:app --reload
```

🧪 Testing
Backend: xUnit for .NET
Frontend: Jest / React Testing Library (optional)
API: Postman collections provided

📊 Future Improvements
✅ Real-time collaborative filtering
✅ Social features (follow friends)
🔄 Improve BERT-based understanding
📈 Analytics dashboard for admin
📬 Email notifications
