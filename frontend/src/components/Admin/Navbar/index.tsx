// Navbar.tsx
'use client';

import React from "react";
import { useState } from "react";
import { useSelector } from "react-redux";
import { selectCurrentUser } from "@/redux/features/user-slice";
import { useRouter } from "next/navigation";

const Navbar = () => {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const toggleDropdown = () => setIsDropdownOpen(!isDropdownOpen);

  const router = useRouter();
  const user = useSelector(selectCurrentUser);

  const handleLogout = () => {
    localStorage.removeItem("token");
    router.push("/signin");
  };

  return (
    <>
    <div className="w-full bg-gray-900 text-black shadow p-4 sticky top-0 z-10">
<nav className="flex justify-between items-center">
        <div className="text-2xl font-bold">Admin Panel</div>
        <div className="flex items-center space-x-6 relative">
          <button onClick={toggleDropdown} className="flex items-center space-x-2 focus:outline-none">
            <span className="hidden md:block font-medium">{user?.fullname ?? 'Admin'}</span>
            <span className="material-icons">expand_more</span>
          </button>
          {isDropdownOpen && (
            <div className="absolute right-0 top-10 w-48 bg-white text-black shadow-md rounded-md z-50">
              <ul className="py-2">
                
                <li onClick={handleLogout} className="px-4 py-2 hover:bg-gray-100 cursor-pointer">Logout</li>
              </ul>
            </div>
          )}
        </div>
      </nav>
    </div>
    
    </>

  );
};

export default Navbar;
