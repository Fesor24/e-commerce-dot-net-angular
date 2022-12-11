import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IConsole } from '../shared/Models/console';
import { IGenre } from '../shared/Models/genre';
import { IPagination } from '../shared/Models/pagination';
import {map} from 'rxjs/operators';
import { ShopParams } from '../shared/Models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7262/api/'

  constructor(private http: HttpClient) { }

  //making a call to our api to return all games
  //this returns an 
  //typescript classes can be used as types, reason why we do not create a new instance below
getProducts(shopParams: ShopParams){

  //lets implement filtering
  //we need the deviceId and genreId in the url
  //we create a params object that we can pass to our url
  let params = new HttpParams();

  //checking if we have a genreId
  if(shopParams.genreId !== 0){
    //first param is the query string we are sending to the url
    //second param is the value..we convert it to a string
    params = params.append('genreId', shopParams.genreId.toString());
  }

  //we do same for consoleDevice
  if(shopParams.deviceId !== 0){
    params = params.append('deviceId', shopParams.deviceId.toString());
  }

  //we do same for search
  if(shopParams.search){
    params = params.append('search', shopParams.search);
  }

  
  params = params.append('sort', shopParams.sort)

  params = params.append('pageIndex', shopParams.pageNumber.toString());

  params = params.append('pageSize', shopParams.pageSize.toString());
  

  //we pass in our params object to our get request
  //we observe the response and pass the params object
  //we need to extract the body out of the response as it does not return the body like the other simple get requests
  //we want to get the body of the response and project it into an IPagination object
  return this.http.get<IPagination>(this.baseUrl + 'games', {observe: 'response', params})
  .pipe(
    //pipe is a wrapper around any rxjs operator

    //inside this pipe we have a httpresponse and we want to map it into an IPagination response
    //this we do by getting the response.body which will be a type of IPagination object
    map(response => {
      return response.body
    })
  );
}

//making a call to our api to return all genres
//this returns an observable
getGenre(){
  return this.http.get<IGenre[]>(this.baseUrl + 'games/genre');
}

//making a call to our api to return all devices
//this returns an observable
getConsole(){
  return this.http.get<IConsole[]>(this.baseUrl + 'games/console');
}
}
