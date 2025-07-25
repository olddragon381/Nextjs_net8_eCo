import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { setCartItems } from "@/redux/features/cart-slice";
import { AppDispatch } from "@/redux/store";
import { fetchCart } from "@/service/cart/CartService";

export default function CartSyncWrapper() {
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    const fetchCartFromServer = async () => {
      const token = localStorage.getItem("token");
      if (!token) return;

      try {
        const data = await fetchCart(token);
        const item = data.items
        console.log("🛒 Cart item debug:", item);
        const mappedItems = item.map((item) => ({
          id: item.id,
          title: item.title,     // bạn có thể bổ sung nếu cần fetch thêm thông tin sách
          price: item.price,
          quantity: Number(item.quantity), 
          image: item.image,     // tương tự
        }));

        dispatch(setCartItems(mappedItems));
        console.log("🛒 Cart mapped & set to Redux:", mappedItems);
      } catch (err) {
        console.error("❌ Failed to fetch cart:", err);
      }
    };

    fetchCartFromServer();
  }, [dispatch]);

  return null;
}
