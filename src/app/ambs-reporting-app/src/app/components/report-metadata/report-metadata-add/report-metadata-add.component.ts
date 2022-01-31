import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';
import { MetaDataService } from 'src/app/services/meta-data.service';

@Component({
  selector: 'app-report-metadata-add',
  templateUrl: './report-metadata-add.component.html',
  styleUrls: ['./report-metadata-add.component.css']
})
export class ReportMetadataAddComponent implements OnInit {

  public metadata: any;
  public dashboards: any[] = [];
  public message: string = '';
  fileData: any;
  previewUrl: any = null;

  constructor(private metadataService: MetaDataService, private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.metadata = {
      id: 0,
      dashboardId: 0,
      dataSource: '',
      brandImage: '',
    };

    this.getDasgboards(1, 100);
  }

  fileProgress(fileInput: any) {
    this.fileData = <File>fileInput.target.files[0];
    this.preview();
  }

  preview() {
    var mimeType = this.fileData.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    var reader = new FileReader();
    reader.readAsDataURL(this.fileData);
    reader.onload = (_event) => {
      this.previewUrl = reader.result;
    }
  }

  getDasgboards(page, size){
    this.dashboardService.getDashboards(page, size).subscribe((res: any) => {
      console.log(res);
      this.dashboards = res;
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
    let fileToUpload = this.fileData;
    const formData = new FormData();
    formData.append('file',fileToUpload,fileToUpload.name);
    this.metadata.brandImage = fileToUpload.name;
    console.log(this.metadata);

    this.metadataService.uploadMetaDatadPhoto(formData,this.metadata.dashboardId).subscribe((photores:any)=>{
      if(formData){
        if(this.validateMetadata()){
          this.metadataService.saveMetadata(this.metadata).subscribe((res: any) => {
            console.log(res);
            this.message = res.message;
          });
        }

      }

    })

    
  }

}
