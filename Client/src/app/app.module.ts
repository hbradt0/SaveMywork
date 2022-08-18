import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent }  from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CoreModule }   from './core/core.module';
import { SharedModule }   from './shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    BrowserModule, 
    AppRoutingModule,
    FormsModule,
    FormBuilder,
    RouterModule,
    CoreModule,   //Singleton objects
    SharedModule  //Shared (multi-instance) objects
  ],
  declarations: [ AppComponent, AppRoutingModule.components ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
