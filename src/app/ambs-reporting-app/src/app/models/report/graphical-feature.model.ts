export class GraphicalFeature {
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
    constructor(){
        this.id=0,
        this.reportId=0,
        this.graphType= 0,
        this.script= '',
        this.title= '',
        this.subTitle= '',
        this.showFilterInfo= true,
        this.showLegend= true,
        this.xaxisTitle= '',
        this.yaxisTitle= '',
        this.createdOn= new Date(),
        this.createdBy= '',
        this.updatedOn= new Date(),
        this.updatedBy= '',
        this.xaxisSuffix= '',
        this.xaxisPrefix= '',
        this.yaxisSuffix= '',
        this.yaxisPrefix= ''
    }
}
// export const createDefaultGraphicalFeature=({
//     id=0,
//     reportId=0,
//     graphType= 0,
//     script= '',
//     title= '',
//     subTitle= '',
//     showFilterInfo= true,
//     showLegend= true,
//     xaxisTitle= '',
//     yaxisTitle= '',
//     createdOn= new Date(),
//     createdBy= '',
//     updatedOn= new Date(),
//     updatedBy= '',
//     xaxisSuffix= '',
//     xaxisPrefix= '',
//     yaxisSuffix= '',
//     yaxisPrefix= ''
// } : IGraphicalFeature={}):IGraphicalFeature=>({
//     id,
//     reportId,
//     graphType,
//     script,
//     title,
//     subTitle,
//     showFilterInfo,
//     showLegend,
//     xaxisTitle,
//     yaxisTitle,
//     createdOn,
//     createdBy,
//     updatedOn,
//     updatedBy,
//     xaxisSuffix,
//     xaxisPrefix,
//     yaxisSuffix,
//     yaxisPrefix
// })
