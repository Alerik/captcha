import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-text-multi-annotator',
  templateUrl: './text-multi-annotator.component.html',
  styleUrls: ['./text-multi-annotator.component.css'],
})
export class TextMultiAnnotatorComponent implements OnInit {
  @Input()
  text : string;
  @Input()
  choices : string[];

  selection : string;
  
  constructor() { }

  ngOnInit() {
  }

  setSelection(selection : string){
    this.selection = selection;
  }

}
