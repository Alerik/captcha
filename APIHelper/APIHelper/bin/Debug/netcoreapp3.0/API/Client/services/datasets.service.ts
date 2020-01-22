import { Injectable } from '@angular/core';

import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { DatasetN } from '../datatypes/DatasetN';

@Injectable({
  providedIn: 'root'
})
export class DatasetNService {

  constructor(private http: HttpClient) {  
  }

  url = 'http://127.0.0.1/captcha/create.php';
  entries: DatasetN[];

get(name : string, prompt : string, description : string, id : string):Observable<DatasetN[]>{
  const data = {
    "name" : name,
"prompt" : prompt,
"description" : description,
"id" : id
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
}import { Injectable } from '@angular/core';

import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { DatasetN } from '../datatypes/DatasetN';

@Injectable({
  providedIn: 'root'
})
export class DatasetNService {

  constructor(private http: HttpClient) {  
  }

  url = 'http://127.0.0.1/captcha/delete.php';
  entries: DatasetN[];

get(id : string):Observable<DatasetN[]>{
  const data = {
    "id" : id
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
}import { Injectable } from '@angular/core';

import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { DatasetN } from '../datatypes/DatasetN';

@Injectable({
  providedIn: 'root'
})
export class DatasetNService {

  constructor(private http: HttpClient) {  
  }

  url = 'http://127.0.0.1/captcha/edit.php';
  entries: DatasetN[];

get(name : string, prompt : string, description : string, id : string):Observable<DatasetN[]>{
  const data = {
    "name" : name,
"prompt" : prompt,
"description" : description,
"id" : id
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