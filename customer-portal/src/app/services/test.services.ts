import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
	providedIn: 'root'
})
export class TestService {
	constructor(private http: HttpClient) {
	}

	addPerson_url : string = 'http://127.0.0.1/captcha/test/addPerson.php';
	addPerson (first_name : string, last_name : string) : Observable<string>{
		const params = new HttpParams().set('first_name',first_name).set('last_name',last_name);
		return this.http.post<string>(this.addPerson_url, params);
	}

	getStatus_url : string = 'http://127.0.0.1/captcha/test/getStatus.php';
	getStatus () : Observable<string>{
		const params = new HttpParams();
		return this.http.get<string>(this.getStatus_url, {params: params});
	}
}