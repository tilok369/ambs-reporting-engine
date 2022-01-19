import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InputOutputService {
  selectedWidget: EventEmitter<any>;
  constructor() {
    this.selectedWidget = new EventEmitter<any>();
  }
  sendSelectedWidget(selectedWidget) {
    this.selectedWidget.emit(selectedWidget);
  }
}
