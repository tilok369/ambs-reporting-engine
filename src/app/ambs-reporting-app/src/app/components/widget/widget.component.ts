import { Component, OnInit } from '@angular/core';
import { WidgetService } from 'src/app/services/widget.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-widget',
  templateUrl: './widget.component.html',
  styleUrls: ['./widget.component.css']
})
export class WidgetComponent implements OnInit {

  public widgets: any[] = [];
  public dashboardId: Number = 0;
  constructor(private router: Router, private widgetService: WidgetService) { }

  ngOnInit(): void {
    this.dashboardId = window.history.state.dashboardId;
    this.getWidgets(this.dashboardId, 1, 100);
  }

  getWidgets(dashboardId, page, size){
    this.widgetService.getWidgets(dashboardId, page, size).subscribe((res: any) => {
      console.log(res);
      this.widgets = res;
    });
  }

  addWidget(){
    this.router.navigateByUrl('/widget-add', {state: {dashboardId: this.dashboardId}});
  }

  editWidget(id){
    this.router.navigateByUrl('/widget-edit', {state: {widgetId: id, dashboardId: this.dashboardId}});
  }
  goToReport(widget){
    this.router.navigateByUrl('/report',{state:{widgetId:widget.id,widgetName:widget.name}});
  }

}
