'use client';

import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { showToast } from '@/components/Common/ShowToaster';
import { addCategoryAdmin, deleteCategoryAdmin, fetchCategoryAdmin } from '@/service/Admin/AdminService';

const AdminCategoryPage = () => {
  const [categories, setCategories] = useState<any[]>([]);
  const [newCategory, setNewCategory] = useState({
    CategoryName: '',
    Description: '',
    Image: '',
  });

  const token =
    typeof window !== 'undefined' ? localStorage.getItem('token') : null;

  const fetchCategories = async () => {
    try {
      const res = await fetchCategoryAdmin(token)
      setCategories(res || []);
    } catch (err) {
      showToast('Lỗi khi tải danh sách thể loại', 'warning');
    }
  };

  const handleAddCategory = async () => {
    if (!newCategory.CategoryName.trim()) {
      showToast('Tên thể loại không được để trống', 'warning');
      return;
    }

    try {

        await addCategoryAdmin(token, newCategory);
    

      showToast('Thêm thể loại thành công', 'success');
      setNewCategory({ CategoryName: '', Description: '', Image: '' });
      fetchCategories();
    } catch (err) {
      showToast('Lỗi khi thêm thể loại', 'warning');
    }
  };

  const handleDeleteCategory = async (id: number) => {
    try {
      await deleteCategoryAdmin(token,id);
      showToast('Xoá thể loại thành công', 'success');
      fetchCategories();
    } catch (err) {
      showToast('Lỗi khi xoá thể loại', 'warning');
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  return (
    <div className="p-6">
      <h1 className="text-xl font-bold mb-4">Quản lý thể loại</h1>

      {/* Form thêm thể loại */}
      <form onSubmit={handleAddCategory} className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-6">
        <input
          type="text"
          value={newCategory.CategoryName}
          onChange={(e) =>
            setNewCategory({ ...newCategory, CategoryName: e.target.value })
          }
          placeholder="Tên thể loại"
          className="border px-4 py-2 rounded"
        />
        <input
          type="text"
          value={newCategory.Description}
          onChange={(e) =>
            setNewCategory({ ...newCategory, Description: e.target.value })
          }
          placeholder="Mô tả"
          className="border px-4 py-2 rounded"
        />
        <input
          type="text"
          value={newCategory.Image}
          onChange={(e) =>
            setNewCategory({ ...newCategory, Image: e.target.value })
          }
          placeholder="Link ảnh"
          className="border px-4 py-2 rounded"
        />
        <button
          type="submit"
          className="bg-blue text-white px-4 py-2 rounded hover:bg-blue-600 md:col-span-3"
        >
          Thêm thể loại
        </button>
      </form>

      {/* Danh sách category */}
      <div className="overflow-x-auto">
        <table className="w-full border rounded shadow text-sm">
          <thead className="bg-gray-100 text-gray-700 font-semibold">
            <tr>
              <th className="px-4 py-2">#</th>
              <th className="px-4 py-2">Ảnh</th>
              <th className="px-4 py-2">Tên</th>
              <th className="px-4 py-2">Mô tả</th>
              <th className="px-4 py-2">Thao tác</th>
            </tr>
          </thead>
          <tbody>
            {categories.map((cat, idx) => (
              <tr key={cat.id} className="border-t">
                <td className="px-2 py-2">{cat.id}</td>
                <td className="px-4 py-2">
                  <img
                    src={cat.image}
                    alt={cat.name}
                    className="w-12 h-12 object-cover rounded"
                  />
                </td>
                <td className="px-4 py-2">{cat.name}</td>
                <td className="px-4 py-2">{cat.description || 'Không có'}</td>
                <td className="px-4 py-2">
                  <button
                    onClick={() => handleDeleteCategory(cat.id)}
                    className="text-red-600 hover:underline"
                  >
                    Xoá
                  </button>
                </td>
              </tr>
            ))}
            {categories.length === 0 && (
              <tr>
                <td colSpan={5} className="text-center py-4 text-gray-500">
                  Không có thể loại nào.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default AdminCategoryPage;
