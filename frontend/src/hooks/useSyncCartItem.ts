// hooks/useSyncCartItem.ts
import { useEffect } from "react";
import { useSelector } from "react-redux";
import { selectCartItems } from "@/redux/features/cart-slice";
import { selectCurrentUser } from "@/redux/features/user-slice";
import axios from "@/app/utils/axios";

export const useSyncCartItem = () => {
  const cartItems = useSelector(selectCartItems);
  const user = useSelector(selectCurrentUser);

  useEffect(() => {
    if (!user || !user.id || cartItems.length === 0) return;

    const latestItem = cartItems[cartItems.length - 1]; 

    if (!latestItem || !latestItem.quantity || !latestItem.id) return;

    const token = localStorage.getItem("token");
    if (!token) return;

    const payload = [
      {
        bookId: latestItem.id,
        quantity: latestItem.quantity,
      },
    ];
    console.log("🧾 Payload gửi lên server:", payload);
    axios
      .post("/Cart/add", payload, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then(() => {
        console.log("✅ Synced item to MongoDB:", payload);
      })
      .catch((err) => {
        console.error("❌ Failed to sync item to MongoDB:", err);
      });
  }, [cartItems.length]); // chỉ chạy khi length thay đổi
};
