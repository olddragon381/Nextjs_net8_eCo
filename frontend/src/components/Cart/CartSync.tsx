// src/components/AppInitializer.tsx
"use client";
import { useEffect, useRef } from "react";
import {  useSelector } from "react-redux";
import { getCurrentUser, selectCurrentUser } from "@/redux/features/user-slice";

import CartSyncWrapper from "@/app/context/CartSyncWrapper";
import CartSyncOnLogin from "@/components/Cart/CartSyncOnLogin";

export default function CartSync() {
  
  const user = useSelector(selectCurrentUser);
  return (
    <>
      {/* Chỉ sync nếu đã có user */}
      {user && (
        <>
          <CartSyncWrapper userIsLoggedIn={!!user} />
          <CartSyncOnLogin userIsLoggedIn={!!user} />
        </>
      )}
    </>
  );
}
