import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AppRoutingModule } from "../app-routing.module";
import { SharedModule } from 'src/app/Shared/shared.module';
import { ShoppingEditComponent } from "./shopping-edit/shopping-edit.component";
import { ShoppingListComponent } from "./shopping-list.component";


@NgModule({
  declarations: [
    ShoppingListComponent,
    ShoppingEditComponent
  ],
  imports: [
    AppRoutingModule,
    RouterModule.forChild([{ path: 'shopping-list', component: ShoppingListComponent }]),
    SharedModule
  ]
})
export class ShoppingListModule { }
