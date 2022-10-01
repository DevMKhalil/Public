import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { Recipe } from '../Recipe.model';
import { Ingredient } from '../../Shared/Ingredient.model';
import { RecipeService } from '../recipe.service';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.css']
})
export class RecipeDetailComponent implements OnInit {

 @Input() loadedRecipe!:Recipe

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
  }

  onSendingredientListToShoppingList(){
    this.recipeService.sendingredientList(this.loadedRecipe.ingredientList);
  }
}
