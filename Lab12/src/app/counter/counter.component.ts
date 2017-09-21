import { Component, OnInit,Output,Input, EventEmitter } from '@angular/core';

@Component({
  selector: 'counter',
  template: `
    
    <button (click)="increase()">+</button>
    {{value}}
    <button (click)="decrease()">-</button>
  `,
  styles: [],
  inputs:['value'],
  outputs:['counterChange']
})
export class CounterComponent {
value:number;
// get CounterValue():number{
//   return this.value;
// }
@Input() set CounterValue(value:number){
  this.value=value;

}
counterChange: EventEmitter<number>;
  constructor() {
    this.value = 0;
    this.counterChange = new EventEmitter();
   }

  //  changeValue():void{
  //    this.counterChange.emit(this.value);
  //  }
increase(){
  this.value = this.value+1;
  this.counterChange.emit(this.value);
return false;
}

decrease(){
  this.value = this.value-1;
  this.counterChange.emit(this.value);
return false;
}

}
