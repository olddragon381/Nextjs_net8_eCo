export interface Category {
  id: string; 
  name: string;
  description?: string;
  image: string;
}

export interface AddNewCategory {

  CategoryName: string;
  Description?: string;
  Image?: string;
}

