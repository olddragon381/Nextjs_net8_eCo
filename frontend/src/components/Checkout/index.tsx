"use client";
import React from "react";
import Breadcrumb from "../Common/Breadcrumb";


import ShippingMethod from "./ShippingMethod";
import PaymentMethod from "./PaymentMethod";
import Coupon from "./Coupon";
import Billing from "./Billing";
import { useDispatch, useSelector } from "react-redux";
import { RootState, useAppSelector } from "@/redux/store";
import { resetCheckout, setNote } from "@/redux/features/checkout-slice";
import { removeAllItemsFromCart, selectTotalPrice } from "@/redux/features/cart-slice";

import { addToOrderHistory, createOrder } from "@/redux/features/order-slice";
import { createCheckout, handlePayment } from "@/service/checkout/CheckoutService";
import { Order } from "@/types/order";
import { removeCart } from "@/service/cart/CartService";
import { useRouter } from 'next/navigation'
import { showToast } from "../Common/ShowToaster";

const Checkout = () => {


const router = useRouter();
const dispatch = useDispatch();

const cartItems = useSelector((state: RootState) => state.cartReducer.items);
const checkout = useSelector((state: RootState) => state.checkout);
const totalPrice = useSelector(selectTotalPrice);
const user = useSelector((state: RootState) => state.user);
const note = useSelector((state: RootState) => state.checkout.note);

const handleCheckout = async (e) => {
  e.preventDefault();
  const token = localStorage.getItem("token");

  if (!token) {
    alert("Bạn cần đăng nhập để đặt hàng.");
    return;
  }

  const orderItems = cartItems.map((item) => ({
    bookid: item.id,
    price: item.price,
    quantity: item.quantity,
    total: item.price * item.quantity,
  }));

  const orderCheckout: Order = {
    userId: user.user.id,
    items: orderItems,
    address: checkout.address,
    note: note,
    paymentmethod: checkout.paymentMethod,
    namefororder: checkout.namefororder,
    phone: checkout.phone,
    couponcode: checkout.coupon ?? "",
    totalamount: totalPrice,
  };

  try {
    // Bước 1: Gửi đơn hàng lên backend
    const res = await createCheckout(token, orderCheckout);
       console.log("res",res.data)
    const createdOrderId = res.data.id;
    console.log("createdOrderId",createdOrderId)

    // Bước 2: Nếu thanh toán bằng ngân hàng
    if (checkout.paymentMethod === "bank") {
      const paymentRes = await handlePayment(
        orderCheckout.namefororder,
        orderCheckout.totalamount,
     
      );
      console.log("paymentRes:", paymentRes);
      console.log("VNPAY URL:", paymentRes?.paymentUrl);
      if (paymentRes?.paymentUrl) {
          window.location.href = paymentRes.paymentUrl;
      } else {
        throw new Error("Không thể tạo link thanh toán VNPAY");
      }
    }

    // Bước 3: Nếu là COD
    const createdOrder = { ...orderCheckout, id: createdOrderId };

    dispatch(createOrder(orderCheckout));
    dispatch(addToOrderHistory(createdOrder));
    dispatch(resetCheckout());
    dispatch(removeAllItemsFromCart());
    removeCart(token, user.user.id);

    router.push("/checkout/success");
    showToast("Đặt hàng thành công!", "success");
  } catch (err) {
    alert("Có lỗi khi đặt hàng. Vui lòng thử lại.");
    console.error(err);
    router.push("/cart");
  }
};



  return (
    <>
      <Breadcrumb title={"Checkout"} pages={["checkout"]} />
      <section className="overflow-hidden py-20 bg-gray-2">
        <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0">
          <div>
            <div className="flex flex-col lg:flex-row gap-7.5 xl:gap-11">
              {/* <!-- checkout left --> */}
              <div className="lg:max-w-[670px] w-full">
                

                {/* <!-- billing details --> */}
                <Billing />

              

                {/* <!-- others note box --> */}
                <div className="bg-white shadow-1 rounded-[10px] p-4 sm:p-8.5 mt-7.5">
                  <div>
                    <label htmlFor="notes" className="block mb-2.5">
                      Other Notes (optional)
                    </label>

                    <textarea
                      name="notes"
                      id="notes"
                      rows={5}
                      value={note}
                      onChange={(e) => dispatch(setNote(e.target.value))}
                      placeholder="Chú thích cho đơn hàng"
                      className="rounded-md border border-gray-3 bg-gray-1 placeholder:text-dark-5 w-full p-5 outline-none duration-200 focus:border-transparent focus:shadow-input focus:ring-2 focus:ring-blue/20"
                    ></textarea>
                  </div>
                </div>
              </div>

              {/* // <!-- checkout right --> */}
              <div className="max-w-[455px] w-full">
                {/* <!-- order list box --> */}
                <div className="">
  <div className="bg-white shadow-1 rounded-[10px]">
                  <div className="border-b border-gray-3 py-5 px-4 sm:px-8.5">
                    <h3 className="font-medium text-xl text-dark">
                      Đơn hàng của bạn
                    </h3>
                  </div>

                  <div className="pt-2.5 pb-8.5 px-4 sm:px-8.5">
                    {/* <!-- title --> */}
                    <div className="flex items-center justify-between py-5 border-b border-gray-3">
                      <div>
                        <h4 className="font-medium text-dark">Sản phẩm</h4>
                      </div>
                      <div>
                        <h4 className="font-medium text-dark text-right">
                          Tổng phụ
                        </h4>
                      </div>
                    </div>

                    
                    {cartItems.length > 0 &&
                    cartItems.map((item) => (

                      <div key={item.id} className="flex items-center justify-between py-5 border-b border-gray-3">
                      <div>
                        <p className="text-dark">{item.title}</p>
                      </div>
                      <div>
                        <p className="text-dark text-right">{item.price*item.quantity +".000 đ" }</p>
                      </div>
                    </div>
                    ))}

                   

                    {/* <!-- total --> */}
                    <div className="flex items-center justify-between pt-5">
                      <div>
                        <p className="font-medium text-lg text-dark">Total</p>
                      </div>
                      <div>
                        <p className="font-medium text-lg text-dark text-right">
                         {totalPrice}
                        </p>
                      </div>
                    </div>
                  </div>
                </div>
                </div>
              

                {/* <!-- coupon box --> */}
                <Coupon />

                {/* <!-- shipping box --> */}
                {/* <ShippingMethod /> */}

                {/* <!-- payment box --> */}
                <PaymentMethod />

                {/* <!-- checkout button --> */}
                <button
                  type="submit"
                  onClick={handleCheckout}
                  className="w-full flex justify-center font-medium text-white bg-blue py-3 px-6 rounded-md ease-out duration-200 hover:bg-blue-dark mt-7.5"
                >
                  Đặt hàng
                </button>
              </div>
            </div>
          </div>
        </div>
      </section>
    </>
  );
};

export default Checkout;
