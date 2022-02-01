import { FilterType } from "src/app/enums/filter-enum"
import { ExportType, ReportType } from "src/app/enums/report-enum"
import { IDropdownFilter } from "../report/dropdown-filter.model"

export class DashboardWidgetReportVM {
    id: number
    name: string
    iframeUrl: string
    status: boolean
    widgets: Array<WidgetVM>
    constructor() {
        this.id = 0
        this.name = ''
        this.iframeUrl = ''
        this.status = true
        this.widgets = []
    }
}
export class WidgetVM {
    id: number
    dashboardId: number
    name: string
    status: boolean
    reports: Array<ReportVM>
    constructor() {
        this.id = 0
        this.dashboardId = 0
        this.name = ''
        this.status = true
        this.reports = []
    }
}
export class ReportVM {
    id: number
    name: string
    status: boolean
    widgetId: number
    type: ReportType
    data?: any
    exportType:ExportType
    filters: Array<FilterVM>
    constructor() {
        this.id = 0
        this.widgetId = 0
        this.name = ''
        this.status = true
        this.type = ReportType.Tabular
        this.filters = []
        this.exportType=ExportType.Excel
    }
}
export class FilterVM {
    id: number
    reportId: number
    name: string
    label: string
    parameter: string
    dependentParameters: string
    status: boolean
    type: FilterType
    value?: any
    filterValue?:IDropdownFilter
    dropdownFilters?: Array<IDropdownFilter>
    constructor() {
        this.id = 0
        this.reportId = 0
        this.name = ''
        this.label = ''
        this.parameter = ''
        this.dependentParameters = ''
        this.status = true
        this.type = FilterType.TextBox
    }
}
