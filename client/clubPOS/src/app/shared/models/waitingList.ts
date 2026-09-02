export interface WaitingList {
    accommodationTypeId: number;
    membershipId: number | null;
    membershipTypeId: number;
    roomAssignedId: number | null;
    transactionId: number;
    employeeName: string;
    phone: string | null;
    comment: string;
    waitListTimeAdded: string;
    isActive: boolean;
}