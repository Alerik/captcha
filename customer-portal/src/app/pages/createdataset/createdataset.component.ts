import { Component, OnInit } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { DatasetTypes } from '../../datatypes/dataset';
import { NgForm, FormGroup, FormControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { globals } from '../../globals';
import { CreatedatasetService } from '../../services/createdataset.service';
import { Text_Index_Entry } from '../../datatypes/entries/TextIndexEntry';
import { RandomentriesService } from '../../services/randomentries.service';

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

  seedEntries : Text_Index_Entry[];
  seedCount = 10;
  seedLoaded = false;
  seedLoading = false;
  
  infoGroup = new FormGroup({
    title: new FormControl(),
    prompt: new FormControl(),
    description: new FormControl()
  });

  dataGroup = new FormGroup({
    data: new FormControl()
  });

  constructor(private router: Router, private service : CreatedatasetService,
    private rndService : RandomentriesService) { }

  fileChange(files: FileList){
    this.dataFile = files.item(0);
  }

  ngOnInit() {
    if(!globals['creation_id']){
      this.service.getProgressID().subscribe((id) => console.log(id));
    }
    // this.seedEntries = [
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'This is line # one'
    //   },
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'5 pounds green beans'
    //   },
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'One fast laptop'
    //   },
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'Black sphynx, guards thee'
    //   },
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'This is not a line of code'
    //   },
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'I am about to eat a pizza'
    //   },
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'Sadness every doth draws near thou'
    //   },
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'This is line # eight'
    //   },
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'Notebook, fridge, counter, glasses, and journal'
    //   },
    //   {
    //     id_dataset:'', consensus_start:-1, consensus_end:-1, certified:false,
    //     querry_total:0, accuracy:0, id:'', complete:false, innertext:'My glass is empty'
    //   }]
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
    let inst = this;
    this.seedLoaded = false;
    this.seedLoading = true;
    this.service.sendDataFile(this.dataFile)
    .subscribe((ret) => 
    {this.fileRecieved  = true; 
      this.fileSent = false; 
      this.lineCount = ret['lines'];
      this.seedEntries = ret['entries'];
      console.log(this.lineCount);
      this.seedLoading = false;
      this.seedLoaded = true;
    });
  }

  getSeedEntries() {
    this.seedLoaded = false;
    this.seedLoading = true;
    this.rndService.get(globals['creation_id'], 10)
    .subscribe((ret) => 
    {
      this.seedEntries = ret;
      
    });
  }

}
