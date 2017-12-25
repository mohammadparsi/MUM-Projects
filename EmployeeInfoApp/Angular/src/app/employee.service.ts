

import { Injectable, EventEmitter } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable'
import { RequestOptions, Response, Request, RequestMethod, Headers } from '@angular/http';
import { Employee2 } from './shared/models/Employee2';
import 'rxjs/Rx';

@Injectable()
export class EmployeeService {
  private employees: Employee2[] = new Array();
  employeesChanges = new EventEmitter<Employee2[]>();
  constructor(private http: Http) { }

  private apiUrl = "http://localhost:40784/api/employee/";

  getEmployees(){
    return this.employees;
  }

  setEmployees(employees: Employee2[]){

  }

  getEmployeeById(id) {
    return this.http.get(this.apiUrl + id)
      .map(
      (response: Response) => {
        const data = response.json();
        return data;
      }
      );
  }

  deleteEmployeeById(id) {
    return this.http.delete(this.apiUrl + id)
      .map(
      (response: Response) => {
        const data = response.json();
        return data;
      }
      );
  }

  addEmployeeWithObservable(employee): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    return this.http.post(this.apiUrl, employee);

  }

  getAll() {
    return this.http.get(this.apiUrl)
      .map(
      (response: Response) => {
        const data = response.json();
        return data;
      }
      );
  }

  editEmployeeWithObservable(employee): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    return this.http.put(this.apiUrl + employee.Id, employee, options)
      .map(
      (response: Response) => {
        const data = response.json();
        return data;
      }
      );

  }


}













