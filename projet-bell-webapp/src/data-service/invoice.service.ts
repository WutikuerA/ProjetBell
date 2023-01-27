import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { DataService } from './data-service';
import { Asset } from 'src/models/asset.model';
import { Invoice } from 'src/models/invoice.model';



@Injectable({
  providedIn: 'root'
})
export class InvoiceDataService {

  constructor(private http: HttpClient, private dataService: DataService) { }

  readonly baseURL = 'http://localhost:5011/api/Invoice'
  list: Asset[] = [];
  formData: Asset = new Asset();


  getInvoices(filter: any) : Observable<any>
  {
    let requestParams = this.dataService.getFormattedParams(filter);
    return this.http.get<Invoice[]>(this.baseURL + "/GetInvoices", { params: requestParams });
  }

  generateInvoice() : Observable<any>
  {
    return this.http.get<Invoice>(this.baseURL + "/GenerateInvoice");
  }

  isAssetChanged() : Observable<boolean>
  {
    return this.http.get<boolean>(this.baseURL + "/IsAssetChanged");
  }



}
