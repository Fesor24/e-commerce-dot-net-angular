import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrdersService implements OnInit {

  constructor(private http: HttpClient) { }

  baseUrl = 'https://localhost:7262/api/';

  ngOnInit(): void {
    
  }

  getOrdersForUser(){
    return this.http.get(this.baseUrl + 'orders');
  }

  getOrderById(id: number){
    return this.http.get(this.baseUrl + 'orders/' + id );
  }
}
