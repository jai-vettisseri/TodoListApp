import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { TodoService, TodoDto } from '../../services/todo';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-home',
  templateUrl: './home.html',
  styleUrls: ['./home.scss'],
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, RouterModule, FormsModule, MatTableModule, MatButtonModule, MatProgressSpinnerModule]
})
export class Home implements OnInit {
  todos: TodoDto[] = [];
  loading = true;
  error?: string;
  filterText = '';
  filtered: TodoDto[] = [];
  showCompleted = false;
  displayedColumns: string[] = ['title', 'description', 'dueDate', 'status', 'actions'];

  constructor(private todoService: TodoService, private router: Router, private cd: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.todoService.getTodos(this.showCompleted).subscribe(
      (data) => {
        this.todos = data || [];
        this.loading = false;
        this.applyFilter();
        this.cd.markForCheck();
      },
      (err) => {
        this.error = 'Failed to load todos';
        console.error(err);
        this.loading = false;
      }
    );
  }

  onShowCompletedChange(): void {
    this.load();
  }

  addNew(): void {
    this.router.navigate(['/todo', 'new']);
  }

  edit(id?: string): void {
    if (!id) return;
    this.router.navigate(['/todo', id]);
  }

  delete(id?: string): void {
    if (!id) return;
    if (!confirm('Delete this todo?')) return;
    this.todoService.deleteTodo(id).subscribe(
      () => { this.todoService.refreshTodos(); this.load() },
      (err) => { console.error(err); alert('Delete failed'); }
    );
  }

  applyFilter(): void {
    const f = (this.filterText || '').toLowerCase();
    if (!f) {
      this.filtered = this.todos;
      this.cd.markForCheck();
      return;
    }
    this.filtered = this.todos.filter(t => (t.title || '').toLowerCase().includes(f));
    this.cd.markForCheck();
  }
}
