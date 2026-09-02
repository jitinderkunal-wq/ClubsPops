export interface ActiveSalesSession {
    userId: string,
    shiftSessionId: number,
    employeeName: string,
    sessionDateCreated: Date,
    sessionEndDate: Date,
    shiftNumber: number,
    beginFloat: number,
    drawerNumber: number,
    businessDay: Date,
    isCurrentlyInUse: boolean
}