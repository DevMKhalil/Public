import { Component, OnInit, OnDestroy } from '@angular/core';
import { ItemService } from './item.service';
import Item from '../Shared/Item.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit ,OnDestroy{
  
  itemList: Item [] = [];
  subscription: Subscription;
  constructor(private itemService: ItemService) { 
    this.itemService.GetItems();

    this.subscription = this.itemService.Items$.subscribe((items) => {
      this.itemList = items;
    });
  }

  ngOnInit(): void {
   
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
