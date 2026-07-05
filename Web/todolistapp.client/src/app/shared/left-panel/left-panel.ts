import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { TodoService, TodoDto } from '../../services/todo';

@Component({
  selector: 'app-left-panel',
  templateUrl: './left-panel.html',
  styleUrls: ['./left-panel.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false
})
export class LeftPanelComponent implements OnInit {
  todos: TodoDto[] = [];
  todayCount = 0;
  weekCount = 0;
  monthCount = 0;
  constructor(private todoService: TodoService, private cd: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.load();

    this.todoService.refresh$.subscribe(() => {
      this.load();
    });
  }

  load(): void {
    this.todoService.getTodos().subscribe({
      next: (t: TodoDto[]) => {
        this.todos = t || [];
        this.calculateCounts();
        this.cd.markForCheck();
      },
      error: (err: any) => {
        console.error('Failed to load todos', err);
      }
    });
  }

  private calculateCounts(): void {
    const now = new Date();
    const startOfToday = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    const startOfWeek = new Date(startOfToday);
    const endOfWeek = new Date(startOfToday);
    startOfWeek.setDate(startOfWeek.getDate() - startOfWeek.getDay());
    endOfWeek.setDate(endOfWeek.getDate() - endOfWeek.getDay() + 7);

    this.todayCount = this.todos.filter(td => this.isSameDay(td.dueDate, startOfToday)).length;
    this.weekCount = this.todos.filter(td => this.isBetween(td.dueDate, startOfWeek, endOfWeek)).length;
    this.monthCount = this.todos.filter(td => {
      if (!td.dueDate) return false;
      const d = new Date(td.dueDate);
      return d.getFullYear() === now.getFullYear() && d.getMonth() === now.getMonth();
    }).length;
  }

  private isSameDay(dateStr?: string, ref?: Date): boolean {
    if (!dateStr) return false;
    const d = new Date(dateStr);
    const r = ref ?? new Date();
    return d.getFullYear() === r.getFullYear() && d.getMonth() === r.getMonth() && d.getDate() === r.getDate();
  }

  private isBetween(dateStr?: string, start?: Date, end?: Date): boolean {
    if (!dateStr) return false;
    const d = new Date(dateStr);
    if (start && d < start) return false;
    if (end && d > end) return false;
    return true;
  }
}
