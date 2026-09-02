export interface MaintenanceLogDetail {
    maintenanceLogDetailId: number;
    maintenanceTaskId: number;
    taskName: string;
    isResolved: boolean;
    dateResolved: string | null;
}