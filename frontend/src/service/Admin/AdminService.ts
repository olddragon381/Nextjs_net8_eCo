import axios from "@/app/utils/axios";
import { AddNewCategory } from "@/types/category";

export async function getCount(token: string) {
  try {
    const headers = {
      Authorization: `Bearer ${token}`,
    };

    const [countUserRes, countBookRes, countOrderRes, countCategoryRes] = await Promise.all([
      axios.get('/admin/getcountusers', { headers }),
      axios.get('/admin/book/getcountbooks', { headers }),
      axios.get('/admin/order/getcount', { headers }),
      axios.get('/admin/getcountcategory', { headers }),
    ]);

    return {
      user: countUserRes.data,
      book: countBookRes.data,
      order: countOrderRes.data,
      category: countCategoryRes.data,
    };
  } catch (err) {
    console.error("❌ Lỗi lấy dữ liệu từ server:", err);
    return null;
  }
}


export async function getPaging(token: string, page: number, pageSize : number) {
  try {
     const res = await axios.get(`/admin/user/paging?page=${page}&pageSize=${pageSize}`, {
        headers: { Authorization: `Bearer ${token}` },
      });

    return res.data
  } catch (err) {
    console.error("❌ Lỗi lấy dữ liệu từ server:", err);
    return null;
  }
}

export async function deleUser(token: string, userId : string) {
  try {
      await axios.delete('/admin/deleteuser', {
        headers: { Authorization: `Bearer ${token}` },
        data: JSON.stringify(userId),
      });

   
  } catch (err) {
    console.error("❌ Lỗi lấy dữ liệu từ server:", err);
    return null;
  }
}


export async function changeRole(token: string, userId : string, newRole:number) {
  try {
      await axios.post(`/admin/changerole?newRole=${newRole}`, JSON.stringify(userId), {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

   
  } catch (err) {
    console.error("❌ Lỗi lấy dữ liệu từ server:", err);
    return null;
  }
}


export async function getOrderPaging(token: string, page: number, pageSize : number) {
  try {
     const res = await axios.get(`admin/order/getall?page=${page}&pageSize=${pageSize}`, {
        headers: { Authorization: `Bearer ${token}` },
      });

    return res.data
  } catch (err) {
    console.error("❌ Lỗi lấy dữ liệu từ server:", err);
    return null;
  }
}


export async function changeOrderStatus(token: string, orderId: string, newStatus: string) {
  try {
    await axios.post(
      `/admin/order/changestatus?newStatus=${newStatus}`,
      JSON.stringify(orderId), // body là chuỗi "orderId"
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );
  } catch (err) {
    console.error("❌ Lỗi khi đổi trạng thái đơn hàng:", err);
    throw err;
  }
}




export async function fetchCategoryAdmin(token: string) {
  try {
    const res = await axios.get('/admin/getallcategory', {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

   return res.data   
  } catch (err) {
    console.error("❌ Lỗi khi đổi trạng thái đơn hàng:", err);
    throw err;
  }
}

export async function addCategoryAdmin(token: string, newCategory:AddNewCategory) {
  try{
   await axios.post('/admin/addcategory', newCategory, {
  headers: {
    Authorization: `Bearer ${token}`,
    'Content-Type': 'application/json',
  },
});


  } catch (err) {
    console.error("❌ Lỗi khi đổi trạng thái đơn hàng:", err);
    throw err;
  }
}
export async function deleteCategoryAdmin(token: string, id:number) {
  try {
    await axios.delete('/admin/deletecategory', {
            headers: {
              Authorization: `Bearer ${token}`,
              'Content-Type': 'application/json',
            },
            data: id,
          });

  } catch (err) {
    console.error("❌ Lỗi khi đổi trạng thái đơn hàng:", err);
    throw err;
  }
}

export const getBooksPaging = async (page: number, pageSize: number) => {
  const res = await axios.get(`/Books/paging?page=${page}&pageSize=${pageSize}`);
  return res.data;
};

export async function createBook(data: any, token: string) {
  return axios.post(`/admin/book/createbook`, data, {
    headers: { Authorization: `Bearer ${token}` },
  });
}

export async function updateBook(bookId: string, data: any, token: string) {
  return axios.post(`/admin/book/updatebook?bookid=${bookId}`, data, {
    headers: { Authorization: `Bearer ${token}` },
  });
}




export async function deleteBook(token: string, bookId:string) {
  try {
    await axios.delete('/admin/book/deletebook', {
            headers: {
              Authorization: `Bearer ${token}`,
              'Content-Type': 'application/json',
            },
            data: bookId,
          });

  } catch (err) {
    console.error("❌ Lỗi khi đổi trạng thái đơn hàng:", err);
    throw err;
  }
}