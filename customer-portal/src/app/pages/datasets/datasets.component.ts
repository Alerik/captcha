import { Component, OnInit, ViewChild } from '@angular/core';
import { Dataset, DatasetTypes } from '../../datatypes/dataset'
import {DatasetsService} from '../../services/datasets.service'
import { DatasetsDataSource } from './DatasetsDataSource';
import { MatRadioChange } from '@angular/material/radio';

@Component({
  selector: 'app-datasets',
  templateUrl: './datasets.component.html',
  styleUrls: ['./datasets.component.css']
})
export class DatasetsComponent implements OnInit {
  displayedColumns: string[] = ['name', 'type', 'accuracy', 'annotations_total','entry_count','completion'];
  dataSource: DatasetsDataSource;
  settype : DatasetTypes;
  //const

  constructor(private dataservice : DatasetsService) { }

  ngOnInit() {
    this.dataSource =  new DatasetsDataSource(this.dataservice);
    this.dataSource.loadDatasets();
  }
  updateRadio($event : MatRadioChange){
      //this.settype = DatasetTypes[$event.value];
  }
}
