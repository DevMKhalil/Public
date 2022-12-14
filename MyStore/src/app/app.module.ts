import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Header/header.component';
import { ItemListComponent } from './item-list/item-list.component';
import { ShopingCartComponent } from './shoping-cart/shoping-cart.component';
import { ItemListItemComponent } from './item-list/item-list-item/item-list-item.component';
import { ItemDetailsComponent } from './item-details/item-details.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ShopingCartItemComponent } from './shoping-cart/shoping-cart-item/shoping-cart-item.component';
import { ConfirmationPageComponent } from './confirmation-page/confirmation-page.component';
import { CustomerInfoComponent } from './shoping-cart/customer-info/customer-info.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ItemListComponent,
    ShopingCartComponent,
    ItemListItemComponent,
    ItemDetailsComponent,
    NotFoundComponent,
    ShopingCartItemComponent,
    CustomerInfoComponent,
    ConfirmationPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
