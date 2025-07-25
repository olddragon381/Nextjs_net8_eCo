import { createSlice } from "@reduxjs/toolkit";
import { Book } from "@/types/product";

type InitialState = {
  value: Book;
};

const initialState = {
  value: {

  id: "",
  title: "",
  image: "",
  authors: "",
  description: "",
  rating: 0,
  ratingCount: 0,
  reviewCount: 0,
  genres: [],
  price: 0,
  numpage: 0,
  } as Book
  
} as InitialState;

export const productDetails = createSlice({
  name: "productDetails",
  initialState,
  reducers: {
    updateproductDetails: (_, action) => {
      return {
        value: {
          ...action.payload,
        },
      };
    },
  },
});

export const { updateproductDetails } = productDetails.actions;
export default productDetails.reducer;
