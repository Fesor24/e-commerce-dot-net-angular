import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { GameItemComponent } from './game-item/game-item.component';
import { SharedModule } from '../shared/shared.module';
import { GameDetailComponent } from './game-detail/game-detail.component';
import { ShopRoutingModule } from './shop-routing.module';



@NgModule({
  declarations: [
    ShopComponent,
    GameItemComponent,
    GameDetailComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule
  ]
})
export class ShopModule { }
