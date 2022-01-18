export interface IGraphicalFeature {
    id:number
    reportId: number
    graphType: number
    script: string
    title: string
    subTitle: string
    showFilterInfo: boolean
    showLegend: boolean
    xaxisTitle: string
    yaxisTitle: string
    createdOn: Date
    createdBy: string
    updatedOn: Date
    updatedBy: string
    xaxisSuffix: string
    xaxisPrefix: string
    yaxisSuffix: string
    yaxisPrefix: string
}
const createDefaultGraphicalFeature:IGraphicalFeature={
    id:0,
    reportId:0,
    graphType: 0,
    script: '',
    title: '',
    subTitle: '',
    showFilterInfo: true,
    showLegend: true,
    xaxisTitle: '',
    yaxisTitle: '',
    createdOn: new Date(),
    createdBy: '',
    updatedOn: new Date(),
    updatedBy: '',
    xaxisSuffix: '',
    xaxisPrefix: '',
    yaxisSuffix: '',
    yaxisPrefix: ''
}
