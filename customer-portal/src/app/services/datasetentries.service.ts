import { Injectable } from '@angular/core';

import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import {Dataset, DatasetTypes} from '../datatypes/Dataset';
import { Text_Index_Entry } from '../datatypes/entries/TextIndexEntry';
import { Text_Index_Annotation } from '../datatypes/annotations/TextIndexAnnotation';
import {CompoundIndexEntry} from '../datatypes/entries/CompoundTextIndexEntry';
import { globals } from '../globals';

@Injectable({
  providedIn: 'root'
})
export class DatasetEntriesService {

  constructor(private http: HttpClient) {  
  }

  baseUrl = 'http://localhost/captcha/getEntries.php';
  entries: Text_Index_Entry[];
  annotations: Text_Index_Annotation[];

get(id_dataset:string, start:number, count:number):Observable<CompoundIndexEntry[]>{
  const data = {
    "id_customer" : globals['user_id'],
    "id_dataset" : id_dataset,
    "start" : start,
    "count" : count
  };

  return this.http.post(this.baseUrl, JSON.stringify(data),
  {headers: globals['json_header']}).pipe(
    map((res) => {
      if('error' in res){
        console.error("Error occured: ", res['error']);
        return;
      }
      this.entries = res['data']['entries'];
      this.annotations = res['data']['annotations'];
      return this.getCompounds(this.entries, this.annotations);
    }),
    catchError(this.handleError));
}
private handleError(error: HttpErrorResponse){
  console.log(error);
  return throwError('Error! something went wrong.');
}

getCompounds(entries : Text_Index_Entry[], annotations : Text_Index_Annotation[]) : CompoundIndexEntry[]{
  let compounds : CompoundIndexEntry[] = [];

  for(let entry of entries){
    let annots : Text_Index_Annotation[] = [];
    for(let j = 0; j < annotations.length; j++){
      if(annotations[j].id_entry == entry.id){
        annots.push(annotations[j]);
      }
    }
    let compound = new CompoundIndexEntry(entry, annots);
    //compound.entry = entry;
    //compound.annotations = annots;
    compounds.push(compound);
  }
  // console.log(entries);
  // console.log(compounds);
  return compounds;
}
}
