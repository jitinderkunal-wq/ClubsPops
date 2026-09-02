export interface User {
    userId: string;
    email: string;
    firstName: string;
    organizationId: number;
    token: string;
    expiration: string;
    roles: string[];
}