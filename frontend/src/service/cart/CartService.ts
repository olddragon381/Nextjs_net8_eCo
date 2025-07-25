import axios from "@/app/utils/axios";
import { Cart } from "@/types/cart";

export async function fetchCart(token: string): Promise<Partial<Cart>> {
  try {
    const response = await axios.get<Cart>("/Cart/GetCart", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    console.log("📦 response fetchCart:", response.data);

    // ✅ Đảm bảo trả về dạng đúng
    return response.data ?? { items: [], totalPrice: 0 };
  } catch (err) {
    console.error("❌ Lỗi khi lấy cart từ server:", err);

    // ✅ Return fallback empty cart
    return { items: [], totalPrice: 0 };
  }
}

export async function removeIteminCart(token: string, id: string) {
  try {
     await axios.delete(`/Cart/delete-item/${id}`, {
        headers: { Authorization: `Bearer ${token}` },
     }
    )
   
  } catch (err) {
    console.error("❌ Lỗi xoa từ server:", err)


  }
}
export async function removeCart(token: string, useid: string) {
  try {
     await axios.delete(`/Cart/ClearCart`, {
        headers: { Authorization: `Bearer ${token}` },
     }
    )
   
  } catch (err) {
    console.error("❌ Lỗi xoa từ server:", err)
  }
}