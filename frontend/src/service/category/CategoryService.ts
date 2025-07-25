import axios  from '@/app/utils/axios';
// services/categoryService.ts

import { Category } from "@/types/category";


export async function fetch7Categories(): Promise<Category[]> {
  const response = await axios.get<Category[]>(`/Category/get7category`);
  return response.data;
}

export async function fetchAllCategories(): Promise<Category[]> {
  const response = await axios.get<Category[]>(`/Category/getallcategory`);
  return response.data;
}
