import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/admin/dashboard/dashboard.component';
import { ConfirmationPageComponent } from './components/user/confirmation-page/confirmation-page.component';
import { CreateReservationComponent } from './components/user/create-reservation/create-reservation.component';
import { EventOverviewComponent } from './components/user/event-overview/event-overview.component';

const routes: Routes = [
  { path: 'new-reservation/:eventName/:eventOccurrenceId', component: CreateReservationComponent },
  //{ path: 'events-view', component: EventsViewComponent },
  { path: 'event-overview', component: EventOverviewComponent },
  { path: 'confirmation', component: ConfirmationPageComponent },
  { path: 'admin', component: DashboardComponent },
  { path: '**', component: EventOverviewComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }