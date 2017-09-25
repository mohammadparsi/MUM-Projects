import { Component, OnInit } from '@angular/core';
import { DbService } from '../db/db.service';
@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  // items: any[];
  // constructor(private service: DbService) {
  //   this.items = service.getPosts();
  // }

  ngOnInit() {
  }

}
