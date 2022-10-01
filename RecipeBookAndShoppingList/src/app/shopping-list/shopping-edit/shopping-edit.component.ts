import { Component, OnInit, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { Ingredient } from '../../Shared/Ingredient.model';
import { ShoppingListService } from '../shopping-list.service';

@Component({
  selector: 'app-shopping-edit',
  templateUrl: './shopping-edit.component.html',
  styleUrls: ['./shopping-edit.component.css']
})
export class ShoppingEditComponent implements OnInit {
  @ViewChild('nameInput') nameInputRef!: ElementRef;
  @ViewChild('numberInput') numberInputRef!: ElementRef;
  @Output() ingredientAdded = new EventEmitter<Ingredient>();

  constructor(private shoppingListService: ShoppingListService) { }

  ngOnInit(): void {
  }

  onAddItem(){
    const ingName = this.nameInputRef.nativeElement.value;
    const ingNumber = this.numberInputRef.nativeElement.value;
    const newIngrediant = new Ingredient(ingName,ingNumber);
    this.shoppingListService.addIngredient(newIngrediant);
  }
}
