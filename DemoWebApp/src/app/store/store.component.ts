import { Component } from '@angular/core';
import { StoreModule } from '../Modules/store/store.module';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.css']
})
export class StoreComponent {

  constructor(private store: StoreModule) { }

  getItems() {
    this.store.getAllItems()
  }

}
