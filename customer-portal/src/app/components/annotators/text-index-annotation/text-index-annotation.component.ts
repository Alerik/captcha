import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { Text_Index_Annotation} from '../../../datatypes/annotations/TextIndexAnnotation'

@Component({
  selector: 'app-text-index-annotation',
  templateUrl: './text-index-annotation.component.html',
  styleUrls: ['./text-index-annotation.component.css']
})
export class TextIndexAnnotationComponent implements OnInit {
  @Input() annotation : Text_Index_Annotation;
  markup: string = '';
  tooltip_message : string = '';
  constructor() { }

  ngOnInit() {
    if(this.highlightable()){
      this.markup = this.annotation.innertext.substring(0, this.annotation.index_start)
      + '<mark>' + this.annotation.innertext.substring(this.annotation.index_start, 
        this.annotation.index_end - this.annotation.index_start) + '</mark>' +
        this.annotation.innertext.substring(this.annotation.index_start);
        this.tooltip_message = 'Consensus between ' + this.annotation.index_start + ' and ' 
        + this.annotation.index_end;
    }
    else{
      this.tooltip_message = 'Not enough data for consensus';
    }
  }

  highlightable() : boolean{
    return true;//return this.annotation.index != -1 && this.entry.consensus_end != -1;
  }
}
