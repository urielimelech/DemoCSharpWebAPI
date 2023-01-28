import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IStockItem } from 'src/app/interfaces/IStockItem';
import { RequestConfigService } from 'src/app/services/requestConfig/request-config.service';
import { Observable } from 'rxjs';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class StoreModule {
  private endpoint: string
  constructor(private _requestConfigService: RequestConfigService) {
    this.endpoint = '/Store'
  }

  getAllItems(): Observable<IStockItem[]> {
    console.log(this._requestConfigService.header)
    return this._requestConfigService.postRequest(`${this.endpoint}/GetItems`, null)
  }
  addNewItem(item: IStockItem): Observable<IStockItem> {
    return this._requestConfigService.postRequest(`${this.endpoint}/CreateItem`, item)
  }
  searchItem(name: string): Observable<IStockItem[]> {
    console.log(name)
    return this._requestConfigService.postRequest(`${this.endpoint}/SearchItemByName`, { name })
  }
  updateItem(item: IStockItem): Observable<IStockItem> {
    return this._requestConfigService.postRequest(`${this.endpoint}/UpdateItem`, item)
  }
}
