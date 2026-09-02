import { MaintenanceLogDetail } from "./maintenanceLogDetail";

export interface MaintenanceLog {
    maintenanceLogId: number;
    employeeName: string;
    logDate: string;
    backInServiceDate: string | null;
    comment: string | null;
    resolvedComment: string | null;
    needsCleaning: boolean;
    resolvedUserId: string | null;
    maintenanceLogDetails: MaintenanceLogDetail[];
}