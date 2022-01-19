export class TabularFeature {
    id: number
    reportId: number
    script: string
    title: string
    subTitle: string
    showFilterInfo: boolean
    template: string
    asOnDate: boolean
    exportable: boolean
    hasTotalColumn: boolean
    hasTotalRow: boolean
    createdOn: Date
    createdBy: string
    updatedOn: Date
    updatedBy: string
    constructor(){
        this.id = 0
        this.reportId = 0
        this.script = ''
        this.title = ''
        this.subTitle = ''
        this.showFilterInfo = false
        this.template = ''
        this.asOnDate = false
        this.exportable = true
        this.hasTotalColumn = false
        this.hasTotalRow = true
        this.createdOn = new Date()
        this.createdBy = 'admin'
        this.updatedOn = new Date()
        this.updatedBy = 'admin'
    }
  }
//   export const createDefaultTabularFeature = ({
//     id = 0,
//     reportId = 0,
//     script = '',
//     title = '',
//     subTitle = '',
//     showFilterInfo = false,
//     template = '',
//     asOnDate = false,
//     exportable = false,
//     hasTotalColumn = false,
//     hasTotalRow = true,
//     createdOn = new Date(),
//     createdBy = 'admin',
//     updatedOn = new Date(),
//     updatedBy = 'admin'
//   }: ITabularFeature = {}): ITabularFeature => ({
//     id,
//     reportId,
//     script,
//     title,
//     subTitle,
//     showFilterInfo,
//     template,
//     asOnDate,
//     exportable,
//     hasTotalColumn,
//     hasTotalRow,
//     createdOn,
//     createdBy,
//     updatedOn,
//     updatedBy
//   });