import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { DatasetinfoService } from '../../services/datasetinfo.service';
import { Dataset } from '../../datatypes/Dataset';
import { TextIndexEntriesDataSource } from './TextIndexEntriesDataSource';
import { DatasetEntriesService } from '../../services/datasetentries.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {TextEntryRowComponent} from '../../components/text-entry-row/text-entry-row.component';
import { Text_Index_Entry } from '../../datatypes/entries/TextIndexEntry';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { CompoundIndexEntry } from '../../datatypes/entries/CompoundTextIndexEntry';

@Component({
  selector: 'app-datasetdetails',
  templateUrl: './datasetdetails.component.html',
  styleUrls: ['./datasetdetails.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', maxHeight:'0px'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class DatasetdetailsComponent implements OnInit {
  displayedColumns: string[] = ['innertext', 
 'querry_total', 'accuracy', 'certified'];
  id_dataset: string;
  name: string;
  private sub: any;
  selectedIndex:number;
  dataset: Dataset;
  dataSource: TextIndexEntriesDataSource;
  fileList : FileList;
  expandedElement : CompoundIndexEntry | null;
  notifications: Notification[];
  
  @ViewChild(MatPaginator, {static: true}) paginator:MatPaginator;
  constructor(private route: ActivatedRoute, private infoService: DatasetinfoService,
    private entryService: DatasetEntriesService, private http:HttpClient) { }


  ngOnInit() {
    this.sub = this.route.params.subscribe(params =>{
      this.id_dataset = params['id'];
      this.name = params['name'];
    });
    this.dataSource = new TextIndexEntriesDataSource(this.id_dataset,this.entryService);
    this.dataSource.loadDatasets(0, 20);
    this.infoService.getInfo(this.id_dataset).subscribe(
      dataset => 
      {
        this.dataset = dataset; 
        this.notifications = this.infoService.notifications;});
  }

  pageEvent(event?: PageEvent){
    let start = event.pageIndex * event.pageSize;
    this.dataSource.loadDatasets(start, event.pageSize);
  }

  ngOnDestroy(){
    this.sub.unsubscribe();
  }

  fileChange(event){
    this.fileList = event.target.files;
  }
  uploadClick(event){
    if(this.fileList.length > 0){
      let file: File = this.fileList[0];
      let formData:FormData = new FormData();
      formData.append('entry_file', file, file.name);
      formData.append('id_customer', '3fe3d6a6-5ac4-4075-9026-0b51e64f4140');
      formData.append('id_dataset', this.id_dataset);
      let headers = new HttpHeaders({
      //  'Content-Type' : 'multipart/form-data',
        'Accept' : 'application/json'
      });
      headers.append('Content-Type', 'multipart/form-data');
        headers.append('Accept', 'application/json');

        this.http.post('http://localhost/captcha/postEntries.php', formData, {headers:headers})
        .subscribe(
          data => console.log('submitted'),
        error => console.log(error));
    }
  }
}
