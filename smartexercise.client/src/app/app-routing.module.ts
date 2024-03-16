import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerDetailsComponent } from './components/customer-details/customer-details.component';
import { AddCustomerComponent } from './components/add-customer/add-customer.component';

const routes: Routes = [
  { path: '', redirectTo: '/', pathMatch: 'full' }, // Redirect to home page
  { path: 'customers', component: CustomerListComponent },
  { path: 'customers/:id', component: CustomerDetailsComponent },
  { path: 'add-customer', component: AddCustomerComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
