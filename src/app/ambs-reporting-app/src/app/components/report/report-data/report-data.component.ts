import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-report-data',
  templateUrl: './report-data.component.html',
  styleUrls: ['./report-data.component.css']
})
export class ReportDataComponent implements OnInit {
@Input() tableDatas=null;
  constructor() { }

  ngOnInit(): void {
  }
  tabClicked(data:any){
    // data.isActive=true;
    // this.tableDatas?.filter(dt=>dt.sheetName!==data.sheetName).forEach(td => {
    //   td.isActive=false;
    // });
  }
}
