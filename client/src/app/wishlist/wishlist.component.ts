
import { Component, OnInit } from '@angular/core';
import { map, Observable, pipe } from 'rxjs';
import { BasketService } from '../basket/basket.service';
import { IGames } from '../shared/Models/games';
import { IWishlist, IWishlistItem } from '../shared/Models/wishlist';
import { ShopService } from '../shop/shop.service';
import { WishlistService } from './wishlist.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit {

  wishlist$: Observable<IWishlist>

  game: IGames;

  constructor (private wishlistService: WishlistService, private shopService: ShopService,
    private basketService: BasketService) {}


  ngOnInit(): void {
    this.wishlist$ = this.wishlistService.wishlist$;
  }

  removeWishlistItem(item: IWishlistItem){
    this.wishlistService.removeItemsFromWishList(item);
  }

  addToCart(id: number){

    this.shopService.getProduct(id).subscribe((games) => {
      this.basketService.addItemToBasket(games);
    }, error => {
      console.log(error);
    })



  }

}
