import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DataFilterService } from '../core/data-filter.service';
import { DataService } from '../core/data.service';
import { IEntry, IPagedResults, IUser, current } from '../shared/interfaces';
import { LoginComponent } from './login.component';

@Component({ 
  selector: 'customers', 
  templateUrl: './customers.component.html'
})
export class CustomersComponent implements OnInit {

  title = '';
  currentUser: string = "";
  customers: IEntry[] = [];
  filteredCustomers: IEntry[] = [];

  totalRecords: number = 0;
  pageSize: number = 10;

  constructor(private router: Router, 
              private dataService: DataService,
    private dataFilter: DataFilterService) { }
  
  ngOnInit() {
    this.title = 'Journal Entries';
    this.getCustomersPage(1);
    this.currentUser = current;
   }

  filterChanged(filterText: string) {
    if (filterText && this.customers) {
        let props = ['entry', 'date', 'email'];
        this.filteredCustomers = this.dataFilter.filter(this.customers, props, filterText);
    }
    else {
      this.filteredCustomers = this.customers;
    }
  }

  pageChanged(page: number) {
    this.getCustomersPage(page);
  }

  getCustomersPage(page: number) {
    this.dataService.getCustomersPage((page - 1) * this.pageSize, this.pageSize)
        .subscribe((response: IPagedResults<IEntry[]>) => {
          this.customers = this.filteredCustomers = response.results;
          this.totalRecords = response.totalRecords;
        },
        (err: any) => console.log(err),
        () => console.log('getCustomersPage() retrieved customers'));
  }

}
