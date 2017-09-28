// developed by Mohammad Parsi. The following is the service class to connect to back-end to save and retrieve data.

import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable'
import { RequestOptions, Request, RequestMethod, Headers } from '@angular/http';
import {AuthHttp} from "angular2-jwt";
@Injectable()
export class DbService {

  constructor(private http: Http, private authHttp:AuthHttp) { }
  students: any[] = [{ id: '1', name: 'Mohammad Parsi', stuID: '12345', email: 'moha@yahoo.com' }, { id: '2', name: 'Asaad Saad', stuID: '12111', email: 'asad@yahoo.com' }];
  getData() {
    return this.students;
  }
  private baseUrl = "http://jsonplaceholder.typicode.com/users/1";
  user: any;
  book: any;
  getUser() {
    this.http.request(this.baseUrl)

      .subscribe(res => this.user = res.json())
      ;
    if (this.user !== undefined) {
      return this.user;
    }

  }

  addBookWithObservable(book: any): Observable<any> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    return this.authHttp.post("http://localhost:3004/api/postBook", book, options);

  }

  getAllBooks(){
    return this.authHttp.get("http://localhost:3004/api/getBooks");
  }
  getBookByISBN(isbn){
    return this.authHttp.get('http://localhost:3004/api/ISBN/'+isbn)
  }

  getBookById(id:any){
    return this.authHttp.request("http://localhost:3004/api/book/"+id);
  }

  addBookReview(review) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    console.log(review.bookId);
    return this.authHttp.post("http://localhost:3004/api/addReview", review);

  }

  getReviewThatNeedsEditing (){
    return this.authHttp.request("http://localhost:3004/api/getEditReview");
  }

  postEditedReview(input){
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    console.log('input '+input.email);
    return this.authHttp.post("http://localhost:3004/api/editReview", input);

  }


}
