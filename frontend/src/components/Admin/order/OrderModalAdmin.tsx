import React from "react";

const OrderModalAdmin = ({
  isOpen,
  onClose,
  order,
}: {
  isOpen: boolean;
  onClose: () => void;
  order: any;
}) => {
  if (!isOpen || !order) return null;

  return (
    <div className="fixed inset-0 z-50 bg-black bg-opacity-50 flex items-center justify-center">
      <div className="bg-white p-6 rounded-md w-full max-w-xl shadow-lg relative">
        <button
          className="absolute top-2 right-2 text-gray-600 hover:text-black"
          onClick={onClose}
        >
          ✕
        </button>
        <h2 className="text-xl font-semibold mb-4">Chi tiết đơn hàng</h2>

        <div className="space-y-2 text-sm">
          <p>
            <span className="font-semibold">Mã đơn:</span> #{order.id.slice(-8)}
          </p>
          <p>
            <span className="font-semibold">Email:</span> {order.email}
          </p>
          <p>
            <span className="font-semibold">Ngày tạo:</span>{" "}
            {new Date(order.createdAt).toLocaleString()}
          </p>
          <p>
            <span className="font-semibold">Trạng thái:</span>{" "}
            {order.status}
          </p>
          <p>
            <span className="font-semibold">Phương thức thanh toán:</span>{" "}
            {order.paymentMethod}
          </p>
          <p>
            <span className="font-semibold">Tổng tiền:</span>{" "}
            {order.totalAmount.toLocaleString() +".000"} VND
          </p>
          <p>
            <span className="font-semibold">Ghi chú:</span> {order.note || "Không có"}
          </p>

          <div className="mt-4">
            <h3 className="font-semibold mb-2">Danh sách sản phẩm:</h3>
            <ul className="list-disc list-inside space-y-1">
              {order.items.map((item: any, index: number) => (
                <li key={index}>
                  {item.bookId}  {item.price+".000d"}x {item.quantity} -{" "}
                  {(item.price * item.quantity).toLocaleString() +".000"} VND
                </li>
              ))}
            </ul>
          </div>
        </div>

        <div className="text-right mt-6">
          <button
            onClick={onClose}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
          >
            Đóng
          </button>
        </div>
      </div>
    </div>
  );
};

export default OrderModalAdmin;
