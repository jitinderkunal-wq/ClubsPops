export interface Organization{
    organizationId: number | 0; 
    organizationName: string | null; 
    street: string | null; 
    city: string | null; 
    province: number; 
    postalCode: number; 
    country: string | null; 
    registrationNumber: string | null; 
    logo: string | null; 
    isActive: boolean;
}