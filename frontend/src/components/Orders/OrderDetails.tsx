import React from "react";
import Image from "next/image";

const OrderDetails = ({ orderItem }: any) => {
  return (
   <div className="p-6 bg-white shadow-lg space-y-5 w-full mx-auto">
  {/* Địa chỉ và thông tin thanh toán */}
  <div className="space-y-2 text-sm text-dark">
    <p className="flex items-center gap-2">
      <span className="text-pink-500">📍</span>
      <span><strong>Địa chỉ:</strong> {orderItem.address}</span>
    </p>
    <p className="flex items-center gap-2">
      <span className="text-pink-500">📞</span>
      <span><strong>Số điện thoại:</strong> {orderItem.phone}</span>
    </p>
    <p className="flex items-center gap-2">
      <span className="text-blue-500">💳</span>
      <span><strong>Thanh toán:</strong> {orderItem.paymentMethod}</span>
    </p>
  </div>

  {/* Danh sách sản phẩm */}
  <div className="space-y-3">
    <h3 className="font-semibold text-base flex items-center gap-2">
      🛒 Sản phẩm:
    </h3>

    {orderItem.items.map((item, index) => (
      <div
        key={index}
        className="flex items-center gap-4 border border-gray-200 rounded-lg p-4 bg-gray-50"
      >
        <div className="w-20 h-20 bg-white rounded overflow-hidden border">
          <Image
            src={item.image || "/images/placeholder.png"}
            alt={item.title}
            width={80}
            height={80}
            className="object-cover w-full h-full"
          />
        </div>
        <div className="flex flex-col gap-1 text-sm">
          <h4 className="font-semibold text-dark">{item.title}</h4>
          <p className="text-gray-600">
            Giá: <span className="text-dark">{item.price.toLocaleString("vi-VN")}₫</span>
          </p>
          <p className="text-gray-600">
            Số lượng: <span className="text-dark">{item.quantity}</span>
          </p>
        </div>
      </div>
    ))}
  </div>
</div>
  );
};

export default OrderDetails;
