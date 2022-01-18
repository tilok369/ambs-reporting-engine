import { ReportType } from "src/app/enums/report-enum";
import { IGraphicalFeature } from "./graphical-feature.model";
import { ReportFilter } from "./report-filter.model";
import { ITabularFeature } from "./tabular-feature.model";

export interface IReport {
    id: number
    widgetId: number
    name: string
    status: boolean
    type: ReportType
    createdOn: Date
    createdBy: string
    updatedOn: Date
    updatedBy: string
    reportFilterList: Array<ReportFilter>
    tabularFeature?: ITabularFeature
    graphicalFeature?: IGraphicalFeature
}
const createDefaultReport: IReport = {
    id: 0,
    widgetId: 0,
    name: '',
    status: true,
    type: ReportType.Tabular,
    createdOn: new Date(),
    createdBy: 'admin',
    updatedOn: new Date(),
    updatedBy: 'admin',
    reportFilterList: []
}
