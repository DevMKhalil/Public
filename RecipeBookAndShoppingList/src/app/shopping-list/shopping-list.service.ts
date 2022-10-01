import { Injectable, EventEmitter } from '@angular/core';
import { Ingredient } from '../Shared/Ingredient.model';

@Injectable({
  providedIn: 'root'
})
export class ShoppingListService {

  ingredientChanged = new EventEmitter<Ingredient[]>();

  constructor() { }

  private ingredientList: Ingredient[] = [
    new Ingredient("ingredient 1",15),
    new Ingredient("ingredient 2",20),
    new Ingredient("Ingredient 3",25)
  ];

  getIngredients(){
    return this.ingredientList.slice();
  }

  addIngredient(Ingredient: Ingredient){
    this.ingredientList.push(Ingredient);
    this.ingredientChanged.emit(this.ingredientList.slice()); 
  }

  addIngredientsToShopingList(ingredientList: Ingredient[]){
    this.ingredientList.push(...ingredientList);
    this.ingredientChanged.emit(this.ingredientList.slice()); 
  }
}
