import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  private _message$ = new BehaviorSubject<string | null>(null);

  get message$(): Observable<string | null> {
    return this._message$.asObservable();
  }

  showError(message: string) {
    console.log('NotificationService.showError:', message);
    this._message$.next(message);
  }

  clear() {
    this._message$.next(null);
  }
}
