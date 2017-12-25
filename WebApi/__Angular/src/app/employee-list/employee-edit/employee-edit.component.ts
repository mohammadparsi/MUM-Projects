import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Employee2 } from '../../shared/models/Employee2';
import { EmployeeService } from '../../employee.service';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css']
})
export class EmployeeEditComponent implements OnInit {

  employee: Employee2;
  @Input() editableEmployee: Employee2;
  @Output() employeeAdded = new EventEmitter<Employee2>();

  constructor(private service: EmployeeService) {}

  onAddEmployee() {
    this.service.addEmployeeWithObservable(this.employee)
      .subscribe((data) => {
        this.employeeAdded.emit(this.employee);
        this.onClearForm();
      }, error => console.error(error));

  }

  ngOnInit() {
    this.employee = new Employee2();
  }

  onClearForm() {
    this.employee = new Employee2();
  }

  onSaveEmployee() {
    this.service.editEmployeeWithObservable(this.employee)
      .subscribe((data) => {
        this.onClearForm();
      }, error => console.error(error));
  }

  setModel(employee) {
    this.employee = employee;
  }

}
