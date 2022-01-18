import { ReportType } from "src/app/enums/report-enum";

export interface IReportList {
    id:number
    name:string
    status:boolean
    type:ReportType
    widgetName:string
    dashboardName:string
}
