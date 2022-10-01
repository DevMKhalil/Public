import { Component, OnInit, Input } from '@angular/core';
import { Recipe } from '../../Recipe.model';
import { RecipeService } from '../../recipe.service';

@Component({
  selector: 'app-recipe-item',
  templateUrl: './recipe-item.component.html',
  styleUrls: ['./recipe-item.component.css']
})
export class RecipeItemComponent implements OnInit {

  @Input() recipeItem!: Recipe;

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
  }

  recipeSelected(){
    this.recipeService.recipeSelected.emit(this.recipeItem); 
  }

}
