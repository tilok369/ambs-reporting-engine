import { ReportType } from "src/app/enums/report-enum";
import { GraphicalFeature } from "./graphical-feature.model";
import { IReportFilter } from "./report-filter.model";
import { TabularFeature } from "./tabular-feature.model";

export class Report {
    id: number
    widgetId: number
    name: string
    status: boolean
    type: ReportType
    createdOn: Date
    createdBy: string
    updatedOn: Date
    updatedBy: string
    cacheAliveTime: number
    isCacheEnable : boolean
    reportFilterList: Array<IReportFilter>
    tabularFeature: TabularFeature
    graphicalFeature: GraphicalFeature
    widgetName:string
    constructor(){
        this.id=0
        this.widgetId=0
        this.name= ''
        this.status= true
        this.type= ReportType.Tabular
        this.createdOn= new Date()
        this.createdBy= 'admin'
        this.updatedOn= new Date()
        this.updatedBy= 'admin'
        this.cacheAliveTime = 0
        this.isCacheEnable = false
        this.reportFilterList= []
        this.tabularFeature=new TabularFeature()
        this.graphicalFeature=new GraphicalFeature()
        this.widgetName=''
    }
}
// export const createDefaultReport = ({
//     id= 0,
//     widgetId= 0,
    // name= '',
    // status= true,
    // type= ReportType.Tabular,
    // createdOn= new Date(),
    // createdBy= 'admin',
    // updatedOn= new Date(),
    // updatedBy= 'admin',
    // reportFilterList= [],
    // tabularFeature=createDefaultTabularFeature(),
    // graphicalFeature=createDefaultGraphicalFeature()
//   }: IReport = {}): IReport => ({
//     id,
//     widgetId,
//     name,
//     status,
//     type,
//     createdOn,
//     createdBy,
//     updatedOn,
//     updatedBy,
//     reportFilterList,
//     tabularFeature,
//     graphicalFeature
//   });