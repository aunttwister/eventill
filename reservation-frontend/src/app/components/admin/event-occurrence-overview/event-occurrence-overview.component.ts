import { animate, state, style, transition, trigger } from '@angular/animations';
import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit, QueryList, SimpleChanges, ViewChild, ViewChildren } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTable, MatTableDataSource, MatTableModule } from '@angular/material/table';
import { EventOccurrence } from 'src/app/models/eventOccurrence';
import { Reservation } from 'src/app/models/reservation';
import { EventService } from 'src/app/services/http.services/event.service';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { DomSanitizer } from "@angular/platform-browser"
import { EditMultipleReservationsCommand } from 'src/app/request-commands/editMultipleReservationsCommand';
import { EditReservationCommand } from 'src/app/request-commands/editReservationCommand';
import { ReservationService } from 'src/app/services/http.services/reservation.service';
import { EventOccurrenceService } from 'src/app/services/http.services/event-occurrence.service';
import { EditEventOccurrenceCommand } from 'src/app/request-commands/editEventOccurrenceCommand';
import { EditMultipleEventOccurrencesCommand } from 'src/app/request-commands/editMultipleEventOccurrencesCommand';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-event-occurrence-overview',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatCheckboxModule],
  templateUrl: './event-occurrence-overview.component.html',
  styleUrls: ['./event-occurrence-overview.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class EventOccurrenceOverviewComponent implements OnInit {

  isLoaded = false;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild('outerSort', { static: true }) sort!: MatSort;
  @ViewChildren('innerSort') innerSort!: QueryList<MatSort>;
  @ViewChildren('innerTables') innerTables!: QueryList<MatTable<Reservation>>;

  now = new Date();
  eventOccurrencesView = new Array<EventOccurrence>();
  eventOccurrencesData: EventOccurrence[] = [];
  dataSource!: MatTableDataSource<EventOccurrence>;
  columnsToDisplay = ['startTime', 'totalTicketCount', 'availableTicketCount', 'reservedTicketCount', 'soldTicketCount', 'activate'];
  columnsToDisplayComplex = [{
    header: 'Početak',
    value: 'startTime'
    }, {
      header: 'Ukupno karata',
      value: 'totalTicketCount'
    }, { 
      header: 'Dostupno karata',
      value: 'availableTicketCount'
    }, { 
      header: 'Rezervisano karata',
      value: 'reservedTicketCount',
    }, { 
      header: 'Prodato karata',
      value: 'soldTicketCount'
    }, { 
      header: 'Aktivno',
      value: 'activate'
    }];
  innerDisplayedColumns = ['name', 'email', 'phoneNumber', 'ticketCount', 'paymentCompleted', 'delete'];
  innerDisplayedColumnsComplex = [
    {
      header: 'Ime i prezime',
      value: 'name' 
    }, { 
      header: 'Email',
      value: 'email' 
    }, {
      header: "Broj telefona",
      value: 'phoneNumber'
    }, {
      header: 'Broj karata',
      value: 'ticketCount'
    }, {
      header: 'Plaćanje izvršeno',
      value: 'paymentCompleted'
    }, {
      header: 'Obriši',
      value: 'delete'
    }];
  expandedElement!: EventOccurrence | null;

  eventOccurrenceChanges = new Array<EventOccurrence>();
  reservationsChanges = new Array<Reservation>();

  constructor(
    private eventService: EventService,
    private cd: ChangeDetectorRef,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    private reservationService: ReservationService,
    private eventOccurrenceService: EventOccurrenceService,
    private notificationService: NotificationService) {
      matIconRegistry.addSvgIcon(
        `excel_icon`,
        this.domSanitizer.bypassSecurityTrustResourceUrl(`../../../../assets/icons8-microsoft-excel.svg`)
      )
      matIconRegistry.addSvgIcon(
        `delete_icon`,
        this.domSanitizer.bypassSecurityTrustResourceUrl(`../../../../assets/icons8-close.svg`)
      )

  }

  ngOnChanges(changes: SimpleChanges): void {
  }
  ngOnInit(): void {
    let eventId = 3;
    this.getEventOccurrences(eventId);
  }

  loadTableData() {
    this.eventOccurrencesView.forEach(eventOccurrenceView => {
      if (eventOccurrenceView.reservations && Array.isArray(eventOccurrenceView.reservations)) {
        this.eventOccurrencesData = [...this.eventOccurrencesData, {...eventOccurrenceView, reservations:(eventOccurrenceView.reservations)}];
      } else {
        this.eventOccurrencesData = [...this.eventOccurrencesData, eventOccurrenceView];
      }
    });

    this.dataSource = new MatTableDataSource(this.eventOccurrencesData);

    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  toggleRow(eventOccurrence: EventOccurrence) {
    eventOccurrence.reservations && eventOccurrence.reservations.length ? (this.expandedElement = this.expandedElement === eventOccurrence ? eventOccurrence : eventOccurrence) : null;
    this.cd.detectChanges();
    this.innerTables.forEach((table, index) => (table.dataSource as MatTableDataSource<Reservation>).sort = this.innerSort.toArray()[index]);
  }

  getEventOccurrences(id: number){
    return this.eventService.getEvent(id).subscribe(data =>
      {
        this.eventOccurrencesView = data.eventOccurrences;
        this.loadTableData();
        this.isLoaded = true;
      }
    )
  }

  onSaveOuterTable(){
    let updateEventOccurrences = new EditMultipleEventOccurrencesCommand();
    updateEventOccurrences.eventOccurrences = new Array<EditEventOccurrenceCommand>();
    this.eventOccurrenceChanges.forEach(eo =>{
      updateEventOccurrences.eventOccurrences.push(new EditEventOccurrenceCommand(
        eo.id, eo.startTime, eo.eventId, eo.isActive, eo.isDeleted, eo.tickets, 
        eo.reservations.length != 0 ? eo.reservations : this.reservationsChanges.filter(r => r.eventOccurrenceId === eo.id)
      ))
    })

    this.eventOccurrenceService.postMultipleEventOccurrences(updateEventOccurrences).subscribe(
      res => {
        this.notificationService.showSuccess('Event Occurrences successfully updated! Refresh the page to see the results.', 'Success!');
      },
      err => {
        this.notificationService.showError(err.error.error.message, 'StatusCode: ' + err.status);
      });
  }

  onSaveInnerTable(){
    let updateReservations = new EditMultipleReservationsCommand();
    updateReservations.reservations = new Array<EditReservationCommand>;
    this.reservationsChanges.forEach(r => {
      updateReservations.reservations.push(new EditReservationCommand(
        r.id, r.name, r.email, r.phoneNumber, r.tickets.length, r.eventOccurrenceId, r.paymentCompleted, r.isDeleted, r.tickets, r.userId))
    })

    this.reservationService.postEventReservations(updateReservations).subscribe(
      res => {
        this.notificationService.showSuccess('Reservations successfully updated! Refresh the page to see the results.', 'Success!');
      },
      err => {
        this.notificationService.showError(err.error.error, 'StatusCode: ' + err.status);
      });
  }

  onDelete(reservation: Reservation){
    reservation.isDeleted = true;
    this.reservationsChanges.push(reservation);

    let eventOccurrence = this.separateEventOccurrence(reservation.eventOccurrenceId);
    eventOccurrence.reservations = eventOccurrence.reservations.filter(r => r.id != reservation.id);
  }

  separateEventOccurrence(eventOccurrenceId: number){
    return this.dataSource.data.filter(eo => eo.id == eventOccurrenceId)[0];
  }

  onCheckboxChangeEventOccurrence(eventOccurrence: EventOccurrence){
    let currentValue = eventOccurrence.isActive;
    eventOccurrence.isActive = !currentValue;

    let index = this.eventOccurrenceChanges.findIndex(eo => eo.id === eventOccurrence.id);
    index === -1 ? this.eventOccurrenceChanges.push(eventOccurrence) : this.eventOccurrenceChanges[index] = eventOccurrence;
  }
  onCheckboxChangeReservation(reservation: Reservation){
    let currentValue = reservation.paymentCompleted;
    reservation.paymentCompleted = !currentValue;

    let index = this.reservationsChanges.findIndex(r => r.id === reservation.id);
    index === -1 ? this.reservationsChanges.push(reservation) : this.reservationsChanges[index] = reservation;
  }

  pastDate(date: Date)
  {
    console.log(this.now > date)
    return this.now > date;
  }
}
