'use client';
import "../../../css/style.css";

import React from "react";
import Navbar from "@/components/Admin/Navbar";
import Sidebar from "@/components/Admin/Sidebar";
import { useState } from "react";

export default function AdminLayout({ children }: { children: React.ReactNode }) {
  const [isSidebarCollapsed, setIsSidebarCollapsed] = useState(false);

  const toggleSidebar = () => setIsSidebarCollapsed(!isSidebarCollapsed);

  return (
    <>
      <Navbar />
      <div className="flex flex-grow overflow-hidden">
        <Sidebar isCollapsed={isSidebarCollapsed} toggleSidebar={toggleSidebar} />
        <main className={`flex-grow p-6 overflow-y-auto transition-all duration-300 "}`}>
          
          {children}
        </main>
      </div>
    </>
  );
}
