import { Component, OnInit } from '@angular/core';
import { WidgetService } from 'src/app/services/widget.service';

@Component({
  selector: 'app-widget-add',
  templateUrl: './widget-add.component.html',
  styleUrls: ['./widget-add.component.css']
})
export class WidgetAddComponent implements OnInit {

  public widget: any;
  public message: string = '';
  constructor(private widgetService: WidgetService) { }

  ngOnInit(): void {
    var id = window.history.state.dashboardId;
    this.widget = {
      id: 0,
      type: 1,
      name: '',
      dashboardId: id,
      status: true,
      createdOn: new Date(),
      createdBy: "admin",
      updatedOn: new Date(),
      updatedBy: "admin"
    }
  }

  validateMetadata(){
    if(!this.widget.name){
      this.message = 'Name is required';
      return false;
    }
    return true;
  }

  saveWidget(){
    this.message = '';
    console.log(this.widget);
    if(this.validateMetadata()){
      this.widgetService.saveWidget(this.widget).subscribe((res: any) => {
        console.log(res);
        this.message = res.message;
      });
    }
  }

}
