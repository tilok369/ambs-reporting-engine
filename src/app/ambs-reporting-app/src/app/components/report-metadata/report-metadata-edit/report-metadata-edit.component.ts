import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';
import { MetaDataService } from 'src/app/services/meta-data.service';

@Component({
  selector: 'app-report-metadata-edit',
  templateUrl: './report-metadata-edit.component.html',
  styleUrls: ['./report-metadata-edit.component.css']
})
export class ReportMetadataEditComponent implements OnInit {

  public metadata: any;
  public dashboards: any[] = [];
  public message: string = '';

  constructor(private metadataService: MetaDataService, private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.metadata = {
      id: 0,
      dashboardId: 0,
      dataSource: ''
    };
    var id = window.history.state.metadataId;
    this.getDasgboards(1, 100);
    this.getMetadata(id);
  }

  getDasgboards(page, size){
    this.dashboardService.getDashboards(page, size).subscribe((res: any) => {
      console.log(res);
      this.dashboards = res;
    });
  }

  getMetadata(id){
    this,this.metadataService.getMetadata(id).subscribe((res: any) => {
      this.metadata = res;
    });
  }

  validateMetadata(){
    if(!this.metadata.dataSource){
      this.message = 'Data source is required';
      return false;
    }
    return true;
  }

  saveMetadata(){
    this.message = '';
    console.log(this.metadata);
    if(this.validateMetadata()){
      this.metadataService.saveMetadata(this.metadata).subscribe((res: any) => {
        console.log(res);
        this.message = res.message;
      });
    }
  }

}
