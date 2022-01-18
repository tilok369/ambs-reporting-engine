import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { FilterService } from 'src/app/services/filter.service';

@Component({
  selector: 'app-report-filter-add',
  templateUrl: './report-filter-add.component.html',
  styleUrls: ['./report-filter-add.component.css']
})
export class ReportFilterAddComponent implements OnInit {

  public filter: any;
  public message: string = '';

  constructor(private filterService: FilterService) { }

  ngOnInit(): void {
    this.filter = {
      id: 0,
      name: '',
      label:'',
      script:'',
      parameter:'',
      dependentParameters:'',
      status: true,
      createdOn: new Date(),
      createdBy: "admin",
      updatedOn: new Date(),
      updatedBy: "admin"
    }
  }


  validateFilter(){
    if(!this.filter.name){
      this.message = 'Name is required';
      return false;
    }
    return true;
  }

  saveFilter(){
    this.message = '';
    console.log(this.filter);
    if(this.validateFilter()){
      this.filterService.saveFilter(this.filter).subscribe((res: any) => {
        console.log(res);
        this.message = res.message;
      });
    }
  }

}
