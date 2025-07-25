"use client"
import Breadcrumb from "@/components/Common/Breadcrumb";
import Link from "next/link";
import React from "react";
import { useRouter, useSearchParams } from "next/navigation";
import { useState } from "react";

import { showToast } from "@/components/Common/ShowToaster";
import { resetpassword } from "@/service/user/AuthServices";


const ResetPassword = () => {
 
  const searchParams = useSearchParams();
    const email = searchParams.get('email') || '';
  const [password, setPassword] = useState("");
  const [retypePassword, setRetypePassword] = useState("");
  const [error, setError] = useState("");
  const router = useRouter();
  

  const handleResetPassword = async (e) => {
    e.preventDefault();
    if (password !== retypePassword) {

      showToast('Mật khẩu không khớp', 'warning');

      return;
    }

    try {
      resetpassword(email,password)
      showToast('Thay đổi mật khẩu', 'success');
     
      router.push("/signin"); 
    } catch (err) {
      console.error("Thay đổi mật khẩu thất bại:", err);
      showToast('Thay đổi mật khẩu thất bại. Vui lòng thử lại.', 'error');

    }
  }

  return (
    <>
      <Breadcrumb title={"Reset Password"} pages={["Reset Password"]} />

      <section className="overflow-hidden py-20 bg-gray-2">
        <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0">
          <div className="max-w-[570px] w-full mx-auto rounded-xl bg-white shadow-1 p-4 sm:p-7.5 xl:p-11">
            <div className="text-center mb-11">
              <h2 className="font-semibold text-xl sm:text-2xl xl:text-heading-5 text-dark mb-1.5">
                Thay đổi mật khẩu
              </h2>
             
            </div>

            



            <div className="mt-5.5">
              <form onSubmit={handleResetPassword} >
                

                <div className="mb-5">
                  <label htmlFor="password" className="block mb-2.5">
                    Mật khẩu <span className="text-red">*</span>
                  </label>

                  <input
                    type="password"
                    name="password"
                    id="password"
                    placeholder="Ghi passwordpassword của bạn"
                    autoComplete="on"
                    className="rounded-lg border border-gray-3 bg-gray-1 placeholder:text-dark-5 w-full py-3 px-5 outline-none duration-200 focus:border-transparent focus:shadow-input focus:ring-2 focus:ring-blue/20"
                    value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  />
                </div>

                <div className="mb-5.5">
                  <label htmlFor="re-type-password" className="block mb-2.5">
                    Kiểm tra mật khẩu <span className="text-red">*</span>
                  </label>

                  <input
                    type="password"
                    name="re-type-password"
                    id="re-type-password"
                    placeholder="Re-type your password"
                    autoComplete="on"
                    className="rounded-lg border border-gray-3 bg-gray-1 placeholder:text-dark-5 w-full py-3 px-5 outline-none duration-200 focus:border-transparent focus:shadow-input focus:ring-2 focus:ring-blue/20"
                    value={retypePassword}
                    onChange={(e) => setRetypePassword(e.target.value)}
                  />
                </div>
                {error && (
                        <p className="text-red text-xs">{error}</p>
                      )}
                <button
                  type="submit"
                  className="w-full flex justify-center font-medium text-white bg-dark py-3 px-6 rounded-lg ease-out duration-200 hover:bg-blue mt-7.5"
                >
                  Thay đổi mật khẩu
                </button>

            
              </form>
            </div>
          </div>
        </div>
      </section>
    </>
  );
};

export default ResetPassword;
