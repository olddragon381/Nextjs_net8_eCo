'use client';

import { forgotpassword, verifyotp } from '@/service/user/AuthServices';
import { useState } from 'react';
import Breadcrumb from '../Common/Breadcrumb';
import { useRouter, useSearchParams } from 'next/navigation';


export default function OTPPassword() {
  const [otp, setOtp] = useState(['', '', '', '']);
  const [message, setMessage] = useState('');
  const [loading, setLoading] = useState(false);
   const router = useRouter();
  const searchParams = useSearchParams();
  const email = searchParams.get('email') || '';

  const handleChange = (index: number, value: string) => {
    if (!/^\d?$/.test(value)) return;

    const newOtp = [...otp];
    newOtp[index] = value;
    setOtp(newOtp);

    // Focus next input
    if (value && index < 3) {
      const nextInput = document.getElementById(`otp-${index + 1}`);
      nextInput?.focus();
    }
  };

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    const enteredOtp = otp.join('');

    try {
      setLoading(true);
       verifyotp(email, enteredOtp) ;
       router.push(`/reset-password?email=${encodeURIComponent(email)}`);
      setMessage('Xác minh thành công!');
    } catch (err) {
      setMessage('Mã OTP không chính xác hoặc đã hết hạn.');
    } finally {
      setLoading(false);
    }
  };

  const handleResend = async () => {
    try {
        forgotpassword(email)
 
      setMessage('Đã gửi lại mã OTP!');
    } catch {
      setMessage('Không thể gửi lại OTP.');
    }
  };

  return (
 <>
      <Breadcrumb title={"Check OTP"} pages={["Check OTP"]} />
      <section className="overflow-hidden py-20 bg-gray-2">
        <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0">
          <div className="max-w-[570px] w-full mx-auto rounded-xl bg-white shadow-1 p-4 sm:p-7.5 xl:p-11">
            <div className="text-center mb-11">
              <h2 className="font-semibold text-xl sm:text-2xl xl:text-heading-5 text-dark mb-1.5">
             Xác minh OTP
              </h2>
              
            </div>

               <div className="max-w-md mx-auto text-center bg-white px-4 sm:px-8 py-10 rounded-xl shadow">
      <header className="mb-8">
       
        <p className="text-[15px] text-slate-500">
          Nhập mã xác minh gồm 4 chữ số đã gửi đến email: <b>{email}</b>
        </p>
      </header>

      <form onSubmit={handleSubmit}>
        <div className="flex items-center justify-center gap-3 mb-4">
          {otp.map((value, i) => (
            <input
              key={i}
              id={`otp-${i}`}
              type="text"
              inputMode="numeric"
              maxLength={1}
              value={value}
              onChange={(e) => handleChange(i, e.target.value)}
              className="w-14 h-14 text-center text-2xl font-extrabold text-slate-900 bg-gray border border-transparent hover:border-slate-200 rounded p-4 outline-none focus:bg-gray focus:border-indigo-400 focus:ring-2 focus:ring-indigo-100"
            />
          ))}
        </div>

        <button
          type="submit"
          disabled={loading}
          className="w-full inline-flex justify-center rounded-lg bg-blue px-4 py-2.5 text-sm font-medium text-white shadow hover:bg-indigo-600"
        >
           
          {loading ? ' Đang xác minh...' : 'Xác minh mã OTP'}
        </button>
      </form>

      <div className="text-sm text-slate-500 mt-4">
        Không nhận được mã?{' '}
        <button
          className="font-medium text-indigo-500 hover:text-indigo-600"
          onClick={handleResend}
        >
          Gửi lại
        </button>
      </div>

      {message && <p className="text-sm mt-4 text-gray-600">{message}</p>}
    </div>
          </div>
        </div>
      </section>
    </>

 
  );
}
