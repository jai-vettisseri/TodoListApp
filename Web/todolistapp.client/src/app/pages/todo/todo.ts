import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TodoService, TodoDto } from '../../services/todo';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.html',
  styleUrls: ['./todo.scss'],
  standalone: false,
})
export class Todo implements OnInit {
  form: FormGroup;
  id?: string;
  loading = false;
  error?: string;

  constructor(
    private fb: FormBuilder,
    private todoService: TodoService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const dateNotPastValidator = (control: AbstractControl): ValidationErrors | null => {
      const v = control.value;
      if (!v) return null;
      const dt = v instanceof Date ? new Date(v.getTime()) : new Date(v);
      if (isNaN(dt.valueOf())) return { invalidDate: true };
      const today = new Date();
      today.setHours(0, 0, 0, 0);
      dt.setHours(0, 0, 0, 0);
      return dt < today ? { pastDate: true } : null;
    };

    this.form = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(60)]],
      description: [''],
      dueDate: [null, [Validators.required, dateNotPastValidator]],
      status: ['Pending', Validators.required]
    });
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') ?? undefined;
    if (this.id && this.id !== 'new') {
      this.load(this.id);
    }
  }

  private load(id: string) {
    this.loading = true;
    this.todoService.getTodo(id).subscribe({
      next: (t: TodoDto) => {
        this.form.patchValue({
          title: t.title || '',
          description: t.description || '',
          dueDate: t.dueDate,
          status: t.status
        });
        this.loading = false;
      },
      error: (err: any) => {
        console.error(err);
        this.error = 'Failed to load todo';
        this.loading = false;
      }
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const payload: TodoDto = {
      id: this.id,
      title: this.form.value.title,
      description: this.form.value.description,
      dueDate: this.form.value.dueDate,
      status: this.form.value.status
    };

    this.loading = true;

    if (!this.id || this.id === 'new') {

      this.todoService.createTodo(payload).subscribe({
        next: () => { this.todoService.refreshTodos(); this.router.navigate(['/']) },
        error: (err: any) => { console.error(err); this.error = 'Create failed'; this.loading = false; }
      });
    } else {
      payload.id = this.id;
      this.todoService.updateTodo(this.id, payload).subscribe({
        next: () => { this.todoService.refreshTodos(); this.router.navigate(['/']) }, 
        error: (err: any) => { console.error(err); this.error = 'Update failed'; this.loading = false; }
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/']);
  }
}
