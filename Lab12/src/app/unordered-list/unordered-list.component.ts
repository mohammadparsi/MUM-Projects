import { Component, Input, OnInit, ElementRef, Directive, Renderer, AfterViewInit } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
// import {
//     Component,
//     Directive,
//     Renderer,
//     ElementRef,
//     NgModule,
//     Input,
//     Output,
//     EventEmitter
// } from '@angular/core';
// import {BrowserModule} from '@angular/platform-browser';

// @Directive({
//   selector: '[upper]'
// })
// export class UpperDirective {

//   constructor(private el: ElementRef) {
//     this.el.nativeElement.textContent = this.el.nativeElement.textContent.toUpperCase();
//   }

// }

// @Directive({
//   selector: '[myvisibility]'
// })
// export class MyVisibilityDirective implements AfterViewInit {

//   constructor(public el: ElementRef) {

//     // if (this.myvisibility) {
      
//     //         // this.renderer.setElementStyle(this.el.nativeElement, 'display', 'none');
//     //         this.el.nativeElement.style.color = 'red';
//     //       }
//     this.el.nativeElement.style.color = 'red';
//   }

//   ngAfterViewInit(): void  {
//     this.el.nativeElement.style.color = 'red';
//   }

//   // @Input() myvisibility: boolean=true;

// }


@Component({
  selector: 'list',
  template: `
    <ul>
    <input type ="text" value="hello" upper />
    <li  upper>must be uppercase</li>
    <li  mycolor (colorChange)="color = $event">my color: {{color}}</li>
    <h1 [myvisibility]="val">This must be hidden</h1>
    <li *ngFor="let item of items"  >{{item}}</li>
      
    </ul>
     <p>
     
     
  `,
  styleUrls: ['../app.component.css']
})
export class UnorderedListComponent {
  val:boolean = true;
  color='';
  @Input() items: string[];
  constructor() { }
  

}
