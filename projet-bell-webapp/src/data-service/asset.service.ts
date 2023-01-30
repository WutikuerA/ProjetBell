import { Injectable, isDevMode } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { DataService } from './data-service';
import { Asset } from 'src/models/asset.model';



@Injectable({
  providedIn: 'root'
})
export class AssetDataService {

  constructor(private http: HttpClient, private dataService: DataService) { }

  port: string = isDevMode() ? '5011' : '8080';
  readonly baseURL = 'http://localhost:' + this.port + '/api/Asset';
  list: Asset[] = [];
  formData: Asset = new Asset();

  getAsset(filter: any) : Observable<any>
  {
    let requestParams = this.dataService.getFormattedParams(filter);
    return this.http.get<Asset[]>(this.baseURL + "/GetAssets", { params: requestParams });
  }

  getAssets(filter: any) : Observable<any>
  {
    window.console.log('port number:' + this.port);
    let requestParams = this.dataService.getFormattedParams(filter);
    return this.http.get<Asset[]>(this.baseURL + "/GetAssets", { params: requestParams });
  }

  postAsset(item: Asset) : Observable<any> {
    return this.http.post(this.baseURL, item);
  }
  
  modifyAsset(item: Asset) : Observable<Asset> 
  {
    return this.http.post<Asset>(this.baseURL, item);
  }

  deleteAsset(id: number)  : Observable<boolean>
  {
    return this.http.delete<boolean>(`${this.baseURL}/${id}`);
  }

//   refreshList() {
//     this.http.get(this.baseURL)
//       .toPromise()
//       .then(res =>this.list = res as Asset[]);
//   }

}
