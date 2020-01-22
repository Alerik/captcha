import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { Dataset } from '../datatypes/dataset';
import { FormControl, Validators } from '@angular/forms';
import { EditdatasetService } from '../services/editdataset.service';

@Component({
  selector: 'app-overview-card',
  templateUrl: './overview-card.component.html',
  styleUrls: ['./overview-card.component.css'],
 // encapsulation: ViewEncapsulation.None
})
export class OverviewCardComponent implements OnInit {
  @Input() dataset: Dataset;

  titleControl : FormControl;
  editingTitle : boolean = false;

  descriptionControl : FormControl;
  editingDescription : boolean = false;

  promptControl : FormControl;
  editingPrompt : boolean = false;

  constructor(private updateService : EditdatasetService) { }

  ngOnInit() {
    this.titleControl =  new FormControl(this.dataset.name, [Validators.maxLength(80)]);
    this.descriptionControl = new FormControl(this.dataset.description);
    this.promptControl = new FormControl(this.dataset.prompt, [Validators.maxLength(100)]);
  }

  editTitle(){
    this.editingTitle = ! this.editingTitle;
    this.editingDescription = false;
    this.editingPrompt = false;
  }

  editDescription(){
    this.editingDescription = ! this.editingDescription;
    this.editingPrompt = false;
    this.editingTitle = false;
  }
  editPrompt(){
    this.editingPrompt = ! this.editingPrompt;
    this.editingDescription = false;
    this.editingTitle = false;
  }

  isDirty() : boolean{
    return this.titleControl.dirty || this.descriptionControl.dirty || this.promptControl.dirty;
  }

  isValid() : boolean{
    return this.titleControl.valid && this.descriptionControl.valid && this.promptControl.valid;
  }

  sendChanges(){
    if(this.isValid()){
      this.updateService.update(this.dataset.id, this.titleControl.value, this.promptControl.value, this.descriptionControl.value).subscribe();
    }
  }
}
