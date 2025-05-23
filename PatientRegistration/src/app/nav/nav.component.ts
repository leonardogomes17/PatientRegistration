import { Component, NgModule, OnInit } from '@angular/core';
import { AppComponent } from '../app.component';
import { BrowserModule } from '@angular/platform-browser';

// @Component({
//   selector: 'app-nav',
//   templateUrl: './nav.component.html',
//   styleUrls: ['./nav.component.css']
// })

@NgModule({
    imports: [BrowserModule],
    //exports: [],
    //declarations: [AppComponent],
    //providers: [],
    bootstrap: [AppComponent]

    // selector: 'app-nav',
    // templateUrl: './nav.component.html',
    // styleUrls: ['./nav.component.css']
})


export class NavComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}

