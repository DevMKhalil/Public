import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Ingredient } from '../../Shared/Ingredient.model';
import { ShoppingListService } from '../shopping-list.service';

@Component({
  selector: 'app-shopping-edit',
  templateUrl: './shopping-edit.component.html',
  styleUrls: ['./shopping-edit.component.css']
})
export class ShoppingEditComponent implements OnInit {
  @Output() ingredientAdded = new EventEmitter<Ingredient>();

  constructor(private shoppingListService: ShoppingListService) { }

  ngOnInit(): void {
    this.projectForm = new FormGroup({
      'Name': new FormControl(null, Validators.required),
      'Amount': new FormControl(null, [Validators.required, Validators.pattern('^[1-9]+[0-9]*$')])
    });
  }

  onSave() {
    console.log(this.projectForm.value);
    const newIngrediant = new Ingredient(this.projectForm.value.Name, this.projectForm.value.Amount);
    this.shoppingListService.addIngredient(newIngrediant);
  }

  projectForm!: FormGroup;
}
