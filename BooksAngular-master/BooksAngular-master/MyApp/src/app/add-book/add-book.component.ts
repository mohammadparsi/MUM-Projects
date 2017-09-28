import { Component, OnInit } from '@angular/core';
import { FormsModule,FormBuilder, ReactiveFormsModule,FormGroup,FormControl,Validators } from '@angular/forms';
import { DbService } from '../db/db.service';
import {Router} from "@angular/router";
//This component has been developed by Mohammad Parsi Esfahani. The component is related to the use-case "add a book".
@Component({
  selector: 'add-book',
  templateUrl: './add-book.component.html',

  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {
myForm:FormGroup;
dbService:DbService;
value;
book = {title:'',author:'',ISBN:'',reviews:[]};
  constructor(fb:FormBuilder, private service: DbService, private router:Router) {
    this.myForm=fb.group({'title':[this.book.title,Validators.required],
    'author':[this.book.author,Validators.required],'ISBN':[this.book.ISBN,Validators.required],'reviews':[this.book.reviews]});

    this.dbService= service;

   }

   onSubmit():void{
     this.dbService.addBookWithObservable(this.myForm.value)
     .subscribe((data)=>{this.value = data.json();
       console.log(this.value);
       this.router.navigate(['/viewBook/', this.value.insertedIds[0]])}, error=>console.error(error));



   }

  ngOnInit() {
  }

}
