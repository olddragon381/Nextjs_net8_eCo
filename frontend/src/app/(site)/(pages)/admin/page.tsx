'use client';

import Dashboard from "@/components/Admin/dashboard";
import withAdminAuth from "@/middleware/withAdminAuth";
import React from "react";

function AdminDashboard() {
  return (
   <Dashboard></Dashboard>
  )
}

export default AdminDashboard
// export default withAdminAuth(AdminDashboard)