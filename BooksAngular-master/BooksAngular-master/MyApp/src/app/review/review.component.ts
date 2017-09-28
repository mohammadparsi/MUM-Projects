// developed by Mohammad Parsi. The following is a component which will be used as a child component(<reviewBox>) in
// the parent component "view-book-info". The following component will be used as a box for each review when showing list of reviews
// in the component "view-book-info".

import {Component, Input, OnInit} from '@angular/core';
import {AuthService} from "../auth/auth.service";
import {DbService} from "../db/db.service";

@Component({
  selector: 'reviewBox',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})
export class ReviewComponent implements OnInit {
@Input() commentText;
  @Input() commenterName;
@Input() userId;
@Input() bookId;
@Input() reviewId;

isEditable =false;

message;

  constructor(public auth:AuthService, private service: DbService) { }

  ngOnInit() {
  }

  onEdit(){
    this.isEditable=true;
    console.log("Book Id working: "+this.bookId);
  }
  onAccept(){

    this.isEditable=false;
    let input={'email':this.auth.getPro().email, 'review':this.message, 'id': this.bookId, 'reviewId':this.reviewId};
    console.log(this.commentText+' message '+this.message);

    this.service.postEditedReview(input).subscribe(data=>data);
  }

}
