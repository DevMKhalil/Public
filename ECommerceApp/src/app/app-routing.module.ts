import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ItemListComponent } from './item-list/item-list.component';
import { ShopingCartComponent } from './shoping-cart/shoping-cart.component';
import { ItemDetailsComponent } from './item-details/item-details.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ConfirmationPageComponent } from './confirmation-page/confirmation-page.component';

const appRoutes: Routes = [
  {path:'',component: ItemListComponent},
  {path:'ShopingCart',component: ShopingCartComponent},
  {path:'ConfirmationPage',component: ConfirmationPageComponent},
  {path:'ItemDetails/:id',component: ItemDetailsComponent},
  {path:'**',component: NotFoundComponent},
]

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
