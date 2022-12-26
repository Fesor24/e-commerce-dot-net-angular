
import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';
import { WishlistService } from './wishlist/wishlist.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'client';


  constructor(private basketService: BasketService, private accountService: AccountService,
    private wishlistService: WishlistService) {}


  ngOnInit(): void {

    this.loadBasket();

    this.loadCurrentUser();

    this.loadCurrentWishlist();
  }

  loadBasket(){
     //Checking our local storage if we have any basketId
     const basketId = localStorage.getItem('basket_id');

     //If the basketId is not null, we fetch the basket from the redis server
     if(basketId){
       this.basketService.getBasket(basketId).subscribe(() => {
         console.log('initialised basket');
       }, error => {
         console.log(error)
       })
     }
  }

  loadCurrentUser(){
    const token = localStorage.getItem('token');


      this.accountService.loadCurrentUser(token).subscribe(() => {
        console.log('user loaded')
      }, error => {
        console.log(error)
      })
    }

    loadCurrentWishlist(){
      const wishlistId = localStorage.getItem('wishlist_id');

      this.wishlistService.getWishlist(wishlistId).subscribe(() => {
        console.log('wishlist initialised')
      }, error => {
        console.log(error);
      })
    }




  }



