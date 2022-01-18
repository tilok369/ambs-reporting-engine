export interface ITabularFeature {
    id: number
    reportId: number
    script: string
    title?: string
    subTitle?: string
    showFilterInfo: boolean
    template?: string
    asOnDate: boolean
    exportable: boolean
    hasTotalColumn: boolean
    hasTotalRow: boolean
    createdOn: Date
    createdBy: string
    updatedOn: Date
    updatedBy: string
}
const createDefaultTabularFeature: ITabularFeature = {
    id: 0,
    reportId: 0,
    script: '',
    title: '',
    subTitle: '',
    showFilterInfo: true,
    template: '',
    asOnDate: false,
    exportable: true,
    hasTotalColumn: false,
    hasTotalRow: true,
    createdOn: new Date(),
    createdBy: 'admin',
    updatedOn: new Date(),
    updatedBy: 'admin',
}
