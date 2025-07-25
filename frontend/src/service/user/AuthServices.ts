import axios from "@/app/utils/axios";

import { setUser } from "@/redux/features/user-slice";
import { AppDispatch } from "@/redux/store"; // nếu cần kiểu
import { useRouter } from "next/router";


export async function login(email: string, password: string, dispatch: AppDispatch) {
  const res = await axios.post("/Auth/login", {
    email,
    passwordHash: password,
  });

  const data = res.data;
  localStorage.setItem("token", data.token);

  dispatch(setUser({
    id: data.userId,
    fullname: data.fullname,
    email: data.email,
    role: data.userRole, // 👈 kiểm tra đúng tên field
  }));

  return data; // 👈 trả về để component xử lý redirect
}

export async function fetchCurrentUser() {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("Chưa đăng nhập");

  const response = await axios.get("/Auth/me", {
    headers: {
      Authorization: `Bearer ${token}`
    }
  });

  return response.data;
}



export async function register (fullName: string, email: string, password: string ) {
  const res = await axios.post('/Auth/register', { 
    
  "fullname": fullName,
  "password": password,
  "email": email

});

  return res.data;
}
export async function forgotpassword ( email: string) {
  await axios.post('Auth/forgot-password', { 
    
  "email": email

});

}
export async function verifyotp ( email: string, otp: string) {
  await axios.post('Auth/verify-otp', { 
    
  "email": email,
  "otp": otp

});

}
export async function resetpassword ( email: string, newPassword: string) {
  await axios.post('Auth/reset-password', { 
    
  "email": email,
  "newPassword": newPassword

})};



export async function changepassword(token, currentPassword, newPassword, confirmNewPassword) {
  const changedata = {
    currentPassword,
    newPassword,
    confirmNewPassword
  };

  const res = await axios.post(
    '/Auth/changepassword',
    changedata,
    {
      headers: {
        Authorization: `Bearer ${token}`
      }
    }
  );

  return res.data;
}




export async function logout  (token) {
  const res = await axios.post('/Auth/logout', { 
  headers: {
      Authorization: `Bearer ${token}`
    }

});

  return res.data;
}