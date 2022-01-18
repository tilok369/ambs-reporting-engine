import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FilterService } from 'src/app/services/filter.service';


@Component({
  selector: 'app-report-filter',
  templateUrl: './report-filter.component.html',
  styleUrls: ['./report-filter.component.css']
})
export class ReportFilterComponent implements OnInit {

  public filters: any[] = [];

  constructor(private router: Router, private filterService: FilterService) { }

  ngOnInit(): void {
    this.getFilters(1, 10);
  }
  

  getFilters(page, size)
  {
    this.filterService.getFilters(page, size).subscribe((res: any) => {
      console.log(res);
      this.filters = res;
    });

  }

  addFilter(){
    this.router.navigateByUrl('/report-filter-add');
  }

  editFilter(id){
    this.router.navigateByUrl('/report-filter-edit', {state: {filterId: id}});
  }

}
