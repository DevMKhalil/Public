import { Component, OnInit, Input } from '@angular/core';
import { Item } from '../../Shared/Item.model';

@Component({
  selector: 'app-item-list-item',
  templateUrl: './item-list-item.component.html',
  styleUrls: ['./item-list-item.component.css']
})
export class ItemListItemComponent implements OnInit {

  @Input() item!: Item; 

  constructor() { }

  ngOnInit(): void {
  }

}
