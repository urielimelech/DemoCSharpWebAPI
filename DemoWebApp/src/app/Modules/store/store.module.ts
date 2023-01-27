import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IStockItem } from 'src/app/interfaces/IStockItem';
import { RequestConfigService } from 'src/app/services/requestConfig/request-config.service';

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

  getAllItems() {
    this._requestConfigService.postRequest(`${this.endpoint}/getItems`, null).subscribe((response: IStockItem[]) => {
      console.log(response)
    })
  }
}
