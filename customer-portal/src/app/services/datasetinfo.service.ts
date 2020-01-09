import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Dataset } from '../datatypes/dataset';
import { globals } from '../globals';

@Injectable({
  providedIn: 'root'
})
export class DatasetinfoService {

  constructor(private http: HttpClient) { }
  baseUrl = 'http://localhost/captcha/getDatasetInfo.php';
  dataset:Dataset;
  notifications: Notification[];

  getInfo(id_dataset:string):Observable<Dataset>{
    const data = {
      "id_customer" : globals['user_id'],
      "id_dataset" : id_dataset
    };
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
      this.dataset = res['data']['info'];
      this.notifications = res['data']['events'];
      console.log(this.notifications);
      return this.dataset;
    }),
    catchError(this.handleError));
  }
  private handleError(error: HttpErrorResponse){
    console.log(error);
    return throwError('Error! something went wrong.');
  }
}
