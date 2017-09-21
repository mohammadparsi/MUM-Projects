import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { UnorderedListComponent } from './unordered-list/unordered-list.component';
import { UpperDirective } from './upper.directive';
import { MyVisibilityDirective } from './my-visibility.directive';
import { MycolorDirective } from './mycolor.directive';

@NgModule({
  declarations: [
    AppComponent,
    CounterComponent,
    UnorderedListComponent,
    UpperDirective,
    MyVisibilityDirective,
    MycolorDirective
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
