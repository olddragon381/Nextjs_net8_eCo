"use client";

import { store } from "./store";
import { Provider, useDispatch, useSelector } from "react-redux";
import React, { useEffect } from "react";




export function ReduxProvider({ children }: { children: React.ReactNode }) {
 
  return (
    <Provider store={store}>
      
      {children}
    </Provider>
  );
}
