import { animate, state, style, transition, trigger } from '@angular/animations';
import { CommonModule } from '@angular/common';
import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit, QueryList, SimpleChanges, ViewChild, ViewChildren } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTable, MatTableDataSource, MatTableModule } from '@angular/material/table';
import { EventOccurrence } from 'src/app/models/eventOccurrence';
import { Reservation } from 'src/app/models/reservation';
import { EventService } from 'src/app/services/event.service';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  selector: 'app-event-occurrence-overview',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatIconModule, MatCheckboxModule],
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
  columnsToDisplay = ['startTime', 'totalTicketCount', 'availableTicketCount', 'reservedTicketCount', 'soldTicketCount'];
  innerDisplayedColumns = ['name', 'email', 'phoneNumber', 'ticketCount', 'paymentCompleted'];
  expandedElement!: EventOccurrence | null;

  constructor(
    private eventService: EventService,
    private cd: ChangeDetectorRef) {

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
        this.eventOccurrencesData = [...this.eventOccurrencesData, {...eventOccurrence, reservations: new MatTableDataSource(eventOccurrence.reservations)}];
      } else {
        this.eventOccurrencesData = [...this.eventOccurrencesData, eventOccurrence];
      }
    });

    this.dataSource = new MatTableDataSource(this.eventOccurrencesData);

    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  toggleRow(eventOccurrence: EventOccurrence) {
    console.log(eventOccurrence)
    eventOccurrence.reservations && (eventOccurrence.reservations as MatTableDataSource<Reservation>).data.length ? (this.expandedElement = this.expandedElement === eventOccurrence ? null : eventOccurrence) : null;
    this.cd.detectChanges();
    this.innerTables.forEach((table, index) => (table.dataSource as MatTableDataSource<Reservation>).sort = this.innerSort.toArray()[index]);
  }

  getEventOccurrences(){
    return this.eventService.getEvent(1).subscribe(data =>
      {
        console.log(data)
        this.eventOccurrences = data.eventOccurrences;
        this.loadTableData();
        this.isLoaded = true;
      }
    )
  }
}
