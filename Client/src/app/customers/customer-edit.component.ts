import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { DataService } from '../core/data.service';
import { IEntry, IUser, current } from '../shared/interfaces';

@Component({
  selector: 'customer-edit',
  templateUrl: './customer-edit.component.html'
})
export class CustomerEditComponent implements OnInit {

  customer: IEntry = {
    entry: '',
    mydate: '',
    email: ''
  };

  errorMessage = '';
  deleteMessageEnabled = false;
  operationText = 'Insert';
  
  constructor(private router: Router, 
              private route: ActivatedRoute, 
    private dataService: DataService) { }

  ngOnInit() {
    let id = this.route.snapshot.params['id'];
    if (id !== '0') {
      this.operationText = 'Update';
      this.getCustomer(id);
    }

 }

  getCustomer(id: string) {
      this.dataService.getCustomer(id)
        .subscribe((customer: IEntry) => {
          this.customer = customer;
        },
        (err: any) => console.log(err));
  }
  
  submit() {
    this.customer.email = current;
      if (this.customer.id) {

        this.dataService.updateCustomer(this.customer)
          .subscribe((customer: IEntry) => {
            if (customer) {
              this.router.navigate(['/customers']);
            } else {
              this.errorMessage = 'Unable to save customer';
            }
          },
          (err: any) => console.log(err));

      } else {

        this.dataService.insertCustomer(this.customer)
          .subscribe((customer: IEntry) => {
            if (customer) {
              this.router.navigate(['/customers']);
            }
            else {
              this.errorMessage = 'Unable to add customer';
            }
          },
          (err: any) => console.log(err));
          
      }
  }
  
  cancel(event: Event) {
    event.preventDefault();
    this.router.navigate(['/']);
  }

  delete(event: Event) {
    event.preventDefault();
    this.dataService.deleteCustomer(this.customer.id as string)
        .subscribe((status: boolean) => {
          if (status) {
            this.router.navigate(['/customers']);
          }
          else {
            this.errorMessage = 'Unable to delete customer';
          }
        },
        (err) => console.log(err));
  }

}
