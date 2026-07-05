import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Home } from './pages/home/home';
import { Todo } from './pages/todo/todo';
import { SharedModule } from './shared/shared-module';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorInterceptorProvider } from './interceptors/error.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorPopupComponent } from './shared/error-popup/error-popup';

@NgModule({
  declarations: [
    AppComponent,
    Todo
  ],
  imports: [
    BrowserModule, BrowserAnimationsModule, HttpClientModule, ReactiveFormsModule,
    AppRoutingModule, SharedModule,
    MatTableModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatIconModule, MatDatepickerModule, MatNativeDateModule,
    Home,
    ErrorPopupComponent
  ],
  providers: [
    ErrorInterceptorProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
