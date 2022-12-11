import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path:'', component: HomeComponent },
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(x => x.ShopModule)},
  //this is a wild card, when someone enters a bad url, he will be redirected to the home page
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
