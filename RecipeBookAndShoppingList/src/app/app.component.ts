import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title = 'RecipeBookAndShoppingList';

  loadedFeature: string = 'recipe';
  
  onNavigate(feature: string){
    this.loadedFeature = feature;
  }
}
