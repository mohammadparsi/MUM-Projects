import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { DbService } from './db/db.service';
import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { UnorderedListComponent } from './unordered-list/unordered-list.component';
import { HomeComponent } from './home/home.component';
import { StudentsComponent } from './students/students.component';
import { UpperDirective } from './upper.directive';
import { MyVisibilityDirective } from './my-visibility.directive';
import { MycolorDirective } from './mycolor.directive';
import {RouterModule,Routes} from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { ActivatedRoute } from '@angular/router';
import { PostComponent } from './post/post.component';
import { FormComponent } from './form/form.component';
import { FormsModule, ReactiveFormsModule,FormGroup,FormControl } from '@angular/forms';
import{HttpModule}from '@angular/http';
import { AddBookComponent } from './add-book/add-book.component';
import { ViewBookInfoComponent } from './view-book-info/view-book-info.component';


const myRoutes:Routes = [{path:'home',component:HomeComponent}
,{path:'students',component:StudentsComponent},{path:'profile/:id',component:ProfileComponent},
{path:'posts',component:PostComponent},{path:'addBook',component:AddBookComponent},
{path:'viewBook',component:ViewBookInfoComponent}];

@NgModule({
  declarations: [
    AppComponent,
    CounterComponent,
    UnorderedListComponent,
    UpperDirective,
    MyVisibilityDirective,
    MycolorDirective,
    HomeComponent,
    StudentsComponent,
    ProfileComponent,
    PostComponent,
    FormComponent,
    AddBookComponent,
    ViewBookInfoComponent,

    
  ],
  imports: [
    BrowserModule,RouterModule.forRoot(myRoutes),
    FormsModule, 
    ReactiveFormsModule,HttpModule
  ],
  providers: [DbService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
