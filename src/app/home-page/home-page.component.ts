import { Component } from '@angular/core';
import { DatafetchService } from '../datafetch.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {


  public users:any=[];
  constructor(private dataService:DatafetchService){}

  ngOnInit() {
    this.dataService.getData().subscribe(res=>{
      this.users=res;
    })
     
 }




}
