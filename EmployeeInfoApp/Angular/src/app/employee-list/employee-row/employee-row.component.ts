import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Employee2 } from '../../shared/models/Employee2';
import { EmployeeService } from '../../employee.service';

@Component({
  selector: 'app-employee-row',
  templateUrl: './employee-row.component.html',
  styleUrls: ['./employee-row.component.css']
})
export class EmployeeRowComponent implements OnInit {

  @Input() employeeInput;
  @Output() editClicked = new EventEmitter<Employee2>();
  @Output() employeeDeleted = new EventEmitter<string>();

  constructor(private service:EmployeeService) { }

  onClickEdit() {
    this.editClicked.emit(this.employeeInput);
  }

  onClickDelete(){
    this.service.deleteEmployeeById(this.employeeInput.Id)
    .subscribe((data) => {
        this.employeeDeleted.emit(this.employeeInput.Id);
    }
    , error => console.error(error));
  }

  ngOnInit() {
  }

}
