import { Injectable } from '@angular/core';

import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { datasets } from '../datatypes/datasets';

@Injectable({
  providedIn: 'root'
})
export class datasetsService {

  constructor(private http: HttpClient) {  
  }

  url = 'http://127.0.0.1/captcha';
  entries: datasets[];

get(name : string, entry_count : number, completed : boolean):Observable<datasets[]>{
  const data = {
    "name" : name,
"entry_count" : entry_count,
"completed" : completed
  };

  return this.http.post(this.url, JSON.stringify(data),
  {headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })}).pipe(
    map((res) => {
      if('error' in res){
        console.error("Error occured: ", res['error']);
        return;
      }
      this.entries = res['data']['entries'];
      return this.entries;
    }),
    catchError(this.handleError));
}
private handleError(error: HttpErrorResponse){
  console.log(error);
  return throwError('Error! something went wrong.');
}
}