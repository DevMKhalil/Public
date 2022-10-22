import { Component, OnInit, Input } from '@angular/core';
import Item from '../../Shared/Item.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-item-list-item',
  templateUrl: './item-list-item.component.html',
  styleUrls: ['./item-list-item.component.css']
})
export class ItemListItemComponent implements OnInit {

  @Input() item!: Item; 

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  OnLoadItem(){
    this.router.navigate(["/ItemDetails"]);
  }
}
