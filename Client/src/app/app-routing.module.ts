import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CustomersComponent } from './customers/customers.component';
import { CustomersGridComponent } from './customers/customers-grid.component';
import { CustomerEditComponent } from './customers/customer-edit.component';
import { CustomerEditReactiveComponent } from './customers/customer-edit-reactive.component';
import { CreateLoginComponent } from './customers/createlogin.component';
import { LoginComponent } from './customers/login.component'; //'./createlogin.component.html'

const routes: Routes = [
  { path: 'customers', component: CustomersComponent },
  { path: 'login', component: LoginComponent },
  { path: 'createlogin', component: CreateLoginComponent },
  //{ path: 'customers/:id', component: CustomerEditComponent},
  { path: 'customers/:id', component: CustomerEditReactiveComponent },
  { path: '**', pathMatch:'full', redirectTo: '/customers' } //catch any unfound routes and redirect to home page
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
    static components = [ CustomersComponent, CustomersGridComponent, CustomerEditComponent, CustomerEditReactiveComponent, CreateLoginComponent, LoginComponent ];
}
