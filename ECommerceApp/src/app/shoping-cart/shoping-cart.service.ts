import { Injectable } from '@angular/core';
import Item from '../Shared/Item.model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopingCartService {

  private ItemList: Item[] = [];
  Items$ = new BehaviorSubject<Item[]>(this.ItemList);
  totalCost: number = 0;

  constructor() { }

  addItem(itemAdded: Item) {
    const index = this.ItemList.findIndex(item => item.id === itemAdded.id);
    if (index <= -1) {
      itemAdded.amount = 1;
      this.ItemList.push(itemAdded);
    }
    else{
      this.ItemList[index].amount += 1;
    }

    this.Items$.next(this.ItemList);
  }

  deleteItem(item: Item) {
    const index = this.ItemList.indexOf(item, 0);
    if (index > -1) {
      this.ItemList.splice(index, 1);
    }

    this.Items$.next(this.ItemList);
  }

  CalculateTotal(){
    return this.ItemList.reduce((sum, item) => sum + (item.amount * item.price), 0);
  }
}
