'use client';

import { getCount } from '@/service/Admin/AdminService';
import React, { useEffect, useState } from 'react';


const Dashboard = () => {
  const [counts, setCounts] = useState({
    user: 0,
    book: 0,
    order: 0,
    category: 0,
  });

  useEffect(() => {
    const fetchData = async () => {
      const token = localStorage.getItem('token'); // hoặc lấy từ Redux/AuthContext
      if (!token) return;

      const result = await getCount(token);
      if (result) setCounts(result);
    };

    fetchData();
  }, []);

  const stats = [
    { title: 'Books', count: counts.book, color: 'bg-blue-500', icon: '📚' },
    { title: 'Users', count: counts.user, color: 'bg-green-500', icon: '👤' },
    { title: 'Orders', count: counts.order, color: 'bg-yellow-500', icon: '🛒' },
    { title: 'Categories', count: counts.category, color: 'bg-purple-500', icon: '📂' },
  ];

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-6">Dashboard Overview</h1>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
        {stats.map((item, index) => (
          <div
            key={index}
            className={`rounded-2xl p-5 shadow-md text-black ${item.color} flex items-center justify-between`}
          >
            <div>
              <div className="text-sm uppercase opacity-75">{item.title}</div>
              <div className="text-3xl font-bold">{item.count}</div>
            </div>
            <div className="text-4xl">{item.icon}</div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Dashboard;
