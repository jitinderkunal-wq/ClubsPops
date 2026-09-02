import { MaintenanceLog } from "./maintenanceLog";
import { WaitingList } from "./waitingList";

export interface AccommodationStatusTypeMaintenanceLogWaitingList {
    accommodationId: number;
    accommodationTypeId: number;
    typeName: string;
    accommodationStatusId: number;
    statusName: string;
    number: string;
    floor: number;
    deActivate: boolean;
    requiresCreditCard: boolean;
    hasMaintenanceReservation: boolean;
    employeeLocker?: string;
    isGhost: boolean;
    hasGloryHole: boolean;
    lastMaintenanceLog?: MaintenanceLog | null;
    activeWaitingList?: WaitingList | null;
}