import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { EmployeeService } from './employee.service';
import { HttpModule, RequestOptions } from "@angular/http";
import { MyRoutes } from "./app.routing";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { Employee2 } from './shared/models/Employee2';
import { HeaderComponent } from './header/header.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeEditComponent } from './employee-list/employee-edit/employee-edit.component';
import { EmployeeRowComponent } from './employee-list/employee-row/employee-row.component';
import { HomeComponent } from './home/home.component';




@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    EmployeeListComponent,
    EmployeeEditComponent,
    EmployeeRowComponent,
    HomeComponent,

  ],
  imports: [
    BrowserModule, HttpModule, FormsModule, ReactiveFormsModule, MyRoutes
  ],
  providers: [EmployeeService, Employee2],
  bootstrap: [AppComponent]
})
export class AppModule { }
