"use client";

import { useDispatch, useSelector } from "react-redux";
import { useEffect, useRef } from "react";
import { removeAllItemsFromCart, selectCartItems } from "@/redux/features/cart-slice";
import axios from "@/app/utils/axios";
import { selectCurrentUser } from "@/redux/features/user-slice";
import { AppDispatch } from "@/redux/store";
import { fetchCart } from "@/service/cart/CartService";

const CartSyncOnLogin = ({ userIsLoggedIn }: { userIsLoggedIn: boolean }) => {
  const dispatch = useDispatch<AppDispatch>();
  const cartItems = useSelector(selectCartItems);
  const user = useSelector(selectCurrentUser);
  const syncedRef = useRef(false);

  useEffect(() => {
    const syncToServer = async () => {
      const token = localStorage.getItem("token");
      const alreadySynced = localStorage.getItem("cartSyncedFromServer");

      if (
        !userIsLoggedIn ||
        !user?.id ||
        cartItems.length === 0 ||
        alreadySynced ||
        syncedRef.current ||
        !token
      ) return;

      try {
        syncedRef.current = true;

        // 🛒 Lấy lại cart từ server
        const serverCart = await fetchCart(token);
        const serverItemIds = serverCart?.items?.map((i) => i.id) ?? [];

        // 🧠 Chỉ giữ lại item local chưa có trên server
        const itemsToAdd = cartItems.filter(
          (localItem) => !serverItemIds.includes(localItem.id)
        );

        if (itemsToAdd.length === 0) {
          console.log("🛑 Không có item nào cần thêm (đã trùng)");
          return;
        }

        const payload = itemsToAdd.map((item) => ({
          bookId: item.id,
          quantity: item.quantity,
        }));

        await axios.post("/Cart/add", payload, {
          headers: { Authorization: `Bearer ${token}` },
        });

        console.log("✅ Đồng bộ cart lên server thành công");
        dispatch(removeAllItemsFromCart());
      } catch (err) {
        console.error("❌ Lỗi khi sync cart lên server:", err);
      }
    };

    syncToServer();
  }, [userIsLoggedIn, user, cartItems, dispatch]);

  return null;
};

export default CartSyncOnLogin;
