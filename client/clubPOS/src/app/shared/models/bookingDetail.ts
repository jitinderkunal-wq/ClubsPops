export interface BookingDetail {
    bookingId: number;
    accommodationId: number;
    accommodationItemId: number | null;
    accommodationRateId: number;
    bookingDetailStatusId: number;
    couponId: number | null;
    promopassId: number | null;
    membershipId: number | null;
    hoursOfStay: number;
    qty: number;
    price: number;
    hasDeposit: boolean;
    returned: number;
    transfered: number;
    accommodationCouponDiscount: number;
    accommodationPromopassDiscount: number;
    transferDepositDetail: number;
    transferOut: number;
    transferDepositsReturned: number;
    accommodationOtherDiscount: number;
    accommodationOtherDisPercent: number;
    checkOutTimeRequested: string | null;
    chargableDeposit: boolean;
    depositTransRefund: number;
    isKeyRefundedOnly: boolean;
}