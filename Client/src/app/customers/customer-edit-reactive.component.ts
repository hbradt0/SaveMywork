import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { DataService } from '../core/data.service';
import { IEntry, IUser } from '../shared/interfaces';
import { ValidationService } from '../shared/validation.service';

@Component({
  selector: 'customer-edit-reactive',
  templateUrl: './customer-edit-reactive.component.html'
})
export class CustomerEditReactiveComponent implements OnInit {

  customerForm: FormGroup = {} as FormGroup;
  get f(): { [key: string]: AbstractControl } {
    return this.customerForm.controls;
  }
  customer: IEntry = {
    entry: '',
    mydate: '',
    email: ''
  };
  errorMessage = '';
  deleteMessageEnabled: boolean = false;
  operationText: string = 'Insert';
  
  constructor(private router: Router, 
              private route: ActivatedRoute, 
              private dataService: DataService,
              private formBuilder: FormBuilder) { }

  ngOnInit() {
    let id = this.route.snapshot.params['id'];
    if (id !== '0') {
      this.operationText = 'Update';
      this.getCustomer(id);
    }

    this.buildForm();
  }

  getCustomer(id: string) {
      this.dataService.getCustomer(id)
        .subscribe((customer: IEntry) => {
          this.customer = customer;
          this.buildForm();
        },
        (err) => console.log(err));
  }

  buildForm() {
    this.customerForm = this.formBuilder.group({
        entry:    [this.customer.entry, Validators.required],
        date:   [this.customer.mydate, Validators.required],
      });
  }
  
  submit({ value, valid }: { value: IEntry, valid: boolean }) {
      
      value.id = this.customer.id;
      // var customer: ICustomer = {
      //   id: this.customer.id,
      // };

      if (value.id) {

        this.dataService.updateCustomer(value)
          .subscribe((customer: IEntry) => {
            if (customer) {
              this.router.navigate(['/customers']);
            }
            else {
              this.errorMessage = 'Unable to save customer';
            }
          },
          (err) => console.log(err));

      } else {

        this.dataService.insertCustomer(value)
          .subscribe((customer: IEntry) => {
            if (customer) {
              this.router.navigate(['/customers']);
            }
            else {
              this.errorMessage = 'Unable to add customer';
            }
          },
          (err) => console.log(err));
          
      }
  }
  
  cancel(event: Event) {
    event.preventDefault();
    this.router.navigate(['/customers']);
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
