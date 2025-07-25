'use client';

import React, { useEffect, useState } from 'react';
import { showToast } from '@/components/Common/ShowToaster';
import OrderModal from '@/components/Orders/OrderModal';
import { changeOrderStatus, getOrderPaging } from '@/service/Admin/AdminService';
import OrderModalAdmin from './OrderModalAdmin';

const ORDER_STATUSES = ['Pending', 'Shipped', 'Delivered', 'Cancelled'];

const AdminOrderPage = () => {
  const [orders, setOrders] = useState<any[]>([]);
  const [selectedOrder, setSelectedOrder] = useState<any | null>(null);
  const [showModal, setShowModal] = useState(false);
  const [page, setPage] = useState(1);
  const pageSize = 10;

  const token = typeof window !== 'undefined' ? localStorage.getItem('token') : null;

  const fetchOrders = async () => {
    try {
      const res = await getOrderPaging(token, page, pageSize);
      console.log("paging order",res)
      setOrders(res.items  || []);
    } catch (err) {
      showToast('Lỗi khi lấy đơn hàng', 'warning');
    }
  };

  const handleChangeStatus = async (orderId: string, newStatus: string) => {
    try {
      await changeOrderStatus(token, orderId, newStatus);
      showToast('Đã cập nhật trạng thái đơn hàng', 'success');
      fetchOrders();
    } catch {
      showToast('Lỗi khi cập nhật trạng thái', 'warning');
    }
  };

  const handleOpenModal = (order: any) => {
    setSelectedOrder(order);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedOrder(null);
  };

  useEffect(() => {
    fetchOrders();
  }, [page]);

  return (
    <div className="p-6">
      <h1 className="text-xl font-bold mb-4">Quản lý đơn hàng</h1>
      <div className="overflow-x-auto">
        <table className="min-w-full border rounded shadow text-sm">
          <thead className="bg-gray-100 text-gray-700 font-semibold">
            <tr>
              <th className="px-4 py-2 text-left">Mã khách hàng</th>
              <th className="px-4 py-2 text-left">Mã đơn</th>
              <th className="px-4 py-2 text-left">Ngày tạo</th>
              <th className="px-4 py-2 text-left">Giá</th>
              <th className="px-4 py-2 text-left">Thao tác</th>
            </tr>
          </thead>
          <tbody>
            {orders.map((order) => (
              <tr key={order.id} className="border-t">
                <td className="px-4 py-2">{order.userId}</td>
                <td
                  className="px-4 py-2 text-blue-600 cursor-pointer hover:underline"
                  onClick={() => handleOpenModal(order)}
                >
                  #{order.id.slice(-8)}
                </td>
                <td className="px-4 py-2">
                  {new Date(order.createdAt).toLocaleString()}
                </td>
                <td className="px-4 py-2 capitalize">{order.totalAmount +".000d"}</td>
                <td className="px-4 py-2">
                  <select
                    className="border border-gray-300 rounded px-2 py-1 text-sm"
                    value={order.status}
                    onChange={(e) =>
                      handleChangeStatus(order.id, e.target.value)
                    }
                  >
                    {ORDER_STATUSES.map((status) => (
                      <option key={status} value={status}>
                        {status}
                      </option>
                    ))}
                  </select>
                </td>
              </tr>
            ))}
            {orders.length === 0 && (
              <tr>
                <td colSpan={5} className="text-center py-4 text-gray-500">
                  Không có đơn hàng.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      {/* Pagination */}
      <div className="mt-4 flex justify-end gap-2">
        <button
          onClick={() => setPage((prev) => Math.max(prev - 1, 1))}
          disabled={page === 1}
          className="px-3 py-1 bg-gray-200 rounded disabled:opacity-50"
        >
          Trước
        </button>
        <button
          onClick={() => setPage((prev) => prev + 1)}
          className="px-3 py-1 bg-gray-200 rounded"
        >
          Sau
        </button>
      </div>

      {/* Modal chi tiết */}
      {selectedOrder && (
        <OrderModalAdmin
          isOpen={showModal}
          onClose={handleCloseModal}
          order={selectedOrder}
        />
      )}
    </div>
  );
};

export default AdminOrderPage;
