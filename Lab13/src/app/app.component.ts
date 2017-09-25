import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  componentCounterValue:number = 0;
  showNewValue(value:number){
this.componentCounterValue = value;
  }
}
