import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IGames } from 'src/app/shared/Models/games';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-game-detail',
  templateUrl: './game-detail.component.html',
  styleUrls: ['./game-detail.component.scss']
})
export class GameDetailComponent implements OnInit {

  game!: IGames;

  //ActivatedRoute gives us access to the route parameters
  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute){}

  ngOnInit(): void {
    this.getGame();
  }

  getGame(){
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')!).subscribe((response => {
      this.game = response
    }), error => {
      console.log(error);
    })
  }
}
