import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header';
import { LeftPanelComponent } from './left-panel/left-panel';
import { ErrorPopupComponent } from './error-popup/error-popup';



@NgModule({
  declarations: [
    HeaderComponent,
    LeftPanelComponent
  ],
  imports: [
    CommonModule
  ]
  ,
  exports: [HeaderComponent, LeftPanelComponent]
})
export class SharedModule { }
