import { createSlice, PayloadAction, createSelector } from "@reduxjs/toolkit";
import { RootState } from "../store";

// Kiểu dữ liệu cho từng sản phẩm trong giỏ hàng
export type CartItem = {
  id: string;
  title: string;
  price: number;
  quantity: number;
  image: string;
};

// Kiểu dữ liệu cho state của giỏ hàng
interface CartState {
  items: CartItem[];
}

// State ban đầu
const initialState: CartState = {
  items: [],
};

// Slice chính
export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    // Thêm sản phẩm vào giỏ
    addItemToCart: (state, action: PayloadAction<CartItem>) => {
      const { id, title, price, quantity, image } = action.payload;
      const existingItem = state.items.find((item) => item.id === id);

      if (existingItem) {
        existingItem.quantity += quantity;
      } else {
        state.items.push({ id, title, price, quantity, image });
      }
    },

    // Xóa sản phẩm khỏi giỏ
    removeItemFromCart: (state, action: PayloadAction<string>) => {
      const itemId = action.payload;
      state.items = state.items.filter((item) => item.id !== itemId);
    },

    // Cập nhật số lượng
    updateCartItemQuantity: (
      state,
      action: PayloadAction<{ id: string; quantity: number }>
    ) => {
      const { id, quantity } = action.payload;
      const item = state.items.find((item) => item.id === id);
      if (item) {
        item.quantity = quantity;
      }
    },

    // Xóa toàn bộ sản phẩm trong giỏ
    removeAllItemsFromCart: (state) => {
      state.items = [];
    },

    // Gán danh sách item từ server vào Redux
    setCartItems: (state, action: PayloadAction<CartItem[]>) => {
      state.items = action.payload;
    },
  },
});

// Selector để lấy danh sách item trong giỏ
export const selectCartItems = (state: RootState) => state.cartReducer.items;

// Selector tính tổng giá trị đơn hàng
export const selectTotalPrice = createSelector([selectCartItems], (items) =>
  items.reduce((total, item) => total + item.price * item.quantity, 0)
);

// Export các actions
export const {
  addItemToCart,
  removeItemFromCart,
  updateCartItemQuantity,
  removeAllItemsFromCart,
  setCartItems,
} = cartSlice.actions;

// Export reducer
export default cartSlice.reducer;
