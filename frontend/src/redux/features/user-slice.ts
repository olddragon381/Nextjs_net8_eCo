import type { RootState } from "../store"; 
import { fetchCurrentUser } from "@/service/user/AuthServices";
import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";


interface UserState {
  user: {
    id: string;
    fullname: string;
    email: string;
    role: string;
  } | null;
  loading: boolean;
  error: string | null;
}

const token = () => localStorage.getItem("token");

// Thunk: lấy thông tin người dùng từ backend
export const getCurrentUser = createAsyncThunk("user/fetchCurrentUser", async (_, thunkAPI) => {
  try {
    const response = await fetchCurrentUser();
    
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue("Không lấy được thông tin người dùng");
  }
});

const initialState: UserState = {
  user: null,
  loading: false,
  error: null,

};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    setUser(state, action: PayloadAction<UserState["user"]>) {
      state.user = action.payload;
    },
    setLoading(state, action: PayloadAction<boolean>) {
      state.loading = action.payload;
    },
    setError(state, action: PayloadAction<string | null>) {
      state.error = action.payload;
    },
    logout(state) {
      state.user = null;
      state.loading = false;
      state.error = null;
      localStorage.removeItem("token");
    },
  },
});

export const { setUser, setLoading, setError, logout } = userSlice.actions;
export const selectCurrentUser = (state: RootState) => state.user.user;
export default userSlice.reducer;
