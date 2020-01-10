import { Injectable } from '@angular/core';
import { Text_Index_Entry } from '../datatypes/entries/TextIndexEntry';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { globals } from '../globals';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RandomentriesService {

  constructor(private http: HttpClient) {  
  }

  baseUrl = 'http://localhost/captcha/randomEntries.php';
  entries: Text_Index_Entry[];

  get(id_dataset:string, count:number) : Observable<Text_Index_Entry[]>{
    const data = {
      'id_customer' : globals['user_id'],
      'id_dataset' : id_dataset,
      'count' : count
    };
    return this.http.post(this.baseUrl, JSON.stringify(data),
  {headers: globals['json_header']}).pipe(
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
