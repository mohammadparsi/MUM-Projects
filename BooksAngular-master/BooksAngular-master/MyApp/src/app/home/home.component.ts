
// developed by Mohammad Parsi. The following is the component corresponding to the use-case "view all books".
import { Component, OnInit } from '@angular/core';
import {DbService} from "../db/db.service";
import "rxjs/add/operator/filter";
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{
  message='';
  data;
  books;
  constructor(private service:DbService) {
    service.getAllBooks().map(res=>res.json()).subscribe(res=>{this.books=res});
  }


  searchText='';
  refreshPage(e){
    console.log("the following is .."+e)
    this.searchText=e;
    this.service.getAllBooks().map(res=>res.json()).map(objects=>{return objects.filter(obj=>{
      var re = obj.title+"";
      var val= e+"";
      return re.includes(val);

    })})
      .subscribe(results=>{this.books=results;
      if (results.length==0){this.nothingFound=true;}});
  }

  nothingFound:boolean;
}
