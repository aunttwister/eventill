import { ChangeDetectionStrategy, ChangeDetectorRef, Component, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-create-event-occurrence',
  imports: [],
  standalone: true,
  templateUrl: './create-event-occurrence.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['../../../app.component.css', './create-event-occurrence.component.css']
})
export class CreateEventOccurrenceComponent {
  
  ngOnChanges(changes: SimpleChanges): void {
  }

  constructor(
    private cd: ChangeDetectorRef
  )
  {
    
  }
}
