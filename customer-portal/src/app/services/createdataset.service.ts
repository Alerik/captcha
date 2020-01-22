import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { globals } from '../globals';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CreatedatasetService {
  startCreateUri : string = 'http://127.0.0.1/captcha/createDataset/startCreate.php';
  setInfoUri : string = 'http://127.0.0.1/captcha/createDataset/setInfo.php';
  sendDataFileUri : string = 'http://127.0.1/captcha/postEntries.php'

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
      map((json) => {
        let res = JSON.parse(json.toString());
        return {'lines': res['data']['entry_count'], 'entries': res['data']['entries']}
      })); 
  }
}