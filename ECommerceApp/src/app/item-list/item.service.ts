import { Injectable } from '@angular/core';
import Item from '../Shared/Item.model';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  private ItemList: Item[] = [];
  Items$ = new BehaviorSubject<Item[]>(this.ItemList);

  constructor(private http: HttpClient) { }

  GetItems(){
    const subscription = this.http.get<Item[]>('../../assets/data.json').subscribe((items) => {
      this.ItemList = items;
      this.Items$.next(this.ItemList);
      subscription.unsubscribe();
    });
  }

  // private itemList: Item [] = [
  //   new Item(
  //     "01",
  //     "Item 1",
  //     100,
  //     0,
  //     "https://m.media-amazon.com/images/I/41Fln4bMH1L._AC_.jpg",
  //   "Some quick example text to build on the card title and make up the bulk of the card's content."),
  //   new Item(
  //   "02",
  //   "Item 2",
  //   100,
  //   0,
  //   "https://m.media-amazon.com/images/I/41pXu93xgzL._AC_.jpg",
  //   "Some quick example text to build on the card title and make up the bulk of the card's content."),
  //   new Item(
  //     "03",
  //     "Item 3",
  //     100,
  //     0,
  //     "https://eg.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/05/578481/1.jpg?8896",
  //     "Some quick example text to build on the card title and make up the bulk of the card's content."),
  //   new Item(
  //     "04",
  //     "Item 4",
  //     100,
  //     0,
  //     "https://eg.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/68/205922/1.jpg?8014",
  //     "Some quick example text to build on the card title and make up the bulk of the card's content."),
  //   new Item(
  //     "05",
  //     "Item 5",
  //     100,
  //     0,
  //     "https://eg.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/49/831062/1.jpg?7126",
  //     "Some quick example text to build on the card title and make up the bulk of the card's content."),
  //   new Item(
  //     "06",
  //     "Item 6",
  //     100,
  //     0,
  //     "https://cdn.shopify.com/s/files/1/0032/2401/0861/products/madmext-mavi-ekoseli-gomlek-4704-d6d0_900x.jpg?v=1621775865",
  //     "Some quick example text to build on the card title and make up the bulk of the card's content."),
  //   new Item(
  //     "07",
  //     "Item 7",
  //     100,
  //     0,
  //     "https://m.media-amazon.com/images/I/41hnqpOFonL._AC_SY780_.jpg",
  //     "Some quick example text to build on the card title and make up the bulk of the card's content."),
  //   new Item(
  //     "08",
  //     "Item 8",
  //     100,
  //     0,
  //     "https://image.made-in-china.com/155f0j00ZsGEPOIKfcpF/Men-s-Dress-Shirt-Men-Plus-Size-Long-Sleeve-Male-Business-Shirts-Slim-Fit-Office-Man-s.jpg",
  //     "Some quick example text to build on the card title and make up the bulk of the card's content."),
  // ]

  
}
