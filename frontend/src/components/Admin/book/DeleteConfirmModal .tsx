"use client";

import React, { useEffect, useState } from "react";

const DeleteConfirmModal = ({ isOpen, onClose, onConfirm, bookTitle, bookid }) => {
  if (!isOpen) return null;


  return (
    <div className="fixed inset-0 bg-black bg-opacity-40 z-50 flex items-center justify-center">
      <div className="bg-white p-6 rounded shadow-md w-[90%] max-w-md">
        <h3 className="text-lg font-semibold mb-4">Xác nhận xoá</h3>
        <p>Bạn có chắc chắn muốn xoá sách <strong>{bookTitle}</strong>?</p>
        <div className="flex justify-end gap-2 mt-6">
          <button onClick={onClose} className="bg-gray px-4 py-2 rounded">Huỷ</button>
          <button onClick={onConfirm} className="bg-red text-white px-4 py-2 rounded">Xoá</button>
        </div>
      </div>
    </div>
  );
};
export default DeleteConfirmModal;
