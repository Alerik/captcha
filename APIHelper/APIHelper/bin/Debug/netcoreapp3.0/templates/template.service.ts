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

    --FUNCTIONS--
}