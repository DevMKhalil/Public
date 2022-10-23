import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Item from '../Shared/Item.model';
import { Subscription } from 'rxjs';
import { ItemService } from '../item-list/item.service';
import { ShopingCartService } from '../shoping-cart/shoping-cart.service';

@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.css']
})
export class ItemDetailsComponent implements OnInit,OnDestroy {
  
   id: number = 0;
   item?: Item;

   routerSubscription: Subscription;
   itemSubscription: Subscription;

  constructor(private route: ActivatedRoute,private router: Router,private itemService: ItemService,private cartService: ShopingCartService) {
    this.routerSubscription = this.route.params.subscribe((parms) => {
      this.id = +parms['id'];
    });

    this.itemSubscription = this.itemService.Items$.subscribe((itemList) => {
      if(itemList.length === 0){
        this.itemService.GetItems();
        return;
      }

      this.item = this.itemService.GetItem(this.id);

      if (!this.item){
        this.router.navigate(['/404']);
      }
    });
   }

  ngOnInit(): void {
    
  }

  AddToCart(){
    this.cartService.addItem(this.item!);
    alert("Item Added Successfully");
    this.router.navigate(["/ShopingCart"]);
  }

  ngOnDestroy(): void {
    this.routerSubscription.unsubscribe();
    this.itemSubscription.unsubscribe();
  }
}
