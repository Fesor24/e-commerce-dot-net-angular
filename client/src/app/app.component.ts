
import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'client';


  constructor(private basketService: BasketService) {}


  ngOnInit(): void {
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


}
