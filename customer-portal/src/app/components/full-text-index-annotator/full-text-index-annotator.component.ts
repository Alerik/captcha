import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-full-text-index-annotator',
  templateUrl: './full-text-index-annotator.component.html',
  styleUrls: ['./full-text-index-annotator.component.css']
})
export class FullTextIndexAnnotatorComponent implements OnInit {
  @Input()
  text:string;
  @Input()
  prompt:string;
  
  constructor() { }

  ngOnInit() {
  }

}
