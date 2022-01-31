import { IReportFilter } from "./report-filter.model";
import { FilterType } from "src/app/enums/filter-enum";

export class Filter {
    id: number
    name: string
    label:string
    script: string
    status: boolean
    type: FilterType
    parameter:''
    DependentParameters:''
    createdOn: Date
    createdBy: string
    updatedOn: Date
    updatedBy: string
    reportFilterList: Array<IReportFilter>
    widgetName:string
    constructor(){
        this.id=0
        this.name= ''
        this.label = ''
        this.script=''
        this.status= true
        this.type = FilterType.TextBox
        this.parameter=''
        this.DependentParameters=''
        this.createdOn= new Date()
        this.createdBy= 'admin'
        this.updatedOn= new Date()
        this.updatedBy= 'admin'
        this.reportFilterList= []
        this.widgetName=''
    }
}
