// store/orderSlice.ts
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

type CartItem = {
  bookid: string;
  quantity: number;
  price: number;
  total: number;
};

type Order = {
  id?: string; // nếu có response từ backend
   userId: string
 
    phone : string;

    couponcode: string;
    totalamount: number
  items: CartItem[];
  address: string;
  namefororder: string;
  paymentmethod: string;
  note?: string;
 
  createdAt?: string;
};

type OrderState = {
  currentOrder: Order | null;
  orderHistory: Order[];
};

const initialState: OrderState = {
  currentOrder: null,
  orderHistory: [],
};

const orderSlice = createSlice({
  name: "order",
  initialState,
  reducers: {
    createOrder: (state, action: PayloadAction<Order>) => {
      state.currentOrder = action.payload;
    },
    addToOrderHistory: (state, action: PayloadAction<Order>) => {
      state.orderHistory.push(action.payload);
    },
    clearCurrentOrder: (state) => {
      state.currentOrder = null;
    },
  },
});

export const { createOrder, addToOrderHistory, clearCurrentOrder } = orderSlice.actions;
export default orderSlice.reducer;
