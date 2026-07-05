import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Todo } from './pages/todo/todo';

const routes: Routes = [
  { path: '', component: Home },
  { path: 'todo/new', component: Todo },
  { path: 'todo/:id', component: Todo },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
