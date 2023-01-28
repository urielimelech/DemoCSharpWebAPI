import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IStockItem } from '../interfaces/IStockItem';
import { StoreModule } from '../Modules/store/store.module';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.css']
})
export class StoreComponent {

  constructor(private store: StoreModule) {
  }
  itemsList!: IStockItem[]

  ngOnInit() {
    this.store.getAllItems().subscribe((response: IStockItem[]) => {
      this.itemsList = response
    })
  }

  addItemForm = new FormGroup({
    itemName: new FormControl(''),
    itemDescription: new FormControl(''),
    itemCount: new FormControl('0'),
    itemPrice: new FormControl('0')
  })

  updateItemForm = new FormGroup({
    itemId: new FormControl(''),
    itemName: new FormControl(''),
    itemDescription: new FormControl(''),
    itemCount: new FormControl('0'),
    itemPrice: new FormControl('0')
  })

  searchItemForm = new FormGroup({
    searchItem: new FormControl('')
  })

  addItem() {
    console.log("add Item")
    const values = this.addItemForm.value
    if (values.itemCount && parseInt(values.itemCount) && values.itemDescription && values.itemName && values.itemPrice) {
      const item: IStockItem = { name: values.itemName, description: values.itemDescription, count: parseInt(values.itemCount), price: values.itemPrice, id: 0, createdAt: '', createdBy: '' }
      const res = this.store.addNewItem(item)
      res.subscribe((i: IStockItem) => {
        this.itemsList.push(i)
      })
    }
  }
  updateItem() {
    const values = this.updateItemForm.value
    if (values.itemId && parseInt(values.itemId) && values.itemCount && parseInt(values.itemCount) && values.itemDescription && values.itemName && values.itemPrice) {
      const item: IStockItem = { name: values.itemName, description: values.itemDescription, count: parseInt(values.itemCount), price: values.itemPrice, id: parseInt(values.itemId), createdAt: '', createdBy: '' }
      this.store.updateItem(item).subscribe((i: IStockItem) => {
        this.itemsList = this.itemsList.map(t => t.id === i.id ? i : t)
      })
    }
  }
  searchItem() {
    console.log("search Item")
    const value = this.searchItemForm.value
    if (value.searchItem) {
      const res = this.store.searchItem(value.searchItem)
      res.subscribe((items: IStockItem[]) => {
        this.itemsList = items
      })
    }
  }

}
