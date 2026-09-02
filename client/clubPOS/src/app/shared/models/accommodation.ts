export interface Accommodation {
    accommodationId: number;
    accommodationTypeId: number;
    accommodationStatusId: number;
    number: string;
    floor: number;
    deActivate: boolean;
    requiresCreditCard: boolean;
    hasMaintenanceReservation: boolean;
    employeeLocker: string | null;
    isGhost: boolean;
    hasGloryHole: boolean;
}