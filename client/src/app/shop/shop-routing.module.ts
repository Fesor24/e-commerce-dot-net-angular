import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop.component';
import { GameDetailComponent } from './game-detail/game-detail.component';

const routes: Routes = [
  {path: '', component: ShopComponent},
  {path: ':id', component: GameDetailComponent},
]

@NgModule({
  declarations: [],
  imports: [
    //forchild means this route would not be available in our app module but only our shop module
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ShopRoutingModule { }
