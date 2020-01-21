import { Injectable } from '@angular/core';

import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { --DATATYPE-- } from '../datatypes/--DATATYPE--';

@Injectable({
  providedIn: 'root'
})
export class --SERVICE_NAME--Service {

  constructor(private http: HttpClient) {  
  }

  url = --URL--;
  entries: --DATATYPE--[];

get(--ARG_IDENTIFIERS--):Observable<--DATATYPE--[]>{
  const data = {
    --ARG_DEFINITIONS--
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