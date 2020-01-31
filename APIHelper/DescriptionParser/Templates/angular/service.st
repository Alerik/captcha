import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { --DATATYPES-- } from '../datatypes/<PathName>';

@Injectable({
  providedIn: 'root'
})
export class <PathName>Service {

  constructor(private http: HttpClient) {  
  }

    --FUNCTIONS--
}