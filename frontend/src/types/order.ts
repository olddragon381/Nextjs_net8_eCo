export interface Order {

  
  userId: string
  namefororder : string;
  phone : string;
  address: string
  items: CartItem[]
  couponcode: string;
  totalamount: number
  paymentmethod: string;
note?: string
  
}
export enum OrderStatus {

  Pending,
Shipped,
Delivered,
Cancelled

}
export interface CartItem{
    bookid: string,
    price: number,
    quantity: number,
    total: number
}


