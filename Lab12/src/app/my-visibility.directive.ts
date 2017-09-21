import { Directive,Input,ElementRef,AfterViewInit,Renderer } from '@angular/core';

@Directive({
  selector: '[myvisibility]'
})
export class MyVisibilityDirective implements AfterViewInit {

  constructor(public el: ElementRef, public renderer:Renderer) {

    
  }

  ngAfterViewInit(): void  {
    
    if (this.myvisibility) {
      
            this.renderer.setElementStyle(this.el.nativeElement, 'display', 'none');
            // this.el.nativeElement.style.color = 'red';
          }
  }

  @Input() myvisibility: boolean;

}

