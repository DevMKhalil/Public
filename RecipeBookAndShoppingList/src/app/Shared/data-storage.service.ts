import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs";
import { Recipe } from "../Recipes/Recipe.model";
import { RecipeService } from "src/app/recipes/RecipeService";

@Injectable({providedIn:'root'})
export class DataStorageService {
  constructor(private http: HttpClient, private recipeService: RecipeService) { debugger; }

  storeRecipes() {
    const recipes = this.recipeService.getRecipes();
    this.http.put('https://recipebookandshoppinglist-ang-default-rtdb.firebaseio.com/recipes.json', recipes)
      .subscribe({
        next: res => {
          console.log(res);
        }});
  }

  fetchRecipes() {
    this.http.get<Recipe[]>('https://recipebookandshoppinglist-ang-default-rtdb.firebaseio.com/recipes.json')
      .pipe(map(recipes => {
        return recipes.map(recipe => {
          return {
            ...recipe,
            ingredientList: recipe.ingredientList ? recipe.ingredientList : []
          };
        });
      }))
      .subscribe({
        next: res => {
          debugger;
          this.recipeService.setRecipes(res);
        },
        error: err => {
          let message = err['error']
          alert(message['error']);
        }
      })
  }
}
