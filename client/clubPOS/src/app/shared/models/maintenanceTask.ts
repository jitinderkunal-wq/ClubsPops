export interface MaintenanceTask {
    maintenanceTaskId: number;
    taskName: string;
    isEndUserFixable: boolean;
    isActive: boolean;
}