import { Component, OnInit } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { DatasetTypes } from '../../datatypes/dataset';
import { NgForm, FormGroup, FormControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { globals } from '../../globals';
import { CreatedatasetService } from '../../services/createdataset.service';

@Component({
  selector: 'app-createdataset',
  templateUrl: './createdataset.component.html',
  styleUrls: ['./createdataset.component.css']
})
export class CreatedatasetComponent implements OnInit {
  radioChecked = false;
  setType : DatasetTypes;
  submitting: boolean = false;
  dataFile: File = null;
  fileRecieved : boolean = false;
  fileSent : boolean = false;
  lineCount : number = -1;
  
  infoGroup = new FormGroup({
    title: new FormControl(),
    prompt: new FormControl(),
    description: new FormControl()
  });;

  dataGroup = new FormGroup({
    data: new FormControl()
  });

  constructor(private router: Router, private service : CreatedatasetService) { }

  fileChange(files: FileList){
    this.dataFile = files.item(0);
  }

  ngOnInit() {
    console.log(globals['creation_id']);
    if(!globals['creation_id']){
      this.service.getProgressID().subscribe((id) => console.log(id));
    }
  }

  //Check if we need to resubmit and if the data has changed
  infoClick(){
    this.service.sendInfo(
      this.infoGroup.value['title'], 
      this.infoGroup.value['prompt'], 
      this.infoGroup.value['description']).subscribe();
  }

  dataClick(){
    this.fileSent = true;
    this.service.sendDataFile(this.dataFile)
    .subscribe((ret) => {this.fileRecieved  = true; this.fileSent = false; this.lineCount = ret;});
  }
}
