import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-dataset-edit',
  templateUrl: './dataset-edit.component.html',
  styleUrls: ['./dataset-edit.component.css']
})
export class DatasetEditComponent implements OnInit {
  infoGroup = new FormGroup({
    title: new FormControl(),
    prompt: new FormControl(),
    description: new FormControl()
  });;

  constructor() { }

  ngOnInit() {
  }

}
