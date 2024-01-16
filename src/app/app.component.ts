import { Component ,OnInit} from '@angular/core';
import { DatafetchService } from './datafetch.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public dataService: DatafetchService) {}

  LogOut() {
    this.dataService.signOut();
  }
}
