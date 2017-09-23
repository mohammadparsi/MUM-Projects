import { Directive, HostBinding, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[mycolor]'
})
export class MycolorDirective {
  @HostBinding('style.color') color = 'red';
  @Output() colorChange: EventEmitter<any> = new EventEmitter();
  @HostListener('click') chooseNewColor() {
    this.color = 'blue';
    
    this.colorChange.emit(this.color);
  }
  constructor() { }

}
