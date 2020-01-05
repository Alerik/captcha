import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { Text_Index_Entry } from '../../datatypes/entries/TextIndexEntry';

@Component({
  selector: 'app-text-entry-row',
  templateUrl: './text-entry-row.component.html',
  styleUrls: ['./text-entry-row.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class TextEntryRowComponent implements OnInit {
  @Input() entry : Text_Index_Entry;
  markup: string = '';
  tooltip_message : string = '';
  constructor() { }

  ngOnInit() {
    //console.log('it tis ' + this.entry);
    if(this.highlightable()){
      this.markup = this.entry.innertext.substring(0, this.entry.consensus_start)
      + '<mark>' + this.entry.innertext.substring(this.entry.consensus_start, 
        this.entry.consensus_end - this.entry.consensus_start) + '</mark>' +
        this.entry.innertext.substring(this.entry.consensus_end);
        this.tooltip_message = 'Consensus between ' + this.entry.consensus_start + ' and ' + this.entry.consensus_end;
    }
    else{
      this.tooltip_message = 'Not enough data for consensus';
    }
  }

  highlightable() : boolean{
    return this.entry.consensus_start != -1 && this.entry.consensus_end != -1;
  }
}
