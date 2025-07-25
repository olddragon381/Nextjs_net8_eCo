"use client"

import React from "react";

import SidebarItem from "./SidebarItem";


const Sidebar = ({ isCollapsed, toggleSidebar }) => {
  const sidebarItems = [
    { href: "/admin/", label: "Dashboard" },
    { href: "/admin/user", label: "User Management" },
    { href: "/admin/order", label: "Order Management"},
    { href: "/admin/category", label: "Category Management" },
    { href: "/admin/book", label: "Book Management"},
    
  ];

  return (
    <div className={`${isCollapsed ? "w-14" : "w-64"} h-full bg-blue-light-5 transition-all duration-300`}>
      <button onClick={toggleSidebar} className="absolute -right-2.5 top-0 w-6 h-6 flex justify-center items-center hover:bg-gray-1 rounded-full transition cursor-pointer">
    
      </button>
      <div className="mt-4">
        <ul>
          {sidebarItems.map((item) => (
            <SidebarItem key={item.href} href={item.href} label={item.label} isCollapsed={isCollapsed}  />
          ))}
        </ul>
      </div>
    </div>
  );
};

export default Sidebar;