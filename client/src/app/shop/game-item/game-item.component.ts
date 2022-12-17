import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IGames } from 'src/app/shared/Models/games';

@Component({
  selector: 'app-game-item',
  templateUrl: './game-item.component.html',
  styleUrls: ['./game-item.component.scss']
})
export class GameItemComponent implements OnInit{

  @Input() game!: IGames;

constructor (private basketService:BasketService) {}

ngOnInit(): void {

}

addItemToBasket(){
  this.basketService.addItemToBasket(this.game)
}

}
