import { Component, OnInit } from '@angular/core';
import { DbService } from '../db/db.service';
@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {

  items: any[];

  constructor(private service: DbService) {
    this.items = service.getData();
  }

  ngOnInit() {
  }

}
