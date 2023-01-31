import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { Observable, Observer } from 'rxjs';

const componentsConfig = [
  {
    component: () =>
      import('../event-occurrence-overview/event-occurrence-overview.component').then(
        (it) => it.EventOccurrenceOverviewComponent
      ),
    inputs: {
      name: 'EventOccurrenceOverview',
    },
  }
]

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['../../../app.component.css', './dashboard.component.css']
})
export class DashboardComponent {
  @ViewChild('container', {read: ViewContainerRef})
  container!: ViewContainerRef;

  asyncTabs: Observable<string[]>;

  constructor() {
    this.asyncTabs = new Observable((observer: Observer<string[]>) => {
      setTimeout(() => {
        observer.next([
          'Događaji',
          'Upravljanje događajima',
          'Statistika',
        ]);
      }, 1000);
    });
  }

  createComponentsBasedOnConfig() {
    this.container.clear();
    componentsConfig.forEach(async (componentConfig) => {
      const componentInstance = await componentConfig.component();
      const componentRef = this.container.createComponent(componentInstance);
    });
  }
}
