import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { users, Row1, Row2 } from '../datatypes/generated';
@Injectable({
	providedIn: 'root'
})
export class MainService {
	constructor(private http: HttpClient) {
	}

	insert_url : string = 'http://127.0.0.1/captchamain/insert';
	insert (arg1 : string, arg2 : number) : Observable<Row1>{
		const params = new HttpParams().set('arg1',arg1).set('arg2',arg2);
		return this.http.post<Row1>(this.insert_url, params);
	}

	concat_url : string = 'http://127.0.0.1/captchamain/concat';
	concat (first : string, second : string, third : string) : Observable<Row1>{
		const params = new HttpParams().set('first',first).set('second',second).set('third',third);
		return this.http.get<Row1>(this.concat_url, params);
	}
}