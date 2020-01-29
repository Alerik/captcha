import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-full-text-multi-annotator',
  templateUrl: './full-text-multi-annotator.component.html',
  styleUrls: ['./full-text-multi-annotator.component.css']
})
export class FullTextMultiAnnotatorComponent implements OnInit {
  @Input()
  text:string;
  @Input()
  prompt:string;
  @Input()
  choices:string[];

  constructor() { }

  ngOnInit() {
  }

}
