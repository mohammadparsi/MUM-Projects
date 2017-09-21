import { Directive, Renderer, ElementRef, HostBinding } from '@angular/core';

@Directive({
  selector: '[upper]'
})
export class UpperDirective {
  @HostBinding('class') x = 'upperCase';
  
  constructor(private el: ElementRef) {
    //this.el.nativeElement.textContent = this.el.nativeElement.textContent.toUpperCase();
  }

}
