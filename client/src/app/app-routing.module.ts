import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path:'', component: HomeComponent },
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(x => x.ShopModule)},
  {path : 'basket', loadChildren: () => import('./basket/basket.module').then(x => x.BasketModule)},
  {path: 'checkout',
    canActivate: [AuthGuard],
    loadChildren: () => import('./checkout/checkout.module').then(x => x.CheckoutModule)},
  {path: 'account', loadChildren: () => import('./account/account.module').then(x => x.AccountModule)},
  //this is a wild card, when someone enters a bad url, he will be redirected to the not-found page
  {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
