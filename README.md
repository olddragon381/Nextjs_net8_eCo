# 📚 Bookstore Platform with Recommender System
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li>1. Clone the Repository</li>
        <li>2. Frontend Setup</li>
        <li>3. Backend Setup (.NET)<</li>
        <li>4. Recommendation Service (Python)</li> 
      </ul>
    </li>
    <li><a href="#feature">Feature</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>

https://github.com/olddragon381/Nextjs_net8_eCo

---
<!-- ABOUT THE PROJECT -->
## About The Project
A modern full-stack **bookstore web application** with an integrated **AI-powered recommendation system** to enhance the user shopping experience.
![image](https://github.com/user-attachments/assets/4120cf37-aa54-4d0f-b822-eaa21e71bafa)
![backendIMG](https://github.com/user-attachments/assets/f5d66513-ad62-4456-8f37-897774a2f0f4)
![Screenshot 2025-07-04 011526](https://github.com/user-attachments/assets/5c5574b4-a002-4bc0-a702-6e076936b2a6)
![Screenshot 2025-07-04 011601](https://github.com/user-attachments/assets/b62282f8-b20e-4f40-9655-1f4e6f876f2b)
![Screenshot 2025-07-04 011713](https://github.com/user-attachments/assets/9239ada8-a7ac-47cf-b28a-c46bedde57dd)
![Screenshot 2025-07-04 011725](https://github.com/user-attachments/assets/081c3d82-47d9-40b0-b1a0-b44f653c6103)
![Screenshot 2025-07-04 011731](https://github.com/user-attachments/assets/0104e536-15b3-4593-9962-17bb2de1e678)
![Screenshot 2025-07-04 011746](https://github.com/user-attachments/assets/110e2034-357b-4fe5-848e-2e9cd83af361)
![Screenshot 2025-07-04 011752](https://github.com/user-attachments/assets/ec462036-1159-4e52-8a87-dd47cdda7a2a)
![Screenshot 2025-07-04 011759](https://github.com/user-attachments/assets/7ad682a8-6656-484c-9a57-33c18dd2c7f8)
![Otp](https://github.com/user-attachments/assets/376b1a4c-3c37-4d59-9e6d-ae4e7f4d6b75)
<img width="857" height="842" alt="image" src="https://github.com/user-attachments/assets/0978301e-fea4-422f-afb8-5bd0623da73b" />


### Built With
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
- Redis cache

### ✅ AI Recommendation System
- Python + FastAPI
- Hybrid approach:
  - **Content-Based Filtering**: TF-IDF & BERT embeddings
  - 
- REST API between .NET backend and Python service
---
<!-- GETTING STARTED -->
## Getting Started
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
API: Postman collections provided

<!-- Features-->
## Features

### 📦 Bookstore Functionality
- 🔍 Browse/search books with filters
- 🛒 Add to cart, wishlist
- 📝 Ratings & comments
- 📚 Book details with author, genre, etc.
- 💳 Checkout process
- 💳 Checkout by VNPay

### 👤 User System
- Register / Login (JWT-based)
- Admin roles & permissions
- View order history
- Send OTP email
- Verify OTP
### 👤 Admin System
- Add data into DB
- Change role user
- Delete data 

### 🧠 Recommendation System
- Recommend similar books on product page
- Recently viewed & trending books
- Personalized suggestions per user
- Fallback to popular/genre-based books
- Use Redis 

---

## 🔄 System Architecture

![image](https://github.com/user-attachments/assets/a9a29f4f-70e2-4c57-bfff-8cb5342e35cd)

<!-- ROADMAP -->
## Roadmap
📊 Future Improvements
✅ Real-time collaborative filtering
📊 Add Collaborative Filtering for user 
✅ Social features (follow friends)
🔄 Improve BERT-based understanding
📈 Analytics dashboard for admin
📬 Email notifications

<!-- LICENSE -->
## License

Distributed under the Unlicense License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>
