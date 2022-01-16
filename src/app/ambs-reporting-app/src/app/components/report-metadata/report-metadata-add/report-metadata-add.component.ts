import { Component, OnInit } from '@angular/core';
import { MetaDataService } from 'src/app/services/meta-data.service';
import { DashboardComponent } from '../../dashboard/dashboard.component';

@Component({
  selector: 'app-report-metadata-add',
  templateUrl: './report-metadata-add.component.html',
  styleUrls: ['./report-metadata-add.component.css']
})
export class ReportMetadataAddComponent implements OnInit {

  public metadata: any;
  public message: string = '';

  constructor(private metadataService: MetaDataService, private dashboardService: DashboardComponent) { }

  ngOnInit(): void {
    this.metadata = {
      id: 0,
      dashboardId: 0,
      dataSource: ''
    }
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
