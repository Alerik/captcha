import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatSliderModule} from '@angular/material/slider';
import {MatSidenavModule} from '@angular/material/sidenav'
import {MatToolbarModule} from '@angular/material/toolbar'
import {MatListModule} from '@angular/material/list';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatTableModule} from '@angular/material/table'; 
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner'; 
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { DatasetsComponent } from './pages/datasets/datasets.component';
import { AccountComponent } from './pages/account/account.component'; 
import {MatCardModule} from '@angular/material/card'; 
import { HttpClientModule } from '@angular/common/http';
import { DatasetdetailsComponent } from './pages/datasetdetails/datasetdetails.component';
import {MatButtonModule} from '@angular/material/button'; 
import {MatInputModule} from '@angular/material/input';
import {MatRadioModule} from '@angular/material/radio';
import { CreatedatasetComponent } from './pages/createdataset/createdataset.component'; 
import {MatChipsModule} from '@angular/material/chips';
import {MatPaginatorModule} from '@angular/material/paginator'; 
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { TextEntryRowComponent } from './components/text-entry-row/text-entry-row.component';
import {MatTooltipModule} from '@angular/material/tooltip';
import { TextIndexAnnotationComponent } from './components/text-index-annotation/text-index-annotation.component';
import { TextIndexEntryDetailsComponent } from './components/text-index-entry-details/text-index-entry-details.component'; 
import {MatCheckboxModule} from '@angular/material/checkbox'; 
import {MatStepperModule} from '@angular/material/stepper'; 
import {MatProgressBarModule} from '@angular/material/progress-bar'; 
import {MatBadgeModule} from '@angular/material/badge'; 
import {MatIconModule} from '@angular/material/icon';
import {MatIconRegistry} from '@angular/material/icon';
import {DomSanitizer} from '@angular/platform-browser';
import {MatTabsModule} from '@angular/material/tabs';
import { BillingComponent } from './pages/billing/billing.component';
import { DatasetEditComponent } from './pages/dataset-edit/dataset-edit.component';
import { TextIndexAnnotatorComponent} from './components/text-index-annotator/text-index-annotator.component';
import { KeepHtmlPipe } from './keep-html.pipe';
import { FullTextIndexAnnotatorComponent } from './components/full-text-index-annotator/full-text-index-annotator.component'; 

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    DatasetsComponent,
    AccountComponent,
    DatasetdetailsComponent,
    CreatedatasetComponent,
    TextEntryRowComponent,
    TextIndexAnnotationComponent,
    TextIndexEntryDetailsComponent,
    BillingComponent,
    DatasetEditComponent,
    TextIndexAnnotatorComponent,
    KeepHtmlPipe,
    FullTextIndexAnnotatorComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatSliderModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatGridListModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatCardModule,
    MatPaginatorModule,
    MatButtonModule,
    MatInputModule,
    MatRadioModule,
    MatChipsModule,
    MatTooltipModule,
    MatCheckboxModule,
    MatStepperModule,
    ReactiveFormsModule,
    MatProgressBarModule,
    MatBadgeModule,
    MatIconModule,
    MatTabsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private iconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer){
    iconRegistry.addSvgIcon('warning', '/assets/warning-24px.svg');
    iconRegistry.addSvgIcon('review', '/assets/pageview-24px.svg');
    iconRegistry.addSvgIcon('settings', '/assets/settings_applications-24px.svg');
    iconRegistry.addSvgIcon('edit', '/assets/edit-24px.svg');
  }
 }
