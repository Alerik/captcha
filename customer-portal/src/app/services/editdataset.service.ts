import { Injectable } from '@angular/core';
import { HttpHeaders, HttpErrorResponse, HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EditdatasetService {

  constructor(private http: HttpClient) {  
  }

  baseUrl = 'http://localhost/captcha/editDataset.php';

update(id:string,name:string, prompt:string, description:string):Observable<boolean>{
  const data = {
    'id' : id,
    'prompt' : prompt,
    'description' : description,
    'name' : name
  };
  const header = new HttpHeaders({
    'Content-Type': 'application/json'
  });
  return this.http.post(this.baseUrl, JSON.stringify(data),
  {headers: header}).pipe(
    map((res) => {
      return true;
    }));
}
}
