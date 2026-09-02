
export interface CashReconcilation {

    accommodationSalesPolled : number,
    refundAccommodationPolled : number,
    voidedAccommodationsPolled : number,
    rateDiscountsPolled : number,
    couponDiscountsPolled : number,
    promoDiscouuntsPolled : number,
    otherDiscountsRolled : number,
    memberDiscountsPolled : number,
    totalAccommodationSalesPolled : number,

    depositsCollectedPolled : number,
    depositsReturnedPolled : number,
    membershipSalesPolled : number,
    otherSalesPolled : number,
    barSalesPolled : number,
    totalFinalSalesPolled : number,

    debitPolled : number,
    masterCardPolled : number,
    visaPolled : number,
    amexPolled : number,
    discoverPolled : number,
    otherCreditCardPolled : number,
    totalDebitCreditPolled : number,

    eosDebitCreditTotalPolled : number,

    totalUsdollarsPolled : number,
    totalEuroDollarsPolled : number,

    finalCashDepositPolled : number,
    paidInsPolled : number,
    receiptsPaidOutsPolled : number,
    cashDropsTotalPolled : number,
    cashOverShortPolled: number,

    lockerQuantity : number,
    singleRoomQuantity : number,
    singleRoomTVQuantity : number,
    doubleRoomTVQuantity : number,
    lockerAccomodationTotal : number,
    singleRoomAccomodationTotal : number,
    singleRoomTVAccomodationTotal : number,
    doubleRoomTVAccomodationTotal : number,

    lockerTransferQuantity : number,
    singleRoomTransferQuantity : number,
    singleRoomTVTransferQuantity : number,
    doubleRoomTVTransferQuantity : number,
    lockerAccomodationTransferTotal : number,
    singleRoomAccomodationTransferTotal : number,
    singleRoomTVAccomodationTransferTotal : number,
    doubleRoomTVAccomodationTransferTotal : number,

    refundedDeposits : number,
    forfittedDeposist : number,
    totalCollected : number

}