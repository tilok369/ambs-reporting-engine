import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-report-configuration-add',
  templateUrl: './report-configuration-add.component.html',
  styleUrls: ['./report-configuration-add.component.css']
})
export class ReportConfigurationAddComponent implements OnInit {

  public dashboard: any;
  public message: string = '';
  fileData: any;

  constructor(private dashboardService: DashboardService) { }
  
  ngOnInit(): void {
    this.dashboard = {
      id: 0,
      name: '',
      iframeUrl: window.origin + '/dashboard?uid='+ new Date().getTime(),
      status: true,
      createdOn: new Date(),
      createdBy: "admin",
      updatedOn: new Date(),
      updatedBy: "admin",
      brandImage: '',
    }
    
  }

  validateDashboard(){
    if(!this.dashboard.name){
      this.message = 'Name is required';
      return false;
    }
    return true;
  }

  fileProgress(fileInput: any) {
    this.fileData = <File>fileInput.target.files[0];
    
  }

  saveDashboad(){
    this.message = '';
    let fileToUpload = this.fileData;
    const formData = new FormData();
    formData.append('file',fileToUpload,fileToUpload.name);
    this.dashboard.brandImage = fileToUpload.name;
    console.log(this.dashboard);
    this.dashboardService.uploadDashboardPhoto(formData).subscribe((photores:any)=>{
      if(formData){
        if(this.validateDashboard()){
          this.dashboardService.saveDashboard(this.dashboard).subscribe((res: any) => {
            console.log(res);
            this.message = res.message;
          });
        }

      }

    })
    
  }

}
