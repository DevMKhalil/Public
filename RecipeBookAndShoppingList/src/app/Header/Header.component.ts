import { Component} from '@angular/core';
import { DataStorageService } from '../Shared/data-storage.service';

@Component({
    selector: `app-header`,
    templateUrl: `./Header.component.html`
})

export class HeaderComponent{

  constructor(private dataService: DataStorageService) { }

  onSaveData() {
    this.dataService.storeRecipes();
  }

  onFeachData() {
    this.dataService.fetchRecipes();
  }
}
