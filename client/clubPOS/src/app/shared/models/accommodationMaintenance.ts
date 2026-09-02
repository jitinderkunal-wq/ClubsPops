export interface AccommodationMaintenance {
    accommodationId: number;
    newAccommodationStatusId: number;
    maintenanceTaskIds?: number[];
    comment?: string;
    resolvedComment?: string;
    needsCleaning: boolean;
}