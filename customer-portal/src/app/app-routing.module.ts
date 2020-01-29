import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountComponent } from './pages/account/account.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { DatasetsComponent } from './pages/datasets/datasets.component';
import {DatasetdetailsComponent} from './pages/datasetdetails/datasetdetails.component'
import { AppComponent } from './app.component';
import { CreatedatasetComponent } from './pages/createdataset/createdataset.component';
import { BillingComponent } from './pages/billing/billing.component';

const routes: Routes = [
  {path: '', component: DashboardComponent},
  {path: 'datasets', component: DatasetsComponent},
  {path: 'dashboard', component: DashboardComponent},
  {path: 'account', component: AccountComponent},
  {path:'billing', component: BillingComponent},
  {path: 'datasets/:name/:id', component: DatasetdetailsComponent},
  {path: 'createdataset', component: CreatedatasetComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
