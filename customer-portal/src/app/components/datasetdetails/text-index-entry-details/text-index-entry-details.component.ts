import { Component, OnInit, Input, ElementRef } from '@angular/core';
import { CompoundIndexEntry } from '../../../datatypes/entries/CompoundTextIndexEntry';
import { trigger, transition, style, animate, state } from '@angular/animations';

@Component({
  selector: 'app-text-index-entry-details',
  templateUrl: './text-index-entry-details.component.html',
  styleUrls: ['./text-index-entry-details.component.css'],
})
export class TextIndexEntryDetailsComponent implements OnInit {
  @Input() entry : CompoundIndexEntry;
  @Input() expanded: boolean = false;
  constructor(private elementRef: ElementRef) { }

  ngOnInit() {
  }
}
