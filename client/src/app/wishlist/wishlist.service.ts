import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, map } from 'rxjs';
import { IGames } from '../shared/Models/games';
import { IWishlist, IWishlistItem, Wishlist } from '../shared/Models/wishlist';

@Injectable({
  providedIn: 'root'
})
export class WishlistService implements OnInit{

  baseUrl = 'https://localhost:7262/api/';

  private wishlistSource = new BehaviorSubject<IWishlist>(null);

  wishlist$ = this.wishlistSource.asObservable();


  constructor(private http: HttpClient, private toastr: ToastrService) { }



  ngOnInit(): void {

  }

  getWishlist(id: string){
    return this.http.get<IWishlist>(this.baseUrl + 'wishlist?id=' + id)
      .pipe(
        map((wishlist: IWishlist)=> {
          this.wishlistSource.next(wishlist)
        })
      )
  }

  setWishlist(wishlist: IWishlist){
    this.http.post<IWishlist>(this.baseUrl + 'wishlist', wishlist).subscribe((response) => {
      this.wishlistSource.next(response);
    }, error => {
      console.log(error);
    })
  }


  deleteWishlist(wishlist: IWishlist){
    this.http.delete(this.baseUrl + 'wishlist?id=' + wishlist.id).subscribe(() => {
      this.wishlistSource.next(null);
      localStorage.removeItem('wishlist_id');

    }, error => {
      console.log(error);
    })
  }

  getWishlistCurrentValue(){
    return this.wishlistSource.value;
  }

  addItemToWishlist(item: IGames){
    const itemToAdd : IWishlistItem = this.mapGameItemToWishlistItem(item)

    const wishlist = this.getWishlistCurrentValue() ?? this.createWishlist();

    wishlist.items = this.addWishlistItems(wishlist.items, itemToAdd);

    console.log(wishlist);

    this.setWishlist(wishlist);


  }

  removeItemsFromWishList(items: IWishlistItem){
    const wishlist = this.getWishlistCurrentValue();

    if(wishlist.items.some(x => x.id === items.id)){
      wishlist.items = wishlist.items.filter(x=> x.id !== items.id)
    }

    if(wishlist.items.length > 0){
      this.setWishlist(wishlist);
    }else{
      this.deleteWishlist(wishlist);
    }
  }

  private addWishlistItems(items: IWishlistItem[], itemToAdd: IWishlistItem): IWishlistItem[] {
    const index = items.findIndex(x => x.id === itemToAdd.id);

    if(index === -1){
      items.push(itemToAdd);
      this.toastr.success("Items added to wishlist");
    }else{
      this.toastr.error('Item already in wishlist')
      return items;

    }

    return items;

  }

  private createWishlist(): IWishlist {
    const wishlist = new Wishlist()

    localStorage.setItem('wishlist_id', wishlist.id);

    return wishlist;

  }


  private mapGameItemToWishlistItem(item: IGames): IWishlistItem {
    return {
      id : item.id,
      gameName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      genre: item.genre,
      device: item.consoleDevice

    }
  }


}
