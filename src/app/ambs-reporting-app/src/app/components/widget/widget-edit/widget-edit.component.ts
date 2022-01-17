import { Component, OnInit } from '@angular/core';
import { WidgetService } from 'src/app/services/widget.service';

@Component({
  selector: 'app-widget-edit',
  templateUrl: './widget-edit.component.html',
  styleUrls: ['./widget-edit.component.css']
})
export class WidgetEditComponent implements OnInit {

  public widget: any;
  public message: string = '';
  constructor(private widgetService: WidgetService) { }

  ngOnInit(): void {
    var id = window.history.state.widgetId;
    this.getWidget(id);
  }

  validateMetadata(){
    if(!this.widget.name){
      this.message = 'Name is required';
      return false;
    }
    return true;
  }

  getWidget(id){
    this.widgetService.getWidget(id).subscribe((res: any) => {
      this.widget = res;
    });
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
