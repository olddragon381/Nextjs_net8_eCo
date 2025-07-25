import React, { useState } from "react";
import Image from "next/image";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "@/redux/store";
import { setPaymentMethod } from "@/redux/features/checkout-slice";

const PaymentMethod = () => {
 
  const dispatch = useDispatch();
  const payment = useSelector((state: RootState) => state.checkout.paymentMethod);
   return (
    <div className="bg-white shadow-1 rounded-[10px] mt-7.5">
      <div className="border-b border-gray-3 py-5 px-4 sm:px-8.5">
        <h3 className="font-medium text-xl text-dark">Phương thức thanh toán</h3>
      </div>

      <div className="p-4 sm:p-8.5">
        <div className="flex flex-col gap-3">
          {/* BANK */}
          <label htmlFor="bank" className="flex cursor-pointer select-none items-center gap-4">
            <div className="relative">
              <input
                type="radio"
                name="payment"
                id="bank"
                className="sr-only"
                checked={payment === "bank"}
                onChange={() => dispatch(setPaymentMethod("bank"))}
              />
              <div className={`flex h-4 w-4 items-center justify-center rounded-full ${payment === "bank" ? "border-4 border-blue" : "border border-gray-4"}`} />
            </div>

            <div className={`w-full rounded-md border-[0.5px] py-3.5 px-5 ease-out duration-200 hover:bg-gray-2 hover:border-transparent hover:shadow-none ${payment === "bank" ? "border-transparent bg-gray-2" : "border-gray-4 shadow-1"}`}>
              <div className="flex items-center">
                <div className="pr-2.5">
                  <Image src="/images/checkout/bank.svg" alt="bank" width={29} height={12} />
                </div>
                <div className="border-l border-gray-4 pl-2.5">
                  <p>Chuyển khoản</p>
                </div>
              </div>
            </div>
          </label>

          {/* CASH */}
          <label htmlFor="cash" className="flex cursor-pointer select-none items-center gap-4">
            <div className="relative">
              <input
                type="radio"
                name="payment"
                id="cash"
                className="sr-only"
                checked={payment === "cash"}
                onChange={() => dispatch(setPaymentMethod("cash"))}
              />
              <div className={`flex h-4 w-4 items-center justify-center rounded-full ${payment === "cash" ? "border-4 border-blue" : "border border-gray-4"}`} />
            </div>

            <div className={`w-full rounded-md border-[0.5px] py-3.5 px-5 ease-out duration-200 hover:bg-gray-2 hover:border-transparent hover:shadow-none ${payment === "cash" ? "border-transparent bg-gray-2" : "border-gray-4 shadow-1"}`}>
              <div className="flex items-center">
                <div className="pr-2.5">
                  <Image src="/images/checkout/cash.svg" alt="cash" width={21} height={21} />
                </div>
                <div className="border-l border-gray-4 pl-2.5">
                  <p>Gửi tiền trực tiếp</p>
                </div>
              </div>
            </div>
          </label>
        </div>
      </div>
    </div>
  );
};


export default PaymentMethod;
