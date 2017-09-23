import { Component, OnInit } from '@angular/core';
import { FormsModule,FormBuilder, ReactiveFormsModule,FormGroup,FormControl,Validators } from '@angular/forms';
import { DbService } from '../db/db.service';
@Component({
  selector: 'app-form',
  // templateUrl: './form.component.html',
  template:`<form [formGroup]="myForm" (ngSubmit)="onSubmit()">
  <input type="text"  
  formControlName="email">
  
  <input type="text"  
  formControlName="name">
  <input type="text"  
  formControlName="post">
  
  <div *ngIf="!myForm.controls['email'].valid">Required</div>
  
  <div *ngIf="!myForm.controls['name'].valid">Required</div>
  <div *ngIf="!myForm.controls['post'].valid">Minimum 10</div> 
  <button type="submit" [disabled]="!myForm.valid">Submit</button>
  <button type="button" (click)="onGetData()" >Get Data</button> 
  {{retrievedObject|json}}
  </form>`,
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
myForm:FormGroup;
dbService:DbService;
user = {name:'mohammad',email:'mohammadp@roc.com',post:'post123444566'};
  constructor(fb:FormBuilder, private service: DbService) {
    this.myForm=fb.group({'email':[this.user.email,Validators.required],
    'name':[this.user.name,Validators.required],'post':[this.user.post,Validators.minLength(10)]});

    this.dbService= service; 
    // console.log(service.getUser());
   }
onGetData():void{
  // this.user.name=this.dbService.getUser().username;
  // this.user.email=this.dbService.getUser().email;
  
  if (this.dbService.getUser()!==undefined) {
    this.myForm.setValue({
      'email': this.dbService.getUser().email, 
      'name': this.dbService.getUser().name,
      'post': "post............"
    });
    this.user.name=this.dbService.getUser().name;
    this.user.email=this.dbService.getUser().email;
  }
}
   onSubmit():void{
     console.log(this.myForm.value);
  
     
   }

  ngOnInit() {
  }

}
