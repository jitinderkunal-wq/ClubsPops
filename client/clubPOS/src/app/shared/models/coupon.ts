export interface Coupon {
    couponId: number;
    accommodationTypeId: number | null;
    dateCreated: string;
    couponName: string;
    description: string;
    couponNumber: number;
    flatRate: number;
    discountDollars: number;
    discountPercent: number;
    startDate: string | null;
    expiryDate: string | null;
    isExclusive: boolean;
    isActive: boolean;
    isCoupon: boolean;
    returnOnCheckOut: boolean;
}

export class CouponFormModel implements Coupon {
    constructor(
        public couponId: number,
        public accommodationTypeId: number | null,
        public dateCreated: string,
        public couponName: string,
        public description: string,
        public couponNumber: number,
        public flatRate: number,
        public discountDollars: number,
        public discountPercent: number,
        public startDate: string | null,
        public expiryDate: string | null,
        public isExclusive: boolean,
        public isActive: boolean,
        public isCoupon: boolean,
        public returnOnCheckOut: boolean
    ) { }
}