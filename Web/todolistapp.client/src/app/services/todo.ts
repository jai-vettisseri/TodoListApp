import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';

export interface TodoDto {
  id?: string;
  title: string;
  description?: string;
  dueDate?: string; // ISO string expected
  duration?: number;
  status?: string;
}

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private apiUrl = '/api/Todo';

  constructor(private http: HttpClient) { }

  private readonly refreshSubject = new Subject<void>();

  refresh$ = this.refreshSubject.asObservable();

  refreshTodos(): void {
    this.refreshSubject.next();
  }

  getTodos(includeCompleted: boolean = false): Observable<TodoDto[]> {
    return this.http.get<TodoDto[]>(`${this.apiUrl}?includeCompleted=${includeCompleted}`);
  }

  createTodo(payload: TodoDto) {
    return this.http.post<string>(this.apiUrl, payload); 
  }

  updateTodo(id: string, payload: TodoDto) {
    return this.http.put<void>(`${this.apiUrl}/${id}`, payload);
  }

  deleteTodo(id: string) {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getTodo(id: string) {
    return this.http.get<TodoDto>(`${this.apiUrl}/${id}`);
  }
}
