"use client"
import { useEffect } from "react";

import { logout } from "@/service/user/AuthServices";
import { useRouter } from "next/navigation";

const LogoutPage = () => {
  const router = useRouter();
  const token = localStorage.getItem("token")  
  useEffect(() => {
 
    logout(token)
    localStorage.removeItem("token");

    setTimeout(() => {
      router.push("/"); 
    }, 2);
  }, []);

  return (
    <div className="text-center mt-10">
      <p>Đang đăng xuất...</p>
    </div>
  );
};

export default LogoutPage;
