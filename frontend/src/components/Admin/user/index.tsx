'use client';

import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { showToast } from '@/components/Common/ShowToaster';
import { changeRole, deleUser, getPaging } from '@/service/Admin/AdminService';

interface User {
  id: string;
  fullName: string;
  email: string;
  userRole: number;
  createdAt: string;
  
}
const roles = ['User', 'Admin', 'SuperAdmin'];

const AdminUserPage = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [page, setPage] = useState(1);
  const pageSize = 10;
  const token = typeof window !== 'undefined' ? localStorage.getItem('token') : null;

  const fetchUsers = async () => {
    try {
      const res = await getPaging(token,page,pageSize)
      console.log("paging",res);
      setUsers(res.items); 
    } catch (err) {
      showToast('Lỗi khi tải danh sách người dùng', 'warning');
    }
  };

  const handleDelete = async (userId: string) => {
    try {
      await deleUser(token, userId);
      showToast('Xoá người dùng thành công', 'success');
      fetchUsers();
    } catch (err) {
      showToast('Xoá thất bại', 'warning');
    }
  };

  const handleRoleChange = async (userId: string, newRole: number) => {
    try {
      await changeRole(token, userId, newRole)
      showToast('Cập nhật vai trò thành công', 'success');
      fetchUsers();
    } catch (err) {
      showToast('Lỗi cập nhật vai trò', 'warning');
    }
  };

  useEffect(() => {
    fetchUsers();
  }, [page]);

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-6">Quản lý người dùng</h1>
      <div className="overflow-x-auto bg-white rounded-lg shadow">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50 text-left text-sm font-semibold text-gray-700">
            <tr>
              <th className="px-6 py-4">Tên</th>
              <th className="px-6 py-4">Email</th>
              <th className="px-6 py-4">Vai trò</th>
              <th className="px-6 py-4">Thao tác</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-200 text-sm">
  {users.map((user) => (
    <tr key={user.id}>
      <td className="px-6 py-4">{user.fullName}</td>
      <td className="px-6 py-4">{user.email}</td>
      <td className="px-6 py-4">
        <select
          value={user.userRole}
          onChange={(e) => handleRoleChange(user.id, parseInt(e.target.value))}
          className="rounded border px-2 py-1"
        >
          <option value={0}>User</option>
          <option value={1}>Admin</option>
          <option value={2}>Moderator</option>
        </select>
      </td>
      <td className="px-6 py-4">
        <button
          onClick={() => handleDelete(user.id)}
          className="text-red-500 hover:text-red-700 font-medium"
        >
          Xoá
        </button>
      </td>
    </tr>
  ))}
</tbody>
        </table>
      </div>

      {/* Phân trang nếu cần */}
      <div className="flex justify-end mt-4">
        <button
          onClick={() => setPage((prev) => Math.max(prev - 1, 1))}
          disabled={page === 1}
          className="mr-2 px-4 py-2 bg-gray-200 rounded"
        >
          Trước
        </button>
        <button
          onClick={() => setPage((prev) => prev + 1)}
          className="px-4 py-2 bg-gray-200 rounded"
        >
          Sau
        </button>
      </div>
    </div>
  );
};

export default AdminUserPage;
