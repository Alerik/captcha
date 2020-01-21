import { Component, OnInit, Input } from '@angular/core';
import { Dataset } from '../datatypes/dataset';

@Component({
  selector: 'app-overview-card',
  templateUrl: './overview-card.component.html',
  styleUrls: ['./overview-card.component.css']
})
export class OverviewCardComponent implements OnInit {
  @Input() dataset: Dataset;

  constructor() { }

  ngOnInit() {
  }

}
