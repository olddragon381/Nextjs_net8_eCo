'use client';

import { useState } from 'react';
import { useRouter } from "next/navigation";

import { forgotpassword } from '@/service/user/AuthServices';
import Breadcrumb from '../Common/Breadcrumb';

export default function ForgotPassword() {
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');
  
     const router = useRouter();


  const handleSendOtp = async (e: React.FormEvent) => {
  e.preventDefault(); 

  try {
    await forgotpassword(email); // ✅ bạn nên await
    setMessage('OTP đã được gửi đến email của bạn.');
    router.push(`/forget-password/OTP?email=${encodeURIComponent(email)}`);
  } catch (err) {
    setMessage('Không thể gửi OTP. Kiểm tra lại email.');
  }
};

  return (
       <>
      <Breadcrumb title={"ForgetPassword"} pages={["Forget Password"]} />
      <section className="overflow-hidden py-20 bg-gray-2">
        <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0">
          <div className="max-w-[570px] w-full mx-auto rounded-xl bg-white shadow-1 p-4 sm:p-7.5 xl:p-11">
            <div className="text-center mb-11">
              <h2 className="font-semibold text-xl sm:text-2xl xl:text-heading-5 text-dark mb-1.5">
                Gửi Email
              </h2>
              
            </div>

            <div>
              <form onSubmit={handleSendOtp}>
                <div className="mb-5">
                  <label htmlFor="email" className="block mb-2.5">
                    Email
                  </label>

                  <input
                    type="email"
                    name="email"
                    id="email"
                    placeholder="Enter your email"
                    className="rounded-lg border border-gray-3 bg-gray-1 placeholder:text-dark-5 w-full py-3 px-5 outline-none duration-200 focus:border-transparent focus:shadow-input focus:ring-2 focus:ring-blue/20"
                    value={email}
        onChange={(e) => setEmail(e.target.value)}
                  />
                </div>

               

                <button
                type="submit"
                  className="w-full flex justify-center font-medium text-white bg-dark py-3 px-6 rounded-lg ease-out duration-200 hover:bg-blue mt-7.5"
                
                >
                    Gửi email
                </button>
                  
                
             
                  
 

                


               
              </form>
            </div>
          </div>
        </div>
      </section>
    </>
  
  );
}
