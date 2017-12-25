import { Component, OnInit, ViewChild } from '@angular/core';
import { Employee2 } from '../shared/models/Employee2';
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';
import { EmployeeService } from '../employee.service';
@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: []
})
export class EmployeeListComponent implements OnInit {

  employees: Employee2[] = new Array();
  @ViewChild('editor') employeeEditComponent: EmployeeEditComponent;

  constructor(private service: EmployeeService) {
  }

  ngOnInit() {
    this.service.getAll().subscribe((data:any[]) => this.employees = data );
  }

  onEmployeeAdded(employee: Employee2) {
    var clone = Object.assign({}, employee);
    this.employees.push(clone);
  }

  onEditClicked(employee) {
    this.employeeEditComponent.setModel(employee);
  }

  onEmployeeDeleted(Id) {
    var removedIndex = 0;
    for (var i = 0; i < this.employees.length; i++) {
      if (this.employees[i].Id == Id) {
        removedIndex = i;
      }
    }

    this.employees.splice(removedIndex, 1);
  }

}
