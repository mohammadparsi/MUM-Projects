
// developed by Mohammad Parsi. The following is the component corresponding to the use-case "view book reviews".
//the following component will show all information of a book plus all it's reviews in a page. In additon, the logged-in user
//can add a new review and edit their reviews in the page.

import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {DbService} from "../db/db.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import "rxjs/add/operator/map";
import {AuthService} from "../auth/auth.service";


@Component({
  selector: 'view-book-info',
  templateUrl: './view-book-info.component.html',
  styleUrls: ['./view-book-info.component.css','./vendor/bootstrap/css/bootstrap.min.css'
,'./css/blog-post.css']
})
export class ViewBookInfoComponent implements OnInit {
  myForm:FormGroup;
  email:string;
  id;
  emailNol = "nolawew103@gmail.com"
  book:any={};

  constructor(fb:FormBuilder, private route:ActivatedRoute, private service:DbService, private auth:AuthService) {
    this.myForm=fb.group({
      'reviewText':['',Validators.required]
          });
    route.params.subscribe(params=>{this.id=params['id']});
    service.getBookById(this.id).subscribe(res=>{this.book=res.json();console.log(this.book.reviews[0].email)});

  }
  onSubmit(){

    let input ={"email": this.auth.getPro().email, "bookId":this.book._id, "reviewText": this.myForm.controls['reviewText'].value,"reviewId":this.book.reviews.length+1};

    this.service.addBookReview(input).map(data=>data.json()).subscribe(data=>this.book=data);


    this.myForm.controls['reviewText'].setValue('');
  }


  ngOnInit() {
  }

}
