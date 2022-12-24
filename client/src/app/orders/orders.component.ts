import { Component, OnInit } from '@angular/core';
import { IOrder, IOrderReturn } from '../shared/Models/order';
import { OrdersService } from './orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orders: IOrderReturn[];

  constructor (private orderService: OrdersService) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders(){
    this.orderService.getOrdersForUser().subscribe((ordersList: IOrderReturn[]) => {
      this.orders = ordersList
    }, error => {
      console.log(error)
    })
  }

}
