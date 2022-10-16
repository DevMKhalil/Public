import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Header/header.component';
import { Routes, RouterModule } from '@angular/router';
import { ItemListComponent } from './item-list/item-list.component';
import { ShopingCartComponent } from './shoping-cart/shoping-cart.component';
import { ItemListItemComponent } from './item-list/item-list-item/item-list-item.component';

const appRoutes: Routes = [
  {path:'',component: ItemListComponent},
  {path:'ShopingCart',component: ShopingCartComponent},
]

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ItemListComponent,
    ShopingCartComponent,
    ItemListItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
