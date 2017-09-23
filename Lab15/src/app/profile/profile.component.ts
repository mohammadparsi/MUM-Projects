import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DbService } from '../db/db.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  id: string;
  student: any;
  students: any[];
  constructor(private route: ActivatedRoute, private service: DbService) {
    route.params.subscribe(params => { this.id = params['id'] });
    this.students = service.getData();
    for (var item of this.students) {
      if (item.id === this.id) {
       this.student=item; 
      }
    }
  }

    ngOnInit() {
    }

  }
