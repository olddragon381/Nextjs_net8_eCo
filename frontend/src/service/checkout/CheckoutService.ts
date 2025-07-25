import axios  from '@/app/utils/axios';
import { Order } from '@/types/order';
// services/categoryService.ts



export async function createCheckout(token: string, data: Order) {
  try {
    const response = await axios.post("/Order/createorder", data, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response;
  } catch (err) {
    console.error("❌ Lỗi khi gửi order về server:", err);
    throw err;
  }
}


export async function getCheckout(token: string) {
  try {
    const response = await axios.get("/Order/getOrder", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (err) {
    console.error("❌ Lỗi khi gửi order về server:", err);
    throw err;
  }
}

export async function handlePayment(Name, TotalAmount) {
  try {


    const res = await axios.post("/Payment/create", {
     
      orderType: "other",
      amount: TotalAmount*100,
      orderDescription: "Thanh toán đơn hàng bằng ngân hàng",
      name: Name,
    }, {
      headers: {
        "Content-Type": "application/json"
      }
    });

    return res.data; // { paymentUrl: "..." }
  } catch (err) {
    console.error("Lỗi khi tạo thanh toán:", err.response?.data || err.message);
    alert("Tạo thanh toán thất bại");
    return null;
  }
}