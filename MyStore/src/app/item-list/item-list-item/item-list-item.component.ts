import { Component, OnInit, Input } from '@angular/core';
import Item from '../../Shared/Item.model';
import { Router } from '@angular/router';
import { ShopingCartService } from '../../shoping-cart/shoping-cart.service';

@Component({
  selector: 'app-item-list-item',
  templateUrl: './item-list-item.component.html',
  styleUrls: ['./item-list-item.component.css']
})
export class ItemListItemComponent implements OnInit {

  @Input() item!: Item; 

  constructor(private router: Router,private cartService: ShopingCartService) { }

  ngOnInit(): void {
  }

  AddToCart(){
    this.cartService.addItem(this.item!);
    alert("Item Added Successfully");
    this.router.navigate(["/ShopingCart"]);
  }
}
