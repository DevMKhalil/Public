import { Component, OnInit } from '@angular/core';
import { ItemService } from './item.service';
import { Item } from '../Shared/Item.model';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {
  itemList: Item [] = [ ]
  constructor(private itemService: ItemService) { }

  ngOnInit(): void {
    this.itemList = this.itemService.GetItems();
  }

}
