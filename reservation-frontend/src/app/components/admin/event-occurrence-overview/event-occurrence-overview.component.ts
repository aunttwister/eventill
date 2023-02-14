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

  eventOccurrences = new Array<EventOccurrence>();
  eventOccurrencesData: EventOccurrence[] = [];
  dataSource!: MatTableDataSource<EventOccurrence>;
  columnsToDisplay = ['startTime', 'totalTicketCount', 'availableTicketCount', 'reservedTicketCount', 'soldTicketCount', 'activate'];
  innerDisplayedColumns = ['name', 'email', 'phoneNumber', 'ticketCount', 'paymentCompleted', 'delete'];
  expandedElement!: EventOccurrence | null;

  constructor(
    private eventService: EventService,
    private cd: ChangeDetectorRef,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    private reservationService: ReservationService) {
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
    console.log('changes are happen');
  }
  ngOnInit(): void {
    this.getEventOccurrences();
  }

  loadTableData() {
    this.eventOccurrences.forEach(eventOccurrence => {
      if (eventOccurrence.reservations && Array.isArray(eventOccurrence.reservations)) {
        this.eventOccurrencesData = [...this.eventOccurrencesData, {...eventOccurrence, reservations:(eventOccurrence.reservations)}];
      } else {
        this.eventOccurrencesData = [...this.eventOccurrencesData, eventOccurrence];
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

  getEventOccurrences(){
    return this.eventService.getEvent(1).subscribe(data =>
      {
        this.eventOccurrences = data.eventOccurrences;
        this.loadTableData();
        this.isLoaded = true;
      }
    )
  }

  onSave(eventOccurrenceId: number){
    let updatedReservations = new EditMultipleReservationsCommand();
    updatedReservations.reservations = new Array<EditReservationCommand>;
    let eventOccurrence = this.separateEventOccurrence(eventOccurrenceId);
    let arrayReservation = eventOccurrence.reservations as Array<Reservation>
    arrayReservation.forEach(r => {
      updatedReservations.reservations.push(new EditReservationCommand(
        r.id, r.name, r.email, r.phoneNumber, r.tickets.length, r.eventOccurrenceId, r.paymentCompleted, r.isDeleted))
    })

    this.reservationService.postEventReservations(updatedReservations).subscribe(
    res => {
      console.log(res)
    },
    err => {
      console.log(err)
    });
  }

  onDelete(eventOccurrenceId: number, reservationId: number){
    let eventOccurrence = this.separateEventOccurrence(eventOccurrenceId);
    eventOccurrence.reservations = eventOccurrence.reservations.filter(r => r.id != reservationId);
  }

  separateEventOccurrence(eventOccurrenceId: number){
    return this.dataSource.data.filter(eo => eo.id == eventOccurrenceId)[0];
  }
}
