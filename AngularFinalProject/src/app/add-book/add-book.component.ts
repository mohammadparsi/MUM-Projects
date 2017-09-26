import { Component, OnInit } from '@angular/core';
import { FormsModule,FormBuilder, ReactiveFormsModule,FormGroup,FormControl,Validators } from '@angular/forms';
import { DbService } from '../db/db.service';
@Component({
  selector: 'add-book',
  templateUrl: './add-book.component.html',
  // template:`<form [formGroup]="myForm" (ngSubmit)="onSubmit()">
  
  // <input type="text"  
  // formControlName="title">
  // <input type="text"  
  // formControlName="author">
  // <input type="text"  
  // formControlName="ISBN">
  
  // <div *ngIf="!myForm.controls['title'].valid">Title Required</div>
  
  // <div *ngIf="!myForm.controls['author'].valid">Author Required</div>
  // <div *ngIf="!myForm.controls['ISBN'].valid">ISBN Required</div> 
  // <button type="submit" [disabled]="!myForm.valid">Submit</button>
  // <button type="button" (click)="onGetData()" >Get Data</button> 
  // {{retrievedObject|json}}
  // </form>`,
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {
myForm:FormGroup;
dbService:DbService;
book = {title:'',author:'',ISBN:''};
  constructor(fb:FormBuilder, private service: DbService) {
    this.myForm=fb.group({'title':[this.book.title,Validators.required],
    'author':[this.book.author,Validators.required],'ISBN':[this.book.ISBN,Validators.required]});

    this.dbService= service; 
    // console.log(service.getUser());
   }
// onGetData():void{
//   // this.user.name=this.dbService.getUser().username;
//   // this.user.email=this.dbService.getUser().email;
  
//   if (this.dbService.getUser()!==undefined) {
//     this.myForm.setValue({
//       'email': this.dbService.getUser().email, 
//       'name': this.dbService.getUser().name,
//       'post': "post............"
//     });
//     this.user.name=this.dbService.getUser().name;
//     this.user.email=this.dbService.getUser().email;
//   }
// }
   onSubmit():void{
     this.dbService.addBookWithObservable(this.myForm.value)
     .subscribe((data)=>data.json(), error=>console.error(error));
  
     
   }

  ngOnInit() {
  }

}
