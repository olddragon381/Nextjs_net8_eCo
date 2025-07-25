'use client';
import React, { useState } from 'react';
import { createBook } from '@/service/Admin/AdminService';
import { showToast } from '@/components/Common/ShowToaster';

const BookCreateModal = ({ onClose, onSave }: any) => {
  const [formData, setFormData] = useState({
    title: '',
    image: '',
    authors: '',
    description: '',
    genres: '',
    numPages: 0,
    price: 0,
    status: 0,
  });

  const token = typeof window !== 'undefined' ? localStorage.getItem('token') : null;

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    try {
      await createBook(
        {
          ...formData,
          genres: formData.genres.split(',').map((g) => g.trim()),
          numPages: Number(formData.numPages),
          price: Number(formData.price),
          rating: 0,
          ratingCount: 0,
          reviewCount: 0,
          status: Number(formData.status),
        },
        token
      );
      showToast('Thêm sách thành công', 'success');
      onSave();
    } catch (err) {
      showToast('Lỗi khi thêm sách', 'warning');
    }
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-30 z-50 flex items-center justify-center">
      <div className="bg- rounded p-6 w-full max-w-2xl shadow-lg">
        <h2 className="text-xl font-bold mb-4">Thêm sách mới</h2>
        <form onSubmit={handleSubmit} className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-semibold mb-1">Tiêu đề</label>
            <input className="border p-2 w-full" value={formData.title} onChange={(e) => setFormData({ ...formData, title: e.target.value })} />
          </div>

          <div>
            <label className="block text-sm font-semibold mb-1">Tác giả</label>
            <input className="border p-2 w-full" value={formData.authors} onChange={(e) => setFormData({ ...formData, authors: e.target.value })} />
          </div>

          <div>
            <label className="block text-sm font-semibold mb-1">Link ảnh</label>
            <input className="border p-2 w-full" value={formData.image} onChange={(e) => setFormData({ ...formData, image: e.target.value })} />
          </div>

          <div>
            <label className="block text-sm font-semibold mb-1">Giá (VNĐ)</label>
            <input type="number" className="border p-2 w-full" value={formData.price} onChange={(e) => setFormData({ ...formData, price: Number(e.target.value) })} />
          </div>

          <div>
            <label className="block text-sm font-semibold mb-1">Số trang</label>
            <input type="number" className="border p-2 w-full" value={formData.numPages} onChange={(e) => setFormData({ ...formData, numPages: Number(e.target.value) })} />
          </div>

          <div>
            <label className="block text-sm font-semibold mb-1">Thể loại (phân cách bởi dấu phẩy)</label>
            <input className="border p-2 w-full" value={formData.genres} onChange={(e) => setFormData({ ...formData, genres: e.target.value })} />
          </div>

          <div className="col-span-2">
            <label className="block text-sm font-semibold mb-1">Mô tả</label>
            <textarea className="border p-2 w-full" rows={4} value={formData.description} onChange={(e) => setFormData({ ...formData, description: e.target.value })} />
          </div>

          <div className="col-span-2">
            <label className="block text-sm font-semibold mb-1">Trạng thái</label>
            <select className="border p-2 w-full" value={formData.status} onChange={(e) => setFormData({ ...formData, status: Number(e.target.value) })}>
              <option value={0}>InStock</option>
              <option value={1}>OutOfStock</option>
              <option value={2}>ComingSoon</option>
              <option value={3}>OnSale</option>
              <option value={4}>Unavailable</option>
            </select>
          </div>

          <div className="col-span-2 flex justify-end gap-2 mt-4">
            <button type="button" onClick={onClose} className="px-4 py-2 bg-gray-300 rounded">Hủy</button>
            <button type="submit" className="px-4 py-2 bg-blue text-white rounded">Thêm</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default BookCreateModal;
