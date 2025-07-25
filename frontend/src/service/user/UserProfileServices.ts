import { UpdateProfile, UserProfile } from './../../types/userprofile';
import axios from "@/app/utils/axios";

export async function fetchUserProfile() 
{
    const token = localStorage.getItem("token");
    if (!token) throw new Error("Chưa đăng nhập");

  const response = await axios.get("/UserProfile/getuserprofile", {
    headers: {
      Authorization: `Bearer ${token}`
    }
  });

  return response.data;
    
}
export async function updateUserProfile(userProfile : UpdateProfile) 
{
    const token = localStorage.getItem("token");
    if (!token) throw new Error("Chưa đăng nhập");

    

  const response = await axios.post("/UserProfile/updateuserprofile",userProfile, {
    
    headers: {
      Authorization: `Bearer ${token}`
    } 
  });

  return response.data;
    
}

export async function updateUserFullname(fullname: string) {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("Chưa đăng nhập");

  const response = await axios.post(
    "/UserProfile/updateuserfullname",
    { fullname }, 
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );

  return response.data;
}


