import { createSlice, PayloadAction } from "@reduxjs/toolkit";
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
  numpage: 0

  } as Book,
} as InitialState;

export const quickView = createSlice({
  name: "quickView",
  initialState,
  reducers: {
    updateQuickView: (_, action) => {
      return {
        value: {
          ...action.payload,
        },
      };
    },

    resetQuickView: () => {
      return {
        value: initialState.value,
      };
    },
  },
});

export const { updateQuickView, resetQuickView } = quickView.actions;
export default quickView.reducer;
