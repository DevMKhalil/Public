import { Injectable } from '@angular/core';
import { Recipe } from './Recipe.model';
import { Ingredient } from '../Shared/Ingredient.model';
import { ShoppingListService } from '../shopping-list/shopping-list.service';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {


  constructor(private shoppingListService: ShoppingListService) { }

  recipeList: Recipe[] = [
    new Recipe('A_test_Recipe_1','This Is Simply a test 1','https://cdn.pixabay.com/photo/2016/06/15/19/09/food-1459693_960_720.jpg',
    [
      new Ingredient('ginger',15),
      new Ingredient('tomato',5)
    ]),
    new Recipe('A test Recipe 2','This Is Simply a test 2','https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F44%2F2021%2F02%2F02%2Fwheat-berry-salad-2000.jpg',
    [
      new Ingredient('botato',3),
      new Ingredient('tomato',6)
    ]),
    new Recipe('A test Recipe 3','This Is Simply a test 3','https://media-cldnry.s-nbcnews.com/image/upload/newscms/2021_32/1759222/ratatouille-mc-main-210809-v2.jpeg',
    [
      new Ingredient('mango',10),
      new Ingredient('onion',12)
    ]),
  ];

  getRecipes(){
    return this.recipeList.slice();
  }

  getRecipe(index: number){
    return this.recipeList[index];
  }

  sendingredientList(ingredientList: Ingredient[]){
    this.shoppingListService.addIngredientsToShopingList(ingredientList);
  }
}
