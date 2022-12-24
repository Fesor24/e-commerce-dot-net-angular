import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrder, IOrderReturn } from 'src/app/shared/Models/order';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit {
order: IOrderReturn;

  constructor (private route: ActivatedRoute, private orderService: OrdersService) {}

  ngOnInit(): void {
    this.orderService.getOrderById(+this.route.snapshot.paramMap.get('id')).subscribe((order: IOrderReturn) => {
      this.order = order;
    }, error => {
      console.log(error);
    })
  }

}
