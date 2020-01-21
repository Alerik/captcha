import { Injectable } from '@angular/core';

import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import {Dataset, DatasetTypes} from '../datatypes/dataset';
import { globals } from '../globals';

@Injectable({
  providedIn: 'root'
})
export class DatasetsService {

  constructor(private http: HttpClient) {  
  }

  baseUrl = 'http://localhost/captcha/getDatasets.php';
  datasets: Dataset[];

getAll():Observable<Dataset[]>{
  const data = {"id_customer" : globals['user_id']};
  const header = new HttpHeaders({
    'Content-Type': 'application/json'
  });
  return this.http.post(this.baseUrl, JSON.stringify(data),
  {headers: header}).pipe(
    map((res) => {
      if('error' in res){
        console.error("Error occured: ", res['error']);
        return;
      }
      this.datasets = res['data'];
      return this.datasets;
    }),
    catchError(this.handleError));
}
private handleError(error: HttpErrorResponse){
  console.log(error);
  return throwError('Error! something went wrong.');
}

}