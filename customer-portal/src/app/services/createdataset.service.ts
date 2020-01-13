import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { globals } from '../globals';
import { catchError, map } from 'rxjs/operators';
import { Text_Index_Entry } from '../datatypes/entries/TextIndexEntry';
import { Text_Index_Annotation } from '../datatypes/annotations/TextIndexAnnotation';

@Injectable({
  providedIn: 'root'
})
export class CreatedatasetService {
  startCreateUri : string = 'http://127.0.0.1/captcha/createDataset/startCreate.php';
  setInfoUri : string = 'http://127.0.0.1/captcha/createDataset/setInfo.php';
  sendDataFileUri : string = 'http://127.0.1/captcha/postEntries.php'

  seedEntries : Text_Index_Entry[];
  lines : number;

  creation_id:string;

  constructor(private http: HttpClient) 
  {
    if(globals['creation_id']){
      this.creation_id = globals['creation_id'];
    }
  }

  //Gets the UUID for building this thing
  getProgressID() : Observable<string>{
    const data = {
      'id_customer' : globals['user_id']
    };

    return this.http.post(this.startCreateUri, JSON.stringify(data),
    {headers: globals['json_header']}).pipe(
      map((res) =>{
        this.creation_id = res['data']['creation_id'];
        globals['creation_id'] = this.creation_id;
        return this.creation_id;
      }));
  }

  //Sends step1 info
  sendInfo(title:string, prompt:string, description:string) : Observable<boolean>{
    const data = {
      'id_customer' : globals['user_id'],
      'id_creation' : this.creation_id,
      'title' : title,
      'prompt' : prompt,
      'description' : description
    }; 

    return this.http.post(this.setInfoUri, JSON.stringify(data),
    {headers: globals['json_header']}).pipe(
      map((res) =>{
        return true;
      }));
  }

  sendDataFile(dataFile : File) : Observable<any>{
      const formData : FormData = new FormData();
      formData.append('entry_file', dataFile, dataFile.name);
      formData.append('id_customer', globals['user_id']);
      formData.append('id_dataset', this.creation_id);
      return this.http.post(this.sendDataFileUri, formData).pipe(
      map((res) => 
      {
        //I think we need to make this an object because we can't have a JSON header sent
        //So we need to parse to object so it can be turned into the correct datatype
        this.lines = JSON.parse(res['data']['entry_count']);
        this.seedEntries = JSON.parse(res['data']['entries']);

        const ret = {
          lines : this.lines,
          entries : this.seedEntries
        }
        return ret;
      }));
  }

  // sendSeedAnnotations(annotations: Text_Index_Annotation[]) : Observable<bool>{
  //   return false;
  // }
}
