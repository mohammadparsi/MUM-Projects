// developed by Mohammad Parsi.
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import {DbService} from "./db/db.service";
import {Http, HttpModule, RequestOptions} from "@angular/http";
import { AddBookComponent } from './add-book/add-book.component';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MyRoutes} from "./app.routing";
import {ViewBookInfoComponent} from "./view-book-info/view-book-info.component";
import { ReviewComponent } from './review/review.component';
import {AuthService} from "./auth/auth.service";
import {AuthGuard} from "./guard/auth.guard";
import {AuthHttp} from "angular2-jwt";
import {authHttpServiceFactory} from "./AuthHttpService";
import { FilterPipe } from './filter.pipe';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AddBookComponent,
    ViewBookInfoComponent,
    ReviewComponent,
    FilterPipe
  ],
  imports: [
    BrowserModule, HttpModule, FormsModule, ReactiveFormsModule, MyRoutes
  ],

  providers: [DbService, AuthService,  AuthGuard, {
    provide: AuthHttp,
    useFactory: authHttpServiceFactory,
    deps: [Http, RequestOptions]
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
