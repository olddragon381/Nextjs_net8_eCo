'use client';
import React from "react";

import Link from 'next/link';
import { usePathname } from 'next/navigation'; // dùng thay useLocation

const SidebarItem = ({ href, label, isCollapsed }) => {
  const pathname = usePathname();
  const isActive = pathname === href;

  return (
    <li className="group">
      <Link href={href} className={`flex items-center py-2.5 px-4 hover:bg-gray-700 transition ${isActive ? "bg-gray-700" : ""}`}>
      
        <span className={`text-black ml-4 ${isCollapsed ? "hidden" : "block"}`}>{label}</span>
      </Link>
    </li>
  );
};

export default SidebarItem;
