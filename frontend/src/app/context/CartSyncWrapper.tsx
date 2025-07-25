"use client";

import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { setCartItems } from "@/redux/features/cart-slice";
import { selectCurrentUser } from "@/redux/features/user-slice";
import { AppDispatch } from "@/redux/store";
import { fetchCart } from "@/service/cart/CartService";

export default function CartSyncWrapper({ userIsLoggedIn = false }: { userIsLoggedIn?: boolean }) {
  const dispatch = useDispatch<AppDispatch>();
  const user = useSelector(selectCurrentUser);

  useEffect(() => {
    const syncFromServer = async () => {
      const token = localStorage.getItem("token");
      if (!userIsLoggedIn || !token || !user?.id) return;

      try {
        const cart = await fetchCart(token);
        const items = cart.items;
        console.log("🛒 Cart item debug:", items);
        if (!Array.isArray(items)) return;

        const mappedItems = items.map((item) => ({
          id: item.id,
          title: item.title,
          price: item.price,
          quantity: item.quantity, // sửa chính tả: quality → quantity
          image: item.image,
        }));

        dispatch(setCartItems(mappedItems));
        localStorage.setItem("cartSyncedFromServer", "true");
        console.log("🛒 Đồng bộ cart từ server thành công");
      } catch (err) {
        console.error("❌ Lỗi khi lấy cart từ server:", err);
      }
    };

    syncFromServer();
  }, [userIsLoggedIn, user, dispatch]);

  return null;
}
