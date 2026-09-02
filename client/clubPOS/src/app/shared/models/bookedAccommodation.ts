import { Booking } from "./booking";

export interface BookedAccommodation {
    bookedAccommodationId: number;
    bookedAccommodation: {
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
        employeeLocker: null | any; // Replace with the actual type if needed
        isGhost: boolean;
        hasGloryHole: boolean;
        lastMaintenanceLog: null | any; // Replace with the actual type if needed
        activeWaitingList: null | any; // Replace with the actual type if needed
    };
    booking: Booking;
}