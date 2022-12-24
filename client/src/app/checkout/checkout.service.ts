import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IDeliveryMethod } from '../shared/Models/deliveryMethod';
import { IOrderCreate } from '../shared/Models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = 'https://localhost:7262/api/';

  constructor(private http: HttpClient) { }

  createOrder(order: IOrderCreate){
    return this.http.post(this.baseUrl + 'orders', order)
  }

  getDeliveryMethods(){
    return this.http.get(this.baseUrl + 'orders/delivery-methods').pipe(
      map((dm : IDeliveryMethod[]) =>{
        return dm.sort((a,b) => b.price - a.price)
      })
    )
  }
}
