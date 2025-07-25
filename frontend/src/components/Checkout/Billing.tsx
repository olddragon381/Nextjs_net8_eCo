import { setAddress, setNameForOrder, setPhone } from "@/redux/features/checkout-slice";
import { RootState } from "@/redux/store";
import React from "react";
import { useDispatch, useSelector } from "react-redux";

const Billing = () => {

  const dispatch = useDispatch();
  const address = useSelector((state: RootState) => state.checkout.address);
  const namefororder = useSelector((state: RootState) => state.checkout.namefororder);
  
  const phone = useSelector((state: RootState) => state.checkout.phone);
  return (
    <div >
      <h2 className="font-medium text-dark text-xl sm:text-2xl mb-5.5">
        Thông tin đơn hàng
      </h2>

      <div className="bg-white shadow-1 rounded-[10px] p-4 sm:p-8.5">
        <div className="flex flex-col lg:flex-row gap-5 sm:gap-8 mb-5">
           <div className="w-full">
            <label htmlFor="lastName" className="block mb-2.5">
              Tên chủ đơn <span className="text-red">*</span>
            </label>

            <input
              value={namefororder}
              type="text"
              name="lastName"
              id="lastName"
              placeholder="Tên chủ đơn "
              onChange={(e) => dispatch(setNameForOrder(e.target.value))}
              className="rounded-md border border-gray-3 bg-gray-1 placeholder:text-dark-5 w-full py-2.5 px-5 outline-none duration-200 focus:border-transparent focus:shadow-input focus:ring-2 focus:ring-blue/20"
            />
          </div>
        </div>

        <div className="mb-5">
          <label htmlFor="address" className="block mb-2.5">
            Địa chỉ
            <span className="text-red">*</span>
          </label>

          <input
            type="text"
            name="address"
            value={address}
            onChange={(e) => dispatch(setAddress(e.target.value))}
            id="address"
            placeholder="Địa chỉ"
            className="rounded-md border border-gray-3 bg-gray-1 placeholder:text-dark-5 w-full py-2.5 px-5 outline-none duration-200 focus:border-transparent focus:shadow-input focus:ring-2 focus:ring-blue/20"
          />
        </div>

       
        <div className="mb-5">
          <label htmlFor="phone" className="block mb-2.5">
            Phone <span className="text-red">*</span>
          </label>

          <input
            type="text"
            name="phone"
            id="phone"
            value={phone}
            onChange={(e) => dispatch(setPhone(e.target.value))}
            className="rounded-md border border-gray-3 bg-gray-1 placeholder:text-dark-5 w-full py-2.5 px-5 outline-none duration-200 focus:border-transparent focus:shadow-input focus:ring-2 focus:ring-blue/20"
          />
        </div>
      </div>
    </div>
  );
};

export default Billing;
