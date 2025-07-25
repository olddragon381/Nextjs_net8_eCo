"use client";
import React from "react";
import Breadcrumb from "../Common/Breadcrumb";
import { AppDispatch, useAppSelector } from "@/redux/store";
import SingleItem from "./SingleItem";
import { useDispatch } from "react-redux";
import { removeAllItemsFromWishlist } from "@/redux/features/wishlist-slice";

export const Wishlist = () => {
  const wishlistItems = useAppSelector((state) => state.wishlistReducer.items);
  const dispatch = useDispatch<AppDispatch>();
  const handleClearCWishlist = async () => {
      const token = localStorage.getItem("token");
      if (!token) return;
      
      try {
        
        
  
        dispatch(removeAllItemsFromWishlist());
        localStorage.removeItem("cartSyncedFromServer"); // tùy chọn
        console.log("🧹 Cart cleared on server & client");
      } catch (err) {
        console.error("❌ Failed to clear cart:", err);
      }
    };

  return (
    <>
      <Breadcrumb title={"Wishlist"} pages={["Wishlist"]} />
      <section className="overflow-hidden py-20 bg-gray-2">
        <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0">
          <div className="flex flex-wrap items-center justify-between gap-5 mb-7.5">
            <h2 className="font-medium text-dark text-2xl">Danh sách yêu thích</h2>
            <button onClick={handleClearCWishlist} className="text-blue">Dọn tất cả sách</button>
          </div>

          <div className="bg-white rounded-[10px] shadow-1">
            <div className="w-full overflow-x-auto">
              <div className="min-w-[1170px]">
                {/* <!-- table header --> */}
                <div className="flex items-center py-5.5 px-10">
                  <div className="min-w-[83px]"></div>
                  <div className="min-w-[387px]">
                    <p className="text-dark">Sản phẩm</p>
                  </div>

                  <div className="min-w-[205px]">
                    <p className="text-dark">Giá</p>
                  </div>

                  <div className="min-w-[265px]">
                    <p className="text-dark">Trạng thái giỏ</p>
                  </div>

                  <div className="min-w-[150px]">
                    
                  </div>
                </div>

                {/* <!-- wish item --> */}
                {wishlistItems.map((item, key) => (
                  <SingleItem item={item} key={key} />
                ))}
              </div>
            </div>
          </div>
        </div>
      </section>
    </>
  );
};
