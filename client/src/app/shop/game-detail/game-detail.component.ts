import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IGames } from 'src/app/shared/Models/games';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-game-detail',
  templateUrl: './game-detail.component.html',
  styleUrls: ['./game-detail.component.scss']
})
export class GameDetailComponent implements OnInit {

  game!: IGames;

  quantity = 1;

  //ActivatedRoute gives us access to the route parameters
  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute,
    private basketService: BasketService){}

  ngOnInit(): void {
    this.getGame();
  }

addItemToBasket(){
this.basketService.addItemToBasket(this.game, this.quantity)
}

incrementQuantity(){
  this.quantity++;
}

decrementQuantity(){
  if(this.quantity > 1){
    this.quantity--;
  }

}

  getGame(){
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')!).subscribe((response => {
      this.game = response
    }), error => {
      console.log(error);
    })
  }
}
