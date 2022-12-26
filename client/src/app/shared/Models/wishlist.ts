import { v4 as uuidv4 } from 'uuid';

export interface IWishlistItem {
  id: number;
  gameName: string;
  price: number;
  pictureUrl: string;
  genre: string;
  device: string;
}

export interface IWishlist {
  id: string;
  items: IWishlistItem[];
}

export class Wishlist implements IWishlist{
  id = uuidv4();
  items: IWishlistItem[] = [];

}
