export interface Book {
  id: string;
  title: string;
  image: string;
  authors: string;
  description: string;
  rating: number;
  ratingCount: number;
  reviewCount: number;
  genres: string[];
  price: number;
  status: number;
  createAt: Date;
  numpage: number;
}


