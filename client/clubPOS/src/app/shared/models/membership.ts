export interface Membership {
    membershipId: number | null;
    creditCardTypeId: number | null;
    membershipTypeId: number;
    membershipNum: number | null;
    firstName: string;
    lastName: string | null;
    address: string | null;
    phone: string | null;
    city: string | null;
    province: string | null;
    country: string | null;
    postalZipCode: string | null;
    membershipStartDate: string;
    membershipEndDate: string | null;
    creditCardNumber: string | null;
    creditCardExpiry: string | null;
    csv: string | null;
    isActive: boolean;
}