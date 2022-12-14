import { Component, Input, OnInit, ChangeDetectionStrategy, NgModule } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { IEntry, IUser, current } from '../shared/interfaces';
import { Sorter } from '../core/sorter';
import { TrackByService } from '../core/trackby.service';
import { DataService } from '../core/data.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
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

  public currentUser: string = '';

  constructor(
    private dataService: DataService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }
  
   checkoutForm = this.formBuilder.group({
    email: '',
    password: ''
  });
  
  enter(): void{
    this.dataService.getUser(this.user.email)
        .subscribe((user: IUser) => {
          if (user) {
            this.currentUser = current;
            this.router.navigate(['/customers']);
          }
          else {
          }
        },
          (err) => console.log(err));

    }
}
