import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';
import { MetaDataService } from 'src/app/services/meta-data.service';
import { environment } from 'src/environments/environment';

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

  fileData: any;
  previewUrl: any = null;

  ngOnInit(): void {
    this.metadata = {
      id: 0,
      dashboardId: 0,
      dataSource: '',
      brandImage: '',
      prevBrandImage:'',
    };
    var id = window.history.state.metadataId;
    this.getDasgboards(1, 100);
    this.getMetadata(id);
  }

  fileProgress(fileInput: any) {
    this.fileData = <File>fileInput.target.files[0];
    //this.fileData = this.previewUrl;
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

  getMetadata(id){
    this,this.metadataService.getMetadata(id).subscribe((res: any) => {
      this.metadata = res;
      this.metadata.prevBrandImage = res.brandImage;
      this.previewUrl = res.imageData;
    });
  }

  validateMetadata(){
    if(!this.metadata.dataSource){
      this.message = 'Data source is required';
      return false;
    }
    return true;
  }

  constructImage(photo) {
    if (!photo)
      photo = 'Dashboard-6logo.jpg';
    return environment.apiEndPointRoot + 'Resources/Dashboard/' + photo;
  }

  saveMetadata(){
    this.message = '';
    let fileToUpload = this.fileData;
    if(!this.fileData)
    {
      if(this.validateMetadata()){
        this.metadataService.saveMetadata(this.metadata).subscribe((res: any) => {
          console.log(res);
          this.message = res.message;
        });
      }
      return
    }
         
    const formData = new FormData();
    formData.append('file',fileToUpload,fileToUpload.name);
    this.metadata.brandImage = fileToUpload.name;
    console.log(this.metadata);

    this.metadataService.uploadMetaDatadPhoto(formData,this.metadata.dashboardId,this.metadata.prevBrandImage).subscribe((photores:any)=>{
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
