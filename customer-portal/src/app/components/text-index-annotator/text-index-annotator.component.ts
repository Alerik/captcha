import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import {KeepHtmlPipe } from '../../keep-html.pipe';

@Component({
  selector: 'app-text-index-annotator',
  templateUrl: './text-index-annotator.component.html',
  styleUrls: ['./text-index-annotator.component.css'],
  encapsulation:ViewEncapsulation.None
})
export class TextIndexAnnotatorComponent implements OnInit {
  @Input()
  text: string;
  words: number[][];
  highlightText : string;

  selection_start = -1;
  selection_end = -1;
  mouseInside: boolean = false;
  constructor() { }

  ngOnInit() {
    //There is some scoping issue preventing 'this from being used in the callback
    document.onselectionchange = () => this.onSelectionChange(this);
    this.initWords();
  }

  initWords(): void {
    this.words = [];
    let re = /[^\s]+/g;
    let m;
    do {
      m = re.exec(this.text);
      if (m) {
        this.words.push([m['index'], m['index'] + m[0].length]);
      }
    } while (m);
  }

  findWord(index : number) : number[]{
    for(let word of this.words){
      if(index >=word[0] && index < word[1]){
        return word;
      }
    }
    return [];
  }

  onMouseEnter(): void {
    this.mouseInside = true;
  }
  onMouseLeave(): void {
    this.mouseInside = false;
  }

  onSelectionChange(t: TextIndexAnnotatorComponent): void {
    if (t.mouseInside) {
      let selection = window.getSelection();
      let start = selection.anchorOffset;
      let end = selection.focusOffset;

      if(start >= end){
        let temp = start;
        start = end;
        end = temp;
       }

      let s_word = this.findWord(start);
      let e_word = this.findWord(end);

      if(s_word.length > 0){
        start = s_word[0];
      }
      if(e_word.length > 0){
        end = e_word[1];
      }

      this.selection_start = start;
      this.selection_end = end;

      this.updateHighlight();
    }
  }

  updateHighlight() : void{
    if(this.selection_start == -1 || this.selection_end == 1){
      this.highlightText = '';
    }
    else{
      let marklen = '<mark></mark>'.length;
      this.highlightText = 
      this.text.substring(0, this.selection_start)
      + '<mark>' + this.text.substring(this.selection_start, this.selection_end)
      + '</mark>' + this.text.substring(this.selection_end);
    }
  }
}
