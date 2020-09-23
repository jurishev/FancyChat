import { Component, Input } from '@angular/core';
import { Envelope } from '../envelope';

@Component({
  selector: 'app-inbox-view',
  templateUrl: './inbox-view.component.html'
})
export class InboxViewComponent {

  @Input() inbox: Envelope[];
}
