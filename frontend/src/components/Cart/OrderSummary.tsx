import { selectTotalPrice } from "@/redux/features/cart-slice";
import { useAppSelector } from "@/redux/store";
import { selectCurrentUser } from "@/redux/features/user-slice";
import React from "react";
import { useSelector } from "react-redux";
import { showToast } from "../Common/ShowToaster";

import { useRouter } from "next/navigation";

const OrderSummary = () => {
  const cartItems = useAppSelector((state) => state.cartReducer.items);
  const totalPrice = useSelector(selectTotalPrice);
  const user = useAppSelector(selectCurrentUser);
  const router = useRouter();
const handleCheckout = () => {
  if (!user || !user.id) {
    showToast("⚠️ Bạn cần đăng nhập để thanh toán", "warning");
    return;
  }

  showToast("✅ Đã đăng nhập. Bắt đầu thanh toán...", "success");
  router.push("/checkout")
  
};

  return (
    <div className="lg:max-w-[700px] w-full">
      {/* <!-- order list box --> */}
      <div className="bg-white shadow-1 rounded-[10px]">
        <div className="border-b border-gray-3 py-5 px-4 sm:px-8.5">
          <h3 className="font-medium text-xl text-dark">Đơn hàng</h3>
        </div>

        <div className="pt-2.5 pb-8.5 px-4 sm:px-8.5">
          {/* <!-- title --> */}
          <div className="grid grid-cols-[3fr_1fr_1fr] gap-4 py-5 border-b border-gray-3">
    <h4 className="font-medium text-dark">Sản phẩm</h4>
    <h4 className="font-medium text-dark text-center">Số lượng</h4>
    <h4 className="font-medium text-dark text-right">Tổng tiền</h4>
  </div>

          {/* <!-- product item --> */}
          {cartItems.map((item, key) => (
    <div key={key} className="grid grid-cols-[3fr_1fr_1fr] gap-4 py-5 border-b border-gray-3">
      <p className="text-dark">{item.title}</p>
      <p className="text-dark text-center">{item.quantity}</p>
      <p className="text-dark text-right">{item.price * item.quantity}.000đ</p>
    </div>
  ))}

          {/* <!-- total --> */}
          <div className="grid grid-cols-3 gap-4 pt-5">
    <p className="font-medium text-lg text-dark col-span-2">Tổng tiền</p>
    <p className="font-medium text-lg text-dark text-right">{totalPrice}.000đ</p>
  </div>
         

          {/* <!-- checkout button --> */}
          <button onClick={handleCheckout}
            type="button"
            className="w-full flex justify-center font-medium text-white bg-blue py-3 px-6 rounded-md ease-out duration-200 hover:bg-blue-dark mt-7.5"
          >
            Process to Checkout
          </button>
        </div>
      </div>
    </div>
  );
};

export default OrderSummary;
