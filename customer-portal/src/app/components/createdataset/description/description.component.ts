import { Component, OnInit, Input } from '@angular/core';
import { FormControl, FormGroup, NgModel } from '@angular/forms';
import { CreatedatasetService } from '../../../services/createdataset.service';
import { Dataset, DatasetTypes } from '../../../datatypes/dataset';

@Component({
  selector: 'app-description',
  templateUrl: './description.component.html',
  styleUrls: ['./description.component.css']
})
export class DescriptionComponent implements OnInit {
  @Input()
  dataset : Dataset;

  infoGroup = new FormGroup({
    title: new FormControl(),
    prompt: new FormControl(),
    description: new FormControl()
  });

  private _name : string = '';
  get name() : string{
    return this._name;
  }
  set name(value : string){
    this._name = value;
    this.dataset.name = this._name;
  }

  private _prompt : string = '';
  get prompt() : string {
    return this._prompt;
  }
  set prompt(value : string){
    this._prompt =  value;
    this.dataset.prompt = value;
  }

  private _description : string = '';
  get description() : string{
    return this._description;
  }
  set description(value : string){
    this._description = value;
    this.dataset.description = value;
  }

  private _settype : DatasetTypes;
  get settype() : DatasetTypes{
    return this._settype;
  }
  set settype(value : DatasetTypes){
    this._settype = value;
    this.dataset.settype = value;
  }

  constructor(private service: CreatedatasetService) { }

  ngOnInit() {
  }

  //Check if we need to resubmit and if the data has changed
  infoClick() {
    this.service.sendInfo(
      this.infoGroup.value['title'],
      this.infoGroup.value['prompt'],
      this.infoGroup.value['description']).subscribe();
  }
}
