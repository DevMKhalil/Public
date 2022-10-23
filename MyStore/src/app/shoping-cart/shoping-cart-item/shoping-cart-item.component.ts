import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import Item from '../../Shared/Item.model';
import { ShopingCartService } from '../shoping-cart.service';

@Component({
  selector: 'app-shoping-cart-item',
  templateUrl: './shoping-cart-item.component.html',
  styleUrls: ['./shoping-cart-item.component.css']
})
export class ShopingCartItemComponent implements OnInit {

  @Input() item!: Item;

  @Output() itemChanged = new EventEmitter();

  constructor(private cartService: ShopingCartService) { }

  ngOnInit(): void {
  }

  RemoveItem(item: Item){
    this.cartService.deleteItem(item);
    alert("Item Removed Successfully");
  }

  IncreaseAmount(item: Item){
    if (item.amount < 1){
      item.amount = 1;
      return;
    }

    item.amount += 1;
    this.itemChanged.emit();
  }

  DecreaseAmount(item: Item){
    if (item.amount <= 1){
      return;
    }

    item.amount -= 1;
    this.itemChanged.emit();
  }

  AmountChanged(amount: number){
    if (amount < 1){
      alert("Amount Must Be Greater Than Zero");
    }
  }

    DomAmountChanged(item: Item){
      if (item.amount < 1){
        item.amount = 1;
      }

    this.itemChanged.emit();
  }
}
