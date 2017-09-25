import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable'
import {RequestOptions, Request, RequestMethod} from '@angular/http';
@Injectable()
export class DbService {

  constructor(private http: Http) { }
  students: any[] = [{ id: '1', name: 'Mohammad Parsi', stuID: '12345', email: 'moha@yahoo.com' }, { id: '2', name: 'Asaad Saad', stuID: '12111', email: 'asad@yahoo.com' }];
  getData() {
    return this.students;
  }
  private baseUrl = "http://jsonplaceholder.typicode.com/users/1";
  user: any;
  book:any;
  getUser() {
    this.http.request(this.baseUrl)

      .subscribe(res => this.user = res.json())
      ;
    if (this.user!==undefined) {
      return this.user;
    }
    //  return this.user;
    // .catch(error => {
    //     console.log(error);
    //     return Observable.throw(error);
    // });
  }

//   addBookWithObservable() {
//     let headerss = new Headers({ 'Content-Type': 'application/json' });
//     let options = new RequestOptions({ headers: headerss });
//     return this.http.post("this.url", this.book, options)
//                .subscribe(obj=>console.log(obj));
               
// } 
}
