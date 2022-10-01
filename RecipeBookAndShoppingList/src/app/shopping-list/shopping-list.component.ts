import { Component, OnInit } from '@angular/core';
import { Ingredient } from '../Shared/Ingredient.model';
import { ShoppingListService } from './shopping-list.service';

@Component({
  selector: 'app-shopping-list',
  templateUrl: './shopping-list.component.html',
  styleUrls: ['./shopping-list.component.css']
})
export class ShoppingListComponent implements OnInit {

  ingredientList: Ingredient[] = [];

  constructor(private shoppingListService: ShoppingListService) { }

  ngOnInit(): void {
    this.ingredientList = this.shoppingListService.getIngredients();
    this.shoppingListService.ingredientChanged.subscribe(
      (ingredients: Ingredient[]) => {
        this.ingredientList = ingredients;
      }
    );
  }
}
