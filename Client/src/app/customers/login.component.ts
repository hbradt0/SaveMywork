import { Component, Input, OnInit, ChangeDetectionStrategy } from '@angular/core';

import { IEntry, IUser } from '../shared/interfaces';
import { Sorter } from '../core/sorter';
import { TrackByService } from '../core/trackby.service';
import { DataService } from '../core/data.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  //When using OnPush detectors, then the framework will check an OnPush 
  //component when any of its input properties changes, when it fires 
  //an event, or when an observable fires an event ~ Victor Savkin (Angular Team)
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit {

  user: IUser = {
    email: '',
    password: '',
    username: '',
  };

  constructor(
    private dataService: DataService, private router: Router) { }

  ngOnInit() {
  }

  enter(username: string, password: string) {

    this.user.email = username;
    this.user.password = password;
    this.dataService.getUser(this.user.email)
      .subscribe((user: IUser) => {
        if (user) {
          this.router.navigate(['/customer']);
        }
        else {
        }
      },
        (err) => console.log(err));

  }
}
