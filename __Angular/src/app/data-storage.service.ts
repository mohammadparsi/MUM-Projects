import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/Rx';

import { EmployeeService } from './employee.service';
import { Employee2 } from './shared/models/Employee2';

@Injectable()
export class DataStorageService {
    constructor(private http: Http, private employeeService: EmployeeService) { }

    private apiUrl = "http://localhost:40784/api/employee/";
    
    storeRecipes() {
        const employees:Employee2[] = this.employeeService.getEmployees();
        for(let employee of employees){
            this.http.put(this.apiUrl, employee)
            .subscribe((response:Response)=>response.json(),
        error=>console.log(error));
        }
    }

    getRecipes() {
        this.http.get(this.apiUrl)
            .map(
            (response: Response) => {
                const employees: Employee2[] = response.json();
                return employees;
            }
            )
            .subscribe(
            (employees: Employee2[]) => {
                this.employeeService.setEmployees(employees);
            }
            );
    }
}
