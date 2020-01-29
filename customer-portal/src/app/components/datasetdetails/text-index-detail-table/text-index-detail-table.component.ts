import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { DatasetEntriesService } from '../../../services/datasetentries.service';
import { TextIndexEntriesDataSource } from '../../../pages/datasetdetails/TextIndexEntriesDataSource';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-text-index-detail-table',
  templateUrl: './text-index-detail-table.component.html',
  styleUrls: ['./text-index-detail-table.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', maxHeight:'0px'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class TextIndexDetailTableComponent implements OnInit {
  @Input()
  id:string;

  @Input()
  entry_count:number;
  
  @ViewChild(MatPaginator, {static: true})
  paginator:MatPaginator;

  displayedColumns: string[] = ['innertext', 
  'querry_total', 'accuracy', 'certified'];
  dataSource : TextIndexEntriesDataSource;

  
  constructor(private entryService: DatasetEntriesService) { }

  ngOnInit() {
    this.dataSource = new TextIndexEntriesDataSource(this.id, this.entryService);
    this.dataSource.loadDatasets(0,20);
  }

  pageEvent(event?: PageEvent){
    let start = event.pageIndex * event.pageSize;
    this.dataSource.loadDatasets(start, event.pageSize);
  }

}
