// developed by Mohammad Parsi. The following is the main component of the app that contains other components.
import { Component } from '@angular/core';
import {AuthService} from "./auth/auth.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  componentCounterValue:number = 0;
  pro;
  showNewValue(value:number){
    this.componentCounterValue = value;
  }
  constructor(public auth:AuthService) {
    auth.handleAuthentication();
    this.pro = auth.getPro();

  }
}
