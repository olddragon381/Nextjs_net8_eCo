export interface Cart {
  id: string; 
  userId: string
  items: CartItemRequest[]
  appliedDiscount?: string;
  lastupdated: Date;
  totalPrice: number
}
export interface CartItemRequest{
 id: string,
 title: string,
 image? : string,
  price : number,
 quantity: number


}
