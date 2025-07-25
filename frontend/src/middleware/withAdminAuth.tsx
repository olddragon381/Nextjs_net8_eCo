'use client';

import { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { useRouter } from 'next/navigation';

export default function withAdminAuth(Component: React.ComponentType) {
  return function AuthWrapper(props: any) {
    const router = useRouter();

    // 👇 role lấy từ redux
     const role = useSelector((state: any) => state.user.user?.role);
    const [checked, setChecked] = useState(false);

    useEffect(() => {
      if (role === '') return; // Đợi redux load
      if (role !== 'Admin') {
        router.replace('/'); // Redirect nếu không phải admin
      } else {
        setChecked(true); // Cho render component
      }
    }, [role]);

    if (!checked) {
      return <div>Đang kiểm tra quyền truy cập...</div>; // Spinner hoặc thông báo
    }

    return <Component {...props} />;
  };
}
