import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-confirmation-page',
  templateUrl: './confirmation-page.component.html',
  styleUrls: ['../../../app.component.css',  '../../../responsive.css', './confirmation-page.component.css']
})
export class ConfirmationPageComponent implements OnInit {

  eventName = '';

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>
      {
        this.eventName = params.get("eventName") as unknown as string
      });
    
  }

}
