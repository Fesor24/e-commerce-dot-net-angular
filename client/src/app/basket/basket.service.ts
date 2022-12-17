import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Basket, IBasket, IBasketItem, IBasketTotals } from '../shared/Models/basket';
import { IGames } from '../shared/Models/games';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  //setting the baseUrl
  baseUrl = 'https://localhost:7262/api/';

  //Creating a BehaviorSubject of the IBasket
  private basketSource = new BehaviorSubject<IBasket | null>(null);

  //Creating a BehaviorSubject of the IBasketTotals
  private basketTotalSource = new BehaviorSubject<IBasketTotals | null>(null);

  //passing the basketSource behaviorSubject as an observable
  basket$ = this.basketSource.asObservable();

  //passing the basketTotalSource behaviorSubject as an observable
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  //This method is calling our api and getting the shopping cart with the id from the redis server
  getBasket(id:string){
    return this.http.get<IBasket>(this.baseUrl + 'shoppingcart?id=' + id)
      .pipe(
        map((basket: IBasket) => {
          //we are updating our basketsource
          this.basketSource.next(basket);
          this.calculateTotal();
        })
      )
  }

  //This method is saving a new shopping cart on our redis server
  setBasket(basket: IBasket){
    return this.http.post<IBasket>(this.baseUrl + 'shoppingcart', basket).subscribe((response => {
      //we are updating our basketsource
      this.basketSource.next(response);
      this.calculateTotal();
    }), error => {
      console.log(error)
    })
  }

  //This method returns the value of the basketsource
  //whatver items we have in it will be returned
  getCurrentBasketValue(){
    //this will return whatever is in our basket
    return this.basketSource.value;
  }

  //This method adds item to our basket
  addItemToBasket(item: IGames, quantity = 1){
    const itemToAdd: IBasketItem = this.mapGameItemToBasketItem(item, quantity)

    //We are getting the basket of the user...if the user doesn't have a basket, we create a new basket
    const basket = this.getCurrentBasketValue() ?? this.createBasket();

    basket.items = this.addOrUpdate(basket.items, itemToAdd, quantity);

    console.log(basket);

    //Updating the basket on our redis server
    this.setBasket(basket);
  }

  incrementItemQuantity(item: IBasketItem){
    //getting the current basket
    const basket = this.getCurrentBasketValue();

    //we check if there is an existing item of that type
    //it returns the index
    const foundItemIndex = basket?.items.findIndex(x => x.id === item.id)

    basket.items[foundItemIndex].quantity++;

    //We are updating our redis server
    this.setBasket(basket);


  }

  decrementItemQuantity(item: IBasketItem){
    //getting the current basket
    const basket = this.getCurrentBasketValue();

    //we check if there is an existing item of that type
    //it returns the index
    const foundItemIndex = basket?.items.findIndex(x => x.id === item.id)

    if(basket.items[foundItemIndex].quantity > 1){
      basket.items[foundItemIndex].quantity--;
    }else{
      this.removeItemFromBasket(item);
    }

    //We are updating our redis server
    this.setBasket(basket);


  }
  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();

    if (basket.items.some(x => x.id === item.id)){
      basket.items = basket.items.filter(x => x.id !== item.id);
    }
    if(basket.items.length > 0){
      this.setBasket(basket)
    }else{
      this.deleteBasket(basket);
    }
  }


  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'shoppingcart?id=' + basket.id).subscribe(() => {
      this.basketSource.next(null)
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error)
    })
  }



  //this method will add the item to cart if it is not there or update the quantity if it is there
  private addOrUpdate(items: IBasketItem[], itemToAdd: IBasketItem, quantity:number): IBasketItem[] {
    const index = items.findIndex(x => x.id === itemToAdd.id);

    //this means that the id was not found
    if (index === -1){
      itemToAdd.quantity = quantity,
      items.push(itemToAdd)
    }else{
      items[index].quantity += quantity;
    }

    return items;
  }

  private createBasket(): IBasket {
    //We are creating a new basket and this will have a unique id
    //It will also initialize it with an empty list of items also
    const basket = new Basket();

    //We want to persist the basket on the local storage on client's browser
    //We will be able to get the basket as long as the user doesn't clear their local storage
    localStorage.setItem('basket_id', basket.id);

    return basket;

  }

  private mapGameItemToBasketItem(item: IGames, quantity: number): IBasketItem {
    return {
      id: item.id,
      gameName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      genre: item.genre,
      device: item.consoleDevice
    }
  }

  private calculateTotal(){
    const basket = this.getCurrentBasketValue();

    const shipping = 0;

    //b represents our items
    //a represent the number we are returning back
    //an initial value of 0
    const subtotal = basket?.items.reduce((a,b)=> b.price * b.quantity + a, 0)!

    const total = shipping + subtotal

    //updating our basketSourceTotal
    this.basketTotalSource.next({shipping, total, subtotal})
  }

}
