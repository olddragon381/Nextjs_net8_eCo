// redux/slices/checkoutSlice.ts
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "../store";


type CheckoutState = {
  address: string;
  paymentMethod: string;
  namefororder: string;
  note?: string;
  phone: string
  coupon: string
};

const initialState: CheckoutState = {
  address: "",
  paymentMethod: "cash",
  namefororder: "",
  note: "",
  phone: "",
  coupon: ""
};

export const checkout = createSlice({
  name: "checkout",
  initialState,
  reducers: {
    setAddress: (state, action: PayloadAction<string>) => {
      state.address = action.payload;
    },

    setNameForOrder: (state, action: PayloadAction<string>) => {
      state.namefororder = action.payload;
    },
    setPaymentMethod: (state, action: PayloadAction<string>) => {
      state.paymentMethod = action.payload;
    },
    setNote: (state, action: PayloadAction<string>) => {
      state.note = action.payload;
    },
    setPhone: (state, action: PayloadAction<string>) => {
      state.phone = action.payload;
    },
    setCoupon:(state, action: PayloadAction<string>) => {
      state.coupon = action.payload;
    },

    resetCheckout: (state) => {
     
      state.paymentMethod = "";
      state.note = "";

      state.coupon = "";
    },
  },
});

export const {
  setAddress,
  setPaymentMethod,
  setNote,
  setPhone,
  setCoupon,
  resetCheckout,
  setNameForOrder
 
} = checkout.actions;

export const selectCheckout = (state: RootState ) => state.checkout;

export default checkout.reducer;
