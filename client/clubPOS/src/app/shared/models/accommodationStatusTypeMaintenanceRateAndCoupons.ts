import { AccommodationRate } from "./accommodationRate";
import { Coupon } from "./coupon";
import { MaintenanceLog } from "./maintenanceLog";

export interface AccommodationStatusTypeMaintenanceRateAndCoupons {
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
    accommodationRate?: AccommodationRate | null;
    lastMaintenanceLog?: MaintenanceLog | null;
    coupons: Coupon[];
}