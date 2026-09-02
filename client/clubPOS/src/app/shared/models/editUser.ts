export interface EditUser {
    userId: string;
    firstName: string;
    lastName: string;
    payRate: number;
    hireDate: String;
    terminationDate: String | null;
    sin: number | null;
    isActive: boolean;
    isSalary: boolean;
    inTrainingMode: boolean;
    organizationId: number;
    role: string;
    email: string;
    street: string;
    city: string;
    province: string;
    postalCode: string;
    cellPhone: string;
}