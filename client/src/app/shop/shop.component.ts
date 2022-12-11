import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IConsole } from '../shared/Models/console';
import { IGames } from '../shared/Models/games';
import { IGenre } from '../shared/Models/genre';
import { ShopParams } from '../shared/Models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
 
  @ViewChild('search', {static:false}) searchTerm!: ElementRef
//initializing the games array variable  
games?: IGames[] = [];

//initializing the genre array variable
genres?: IGenre[] = [];

//initializing the console array variable
consoleDevices?: IConsole[] = [];

//This will receive the total count of products
totalCount!: number;

//Creating an instance of our ShopParams
shopParams = new ShopParams();

//for sorting we will create an array of objects
//the value is what we will be sending to the query url
//we will loop over this and display in our html
sortOptions = [
  {name: 'Alphabetical', value: 'name'},
  {name: 'Price: Low to High', value: 'priceAsc'},
  {name: 'Price: High to Low', value: 'priceDesc'}
]
//injecting our shop service
constructor(private shopService: ShopService){}

ngOnInit(){
 
 this.getProducts();

 this.getGenre();

 this.getConsole();
 
}

//subscribing so we can get all products
getProducts(){
  this.shopService.getProducts(this.shopParams).subscribe((response => {
    this.games = response!.data;
    this.shopParams.pageSize = response!.pageSize;
    this.shopParams.pageNumber = response!.pageIndex;
    this.totalCount = response!.count;
  }), error => {
    console.log(error)
  })
}

//subscribing so we can get all genre
getGenre(){
  this.shopService.getGenre().subscribe((response => {

    //giving the users the option to pick all
    //spread operator
    //we are creating another object to add to the array
  this.genres = [{'id': 0, name: 'All'}, ...response]
  }), error => {
    console.log(error);
    
  })
}

//subscribing so we can get all console
getConsole(){
  this.shopService.getConsole().subscribe((response => {
    this.consoleDevices = [{'id': 0, name: 'All'}, ...response]
  }), error => {
    console.log(error)
  })
}

//This method will be used with the onclick event
//If an item is clicked, it will get the genre.id of that item
//Then it will pass it to the genreIdSelected variable
onGenreSelected(genreId: number){
  this.shopParams.genreId = genreId;

  //so we return the first page after our filter has been applied
  this.shopParams.pageNumber = 1

  //we get the products based on the genre id
  this.getProducts();
}

onDeviceSelected(deviceId: number){
  this.shopParams.deviceId = deviceId;

  //after we apply our filter we return the first page
  this.shopParams.pageNumber = 1;

  //we get the products based on the genre id
  this.getProducts();
}

//in the html, instead of a click event we will use a change event
//bcos as soon as the dropdown is selected, we want it to fire the particular event we are looking for

//we cast the event.target as HtmlInputelement so we can get the value and we pass it to sort

//another way to go abt this if we decide to use string as our parameter is to set
//strictDomEventTypes to false in tsconfig.json
onSortSelected(event: Event){
  const sort = (event.target as HTMLInputElement).value
  this.shopParams.sort = sort;
  this.getProducts();
}

//event.page is what we get from our event which is passed by the pagination method
onPageChanged(event: any){

  if(this.shopParams.pageNumber !== event){
     //event.page emits the current page number
  this.shopParams.pageNumber = event;
  this.getProducts();
  }
 
}

onSearch(){
  this.shopParams.search = this.searchTerm.nativeElement.value
  this.getProducts();
}

onReset(){
  this.searchTerm.nativeElement.value = '';
  this.shopParams = new ShopParams();
  this.getProducts();
}


}
