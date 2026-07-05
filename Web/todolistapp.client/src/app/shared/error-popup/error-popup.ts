import { Component, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-error-popup',
  templateUrl: './error-popup.html',
  styleUrls: ['./error-popup.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class ErrorPopupComponent implements OnDestroy {
  message: string | null = null;
  private sub: Subscription;

  constructor(private notification: NotificationService, private cd: ChangeDetectorRef) {
    this.sub = this.notification.message$.subscribe(m => {
      this.message = m;
      try { this.cd.detectChanges(); } catch {}
    });
  }

  close() {
    this.notification.clear();
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
