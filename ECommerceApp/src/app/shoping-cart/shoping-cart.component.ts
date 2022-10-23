import { Component, OnInit, OnDestroy } from '@angular/core';
import { ShopingCartService } from './shoping-cart.service';
import Item from '../Shared/Item.model';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shoping-cart',
  templateUrl: './shoping-cart.component.html',
  styleUrls: ['./shoping-cart.component.css']
})
export class ShopingCartComponent implements OnInit ,OnDestroy {

  itemList: Item [] = [];
  subscription: Subscription;

  totalCost: number = 0;

  constructor(private cartService: ShopingCartService,private router: Router) {
    this.subscription = this.cartService.Items$.subscribe((items) => {
      this.itemList = items;
    });
   }

  ngOnInit(): void {
    this.totalCost = this.cartService.CalculateTotal();
  }

  // OnSubmit(){
  //   this.router.navigate(["/CustomerInfo"]);
  // }

  CalculateTotal(){
    if (this.itemList.some(e => e.amount < 0)){
      alert("Amount Must Be Greater Than Zero");
      return;
    }

    this.totalCost = this.cartService.CalculateTotal();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
