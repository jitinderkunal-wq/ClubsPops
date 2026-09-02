using System.Diagnostics;
using System.Security.Policy;
using System.Text;
using Azure;
using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Common;
using ClubPOS.Infrastructure.IRepositories;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace ClubPOS.Services.Services;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    public BookingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<WaitingListDTO>> GetActiveWaitingListsAsync(int organizationId)
    {
        var activeWaitingLists = await _unitOfWork.Repository<WaitingList>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsActive == true);
        var activeWaitingListsToReturn = activeWaitingLists.Select(activeWaitingList => new WaitingListDTO
        {
            AccommodationId = activeWaitingList.AccommodationId,
            AccommodationTypeId = activeWaitingList.AccommodationTypeId,
            MembershipId = activeWaitingList.MembershipId,
            MembershipTypeId = activeWaitingList.MembershipTypeId,
            RoomAssignedId = activeWaitingList.RoomAssignedId,
            TransactionId = activeWaitingList.TransactionId,
            UserId = activeWaitingList.UserId,
            Phone = activeWaitingList.Phone,
            Comment = activeWaitingList.Comment,
            WaitListTimeAdded = activeWaitingList.WaitListTimeAdded,
            IsActive = activeWaitingList.IsActive
        }).ToList();
        return activeWaitingListsToReturn;
    }

    public async Task<IReadOnlyList<MembershipListDTO>> GetMembershipListsAsync(int organizationId)
    {
        var activemembershipLists = await _unitOfWork.Repository<Membership>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsActive == true && x.LastName == "Platinum" || x.LastName == "Manage Ovverrrde");
        var activemembershipListsToReturn = activemembershipLists.Select(activemembershipList => new MembershipListDTO
        {
            //MembershipId = activemembershipList.Id,
            MembershipTypeId = activemembershipList.MembershipTypeId,
            MembershipName = activemembershipList.LastName!
        }).ToList();
        return activemembershipListsToReturn;
    }

    public async Task<ResponseStatus> AddToWaitingAsync(WaitingListCreateDTO waitingListCreateDTO)
    {
        // Checking If (Locker or room must not be on the Waiting List already)
        var waitingLists = await _unitOfWork.Repository<WaitingList>().ListAllWhereAsync(x => x.OrganizationId == waitingListCreateDTO.OrganizationId && x.AccommodationId == waitingListCreateDTO.AccommodationId && x.IsDeleted == false && x.IsActive == true);
        var accomodationExist = waitingLists.FirstOrDefault();
        if (accomodationExist != null)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Error,
                Message = "Accommodation already in waiting list",
            };
        }
        var membershipLists = await _unitOfWork.Repository<Membership>().ListAllWhereAsync(x => x.OrganizationId == waitingListCreateDTO.OrganizationId && x.MembershipTypeId == waitingListCreateDTO.MembershipTypeId);
        var membership = membershipLists.FirstOrDefault();
        if (membership == null)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Error,
                Message = "No membership found",
            };
        }
        var bookingDetailLists = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(x => x.OrganizationId == waitingListCreateDTO.OrganizationId && x.AccommodationId == waitingListCreateDTO.AccommodationId && x.BookingDetailStatusId == 5);
        var bookingDetail = bookingDetailLists.FirstOrDefault();
        if (bookingDetail == null)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Error,
                Message = "No booking detail found",
            };
        }
        // 4 hours earlier time 
        var time = DateTime.Now.AddHours(4);
        // Checking if ( Total hours stayed must be 117 Hrs or less ) and ( Must have 4 hours or more left before Check-Out time )
        if (bookingDetail.HoursOfStay <= 117 && bookingDetail.CheckOutTimeRequested >= time)
        {
            var bookingLists = await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == waitingListCreateDTO.OrganizationId && x.Id == bookingDetail.BookingId);
            var booking = bookingLists.FirstOrDefault();
            if (booking == null)
            {
                return new ResponseStatus
                {
                    Status = ResponseEnum.Error,
                    Message = "No booking found",
                };
            }
            var transactionLists = await _unitOfWork.Repository<Transaction>().ListAllWhereAsync(x => x.OrganizationId == waitingListCreateDTO.OrganizationId && x.Id == booking.TransactionId);
            var transaction = transactionLists.FirstOrDefault();
            if (transaction == null)
            {
                return new ResponseStatus
                {
                    Status = ResponseEnum.Error,
                    Message = "No transection found",
                };
            }
            // Binding Details in WaitingList Model
            var waitingListdata = new WaitingList
            {
                AccommodationId = waitingListCreateDTO.AccommodationId,
                AccommodationTypeId = waitingListCreateDTO.AccommodationTypeId,
                MembershipId = membership.Id,
                MembershipTypeId = waitingListCreateDTO.MembershipTypeId,
                RoomAssignedId = waitingListCreateDTO.AccommodationId,
                TransactionId = transaction.Id,
                UserId = waitingListCreateDTO.UserId,
                Phone = waitingListCreateDTO.Phone,
                Comment = waitingListCreateDTO.Comment,
                WaitListTimeAdded = DateTime.Now,
                IsActive = true,
                OrganizationId = waitingListCreateDTO.OrganizationId,
                CreatedByUserId = waitingListCreateDTO.UserId,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            // Saving the data to the Waiting List Table
            _unitOfWork.Repository<WaitingList>().Add(waitingListdata);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                return new ResponseStatus
                {
                    Status = ResponseEnum.Error,
                    Message = "Unable to add in waiting list",
                };
            }
            return new ResponseStatus
            {
                Status = ResponseEnum.Success,
                Message = "Waiting list added successfully",
            };
        }
        else
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Failed,
                Message = "Must have atleast 4 hours before check out.",
            };
        }
    }

    public async Task<bool> DeleteWaitingAsync(int organizationId, int accommodationId)
    {
        var waitingLists = await _unitOfWork.Repository<WaitingList>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.AccommodationId == accommodationId && x.IsActive == true && x.IsDeleted == false);
        var waitingAccommodationExist = waitingLists.FirstOrDefault();
        if (waitingAccommodationExist == null) return false;
        // Updating Waiting List Table and set IsActive = false & IsDeleted = false
        waitingAccommodationExist.IsActive = false;
        waitingAccommodationExist.IsDeleted = true;
        waitingAccommodationExist.DeletedByUserId = waitingAccommodationExist.UserId;
        waitingAccommodationExist.DeletedDate = DateTime.Now;
        _unitOfWork.Repository<WaitingList>().Update(waitingAccommodationExist);
        if (await _unitOfWork.Complete() <= 0) return false!;
        return true;
    }

    public async Task<BookingDTO?> CheckInAccommodationAsync(BookingCreateDTO bookingCreateDTO)
    {
        var accommodation = await _unitOfWork.Repository<Accommodation>().GetByIdAsync(bookingCreateDTO.AccommodationId);
        if (accommodation == null) return null;
        var accommodationTypes = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.OrganizationId == bookingCreateDTO.OrganizationId && x.Id == accommodation.AccommodationTypeId);
        var desiredAccommodationType = accommodationTypes.FirstOrDefault();
        if (desiredAccommodationType == null) return null;
        var accommodationRates = await _unitOfWork.Repository<AccommodationRate>().ListAllWhereAsync(x => x.OrganizationId == bookingCreateDTO.OrganizationId);
        var salesTypes = await _unitOfWork.Repository<SalesType>().ListAllWhereAsync(x => x.OrganizationId == bookingCreateDTO.OrganizationId && x.SalesTypeName == "Booking");
        var desiredSalesType = salesTypes.FirstOrDefault();
        if (desiredSalesType == null) return null;
        var bookingDetailStatuses = await _unitOfWork.Repository<BookingDetailStatus>().ListAllWhereAsync(x => x.OrganizationId == bookingCreateDTO.OrganizationId && x.BookingDetailStatusName == "Booked");
        var desiredBookingDetailStatus = bookingDetailStatuses.FirstOrDefault();
        if (desiredBookingDetailStatus == null) return null;
        var receiptMessagesAndCoupons = await _unitOfWork.Repository<ReceiptMessagesAndCoupon>().ListAllWhereAsync(x => x.OrganizationId == bookingCreateDTO.OrganizationId);
        var desiredreceiptMessageAndCoupon = receiptMessagesAndCoupons.FirstOrDefault();
        if (desiredreceiptMessageAndCoupon == null) return null;
        var exchangeRates = await _unitOfWork.Repository<ExchangeRate>().ListAllWhereAsync(x => x.OrganizationId == bookingCreateDTO.OrganizationId);
        var usExchangeRate = exchangeRates.Where(x => x.RateName!.Equals("US Exchange Rate")).First().Rate;
        var euroExchangeRate = exchangeRates.Where(x => x.RateName!.Equals("Euro Exchange Rate")).First().Rate;
        var accommodationItems = await _unitOfWork.Repository<AccommodationItem>().ListAllWhereAsync(x => x.OrganizationId == bookingCreateDTO.OrganizationId);
        var firstAccommodationItem = accommodationItems.FirstOrDefault(ai => ai.Id == 1);
        if (firstAccommodationItem == null) return null;
        var secondAccommodationItem = accommodationItems.FirstOrDefault(ai => ai.Id == 2);
        if (secondAccommodationItem == null) return null;
        var thirdAccommodationItem = accommodationItems.FirstOrDefault(ai => ai.Id == 3);
        if (thirdAccommodationItem == null) return null;
        var creditCardPayments = bookingCreateDTO.Amex + bookingCreateDTO.MasterCard + bookingCreateDTO.Visa + bookingCreateDTO.DiscoveryCard + bookingCreateDTO.UnionPay + bookingCreateDTO.Diners;
        // Create a new Transaction instance
        var transaction = new Transaction
        {
            SalesTypeId = desiredSalesType.Id,
            OrganizationId = bookingCreateDTO.OrganizationId,
            Sequence = 0,
            UserId = bookingCreateDTO.UserId,
            NumOfGuests = 0,
            TransactionStartTime = DateTime.Now,
            TransactionCheckInTime = null,                                          // UpdateTransaction : Bookings.OrderBy(b => b.BookingId).First().CheckInTime; DONE
            TransacationCheckOutTime = null,                                                // UpdateTransaction : Bookings.OrderBy(b => b.BookingId).Last().CheckOutTimeScheduled; DONE
            TotalDeposits = 0,                                                              // UpdateTransaction : Bookings.Sum(b => b.TotalDeposits);
            TotalReturnedDeposits = 0,
            TotalNonReturnedDeposits = 0,                                                   // UpdateTransaction : Bookings.Where(b => b.NonRefundedDeposits.HasValue).Sum(b => b.NonRefundedDeposits.Value); DONE
            TotalCoupons = 0,                                                               // UpdateTransaction : Bookings.Sum(b => b.CouponsDiscount); DONE
            TotalRefunds = 0,                                                               // UpdateTransaction : Bookings.Sum(b => b.Refunded); DONE
            SubTotal = 0,                                                                   // UpdateTransaction : Bookings.Sum(b => b.SubTotal); DONE
            Taxes = 0,                                                                      // UpdateTransaction : Bookings.Sum(b => b.TotalTaxes); DONE
            GrandTotal = 0,                                                                 // UpdateTransaction : Bookings.Sum(b => b.Total); DONE
            AmountTendered = 0,
            Change = 0,
            TotalTransfer = 0,
            TotalVoids = 0,
            TotalCancelled = 0,
            StoredOrder = 0,
            IsVoid = false,
            Cash = 0,                                    // UpdateTransaction : if (Bookings.Count() > 0) => Bookings.Sum(b => b.Cash); else if (MembershipSales.Count() > 0) => MembershipSales.Sum(m => m.Cash);
            Debit = 0,
            Amex = 0,
            MasterCard = 0,
            Visa = 0,
            DiscoveryCard = 0,
            UnionPay = 0,
            Diners = 0,
            TotalUSDollars = 0,
            USExchangePaid = 0,
            USExchangeReceived = 0,
            RateTotal = 0,                                                               // UpdateTransaction : Bookings.Sum(b => b.RateTotal); DONE
            CreatedByUserId = bookingCreateDTO.UserId,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };
        // Save the Transaction to the database
        _unitOfWork.Repository<Transaction>().Add(transaction);
        var result = await _unitOfWork.Complete();
        if (result <= 0) return null!;
        int transactionId = transaction.Id;
        // Create a new Booking instance
        var booking = new Booking
        {
            TransactionId = transactionId,
            TimeCreated = DateTime.Now,
            CheckInTime = DateTime.Now,
            SubTotal = 0,                                                   // (BookingDetails.Where(d => !d.IsSubItem).Sum(d => d.Qty * (d.Price))) - Taxes; DONE
            TotalDeposits = 0,                                              // BookingDetails.Where(d => d.IsSubItem & d.HasDeposit & d.ChargableDeposit).Sum(d => (d.ChargeableQty > 0) ? (d.ChargeableQty * d.Price) : (d.QtyToAdd > 0) ? (d.QtyToAdd * d.Price) : (d.Qty - d.Transfered) * d.Price); DONE
            Refunded = 0,
            Total = 0,                                                      // RateTotal + TotalDiscount + TotalDeposits + MembershipSales + returnedDeposits + TotalSundrySale; DONE
            AmountPaid = 0,
            Tendered = 0,
            Change = bookingCreateDTO.Change,
            Cash = bookingCreateDTO.Cash,
            Debit = bookingCreateDTO.Debit,
            Amex = bookingCreateDTO.Amex,
            MasterCard = bookingCreateDTO.MasterCard,
            Visa = bookingCreateDTO.Visa,
            DiscoveryCard = bookingCreateDTO.DiscoveryCard,
            UnionPay = bookingCreateDTO.UnionPay,
            Diners = bookingCreateDTO.Diners,
            TotalDiscount = 0,                                              // MemberDiscount + RateDiscount + OtherDiscountDollar + CouponsDiscount + PROMODiscount; DONE
            TaxRate = 0.13m,                                                // HST * 0.01
            Comment = bookingCreateDTO.Comment,
            RateDetails = null,
            // "Check-In " + DateTime.Now + " - Check-Out " + DateTime.Now.AddHours(6) + "\nRates:\n" + rateName + "6 hours $" + bookingCreateDTO.TotalPrice, DONE
            TotalTaxes = 0,                                                 // Taxes + SundryTax;      Taxes = Math.Round(RateTotal - (RateTotal / (1 + TaxRate)), 2); DONE
            TimeCheckedOut = null,
            WakeUpCall = null,
            NonRefundedDeposits = bookingCreateDTO.NonRefundedDeposits ?? null,
            RefundComment = null,
            RateTotal = 0,                                                  // BookingDetailsAccommodations.Sum(d => d.Qty * d.Price); DONE
            RateDiscount = 0,
            MemberDiscount = 0,
            OtherDiscountDollar = bookingCreateDTO.OtherDiscountDollar,
            OtherDiscountReason = bookingCreateDTO.OtherDiscountReason,
            Rate = 0,
            CouponsDiscount = 0,
            PromoDiscount = 0,
            TimeCompleted = null,
            CheckedOutByEmployee = bookingCreateDTO.CheckedOutByEmployee ?? null,
            NewCreditCardNumber = null,
            NewCardExpiryDate = null,
            ShiftBookedIn = bookingCreateDTO.ShiftBookedIn,
            ShiftBookedOut = bookingCreateDTO.ShiftBookedOut,
            ReceiptMessage1 = desiredreceiptMessageAndCoupon.Message1,
            ReceiptMessage2 = desiredreceiptMessageAndCoupon.Message2,
            ReceiptCouponText = desiredreceiptMessageAndCoupon.CouponText,
            Disclaimer = desiredreceiptMessageAndCoupon.Disclaimer,
            USDollars = bookingCreateDTO.USDollars,
            EuroDollars = bookingCreateDTO.EuroDollars,
            USExchangeRate = usExchangeRate,
            EuroExchangeRate = euroExchangeRate,
            MembershipSales = 0,
            NetAmount = 0,
            OrganizationId = bookingCreateDTO.OrganizationId,
            CreatedByUserId = bookingCreateDTO.UserId,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };
        // Perform any additional operations or calculations here before saving the booking
        // Save the booking to the database
        _unitOfWork.Repository<Booking>().Add(booking);
        accommodation.AccommodationStatusId = 2;
        result = await _unitOfWork.Complete();
        if (result <= 0) return null!;
        int bookingId = booking.Id;
        var bookingDetails = new List<BookingDetail>
        {
            new BookingDetail
            {
                BookingId = bookingId,
                AccommodationId = accommodation.Id,
                AccommodationRateId = bookingCreateDTO.AccommodationRateId,
                BookingDetailStatusId = desiredBookingDetailStatus.Id,
                CouponId = bookingCreateDTO.CouponId > 0 ? bookingCreateDTO.CouponId : null,
                PromopassId = bookingCreateDTO.PromopassId > 0 ? bookingCreateDTO.PromopassId : null,
                DiscountRateId = bookingCreateDTO.DiscountRateId ?? null,
                MembershipId = bookingCreateDTO.MembershipId ?? null,
                HoursOfStay = bookingCreateDTO.Hours,
                Qty = 1,
                Price = bookingCreateDTO.TotalPrice,
                HasDeposit = false,
                DiscountFactor = 1.0m,
                NonMemberDiscount = 0,
                PriceDiscount = 0,
                Returned = 0,
                Transfered = 0,
                MemberDiscountDetail = 0,
                AccommodationCouponDiscount = bookingCreateDTO.CouponsDiscount,
                AccommodationPromopassDiscount = bookingCreateDTO.PromoDiscount,
                TransferDepositDetail = 0,
                TransferOut = 0,
                TransferDepositsReturned = 0,
                AccommodationOtherDiscount = 0,
                AccommodationOtherDisPercent = 0,
                CheckOutTimeRequested = DateTime.Now.AddHours(6),
                ChargableDeposit = false,
                DepositTransRefund = 0m,
                IsKeyRefundedOnly = false,
                OrganizationId = bookingCreateDTO.OrganizationId,
                CreatedByUserId = bookingCreateDTO.UserId,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            }
        };
        #region According to client Accommodation Items are not required
        //if (desiredAccommodationType.Id == 1)
        //{
        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = firstAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 0,
        //        Qty = 1,
        //        Price = firstAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });

        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = secondAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 0,
        //        Qty = 1,
        //        Price = secondAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });
        //}

        //if (desiredAccommodationType.Id == 2)
        //{
        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = firstAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 0,
        //        Qty = 1,
        //        Price = firstAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });

        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = secondAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 0,
        //        Qty = 1,
        //        Price = secondAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });
        //}

        //if (desiredAccommodationType.Id == 3)
        //{
        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = firstAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 0,
        //        Qty = 1,
        //        Price = firstAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });

        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = secondAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 0,
        //        Qty = 1,
        //        Price = secondAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });

        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = thirdAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 0,
        //        Qty = 1,
        //        Price = thirdAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });
        //}

        //if (desiredAccommodationType.Id == 4)
        //{
        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = firstAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 0,
        //        Qty = 1,
        //        Price = firstAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });

        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = secondAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 0,
        //        Qty = 1,
        //        Price = secondAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });

        //    bookingDetails.Add(new BookingDetail
        //    {
        //        BookingId = bookingId,
        //        AccommodationId = accommodation.Id,
        //        AccommodationItemId = thirdAccommodationItem.Id,
        //        BookingDetailStatusId = 5,
        //        HoursOfStay = 6,
        //        Qty = 1,
        //        Price = thirdAccommodationItem.Price,
        //        HasDeposit = false,
        //        Returned = 0,
        //        Transfered = 0,
        //        AccommodationCouponDiscount = 0m,
        //        AccommodationPromopassDiscount = 0m,
        //        TransferDepositDetail = 0m,
        //        TransferOut = 0m,
        //        TransferDepositsReturned = 0m,
        //        ChargableDeposit = false,
        //        DepositTransRefund = 0m,
        //        IsKeyRefundedOnly = false
        //    });
        //}

        #endregion
        _unitOfWork.Repository<BookingDetail>().AddRange(bookingDetails);
        result = await _unitOfWork.Complete();
        if (result <= 0) return null!;
        // Recalculate missing fields in booking on the basis of booking details
        booking.RateTotal = bookingDetails.Sum(bd => bd.Qty * bd.Price);
        var taxes = Math.Round(booking.RateTotal - (booking.RateTotal / (1 + booking.TaxRate!)), 2);
        booking.SubTotal = bookingDetails.Sum(bd => bd.Qty * bd.Price) - taxes;
        booking.TotalDeposits = bookingDetails.Where(bd => bd.HasDeposit & bd.ChargableDeposit).Sum(bd => (bd.Qty - bd.Transfered) * bd.Price);
        booking.MemberDiscount = bookingDetails.Sum(bd => bd.MemberDiscountDetail);
        var checkOutTime = booking.CheckInTime.AddHours(bookingDetails.FirstOrDefault(bd => bd.BookingId == booking.Id)!.HoursOfStay);
        booking.RateDetails = "Check-In: " + booking.CheckInTime.ToString("ddd h:mmtt") + "   -   Check-Out: " + checkOutTime.ToString("ddd h:mmtt") + Environment.NewLine + "Rates:";
        booking.RateDiscount = bookingDetails.Sum(bd => bd.Qty * bd.PriceDiscount);
        booking.CouponsDiscount = bookingDetails.Sum(bd => bd.AccommodationCouponDiscount);
        booking.PromoDiscount = bookingDetails.Sum(bd => bd.AccommodationPromopassDiscount);
        // Calculation of OtherDiscountDollar is sophisticated (Involves Availability)
        booking.TotalDiscount = booking.MemberDiscount + booking.RateDiscount + booking.OtherDiscountDollar + booking.CouponsDiscount + booking.PromoDiscount;
        decimal returnedDeposits = bookingDetails.Last().DepositTransRefund;
        booking.Total = booking.RateTotal + booking.TotalDiscount + booking.TotalDeposits + booking.MembershipSales + returnedDeposits; //+booking.TotalSundrySale
        booking.AmountPaid = booking.Tendered;
        booking.TotalTaxes = taxes; //+SundryTax
        booking.Rate = booking.RateTotal - taxes;
        booking.TimeCompleted = DateTime.Now;
        booking.NetAmount = booking.RateTotal + booking.TotalDiscount + booking.MembershipSales; // +booking.TotalSundrySale
        booking.OrganizationId = bookingCreateDTO.OrganizationId;
        booking.EditedByUserId = bookingCreateDTO.UserId;
        booking.EditedDate = DateTime.Now;

        // Recalculate missing fields in transaction on the basis of booking
        transaction.TransactionCheckInTime = booking.CheckInTime;
        transaction.TransacationCheckOutTime = checkOutTime;
        transaction.TotalDeposits = booking.TotalDeposits;
        transaction.TotalNonReturnedDeposits = booking.NonRefundedDeposits ?? 0;
        transaction.TotalCoupons = booking.CouponsDiscount;
        transaction.TotalRefunds = booking.Refunded;
        transaction.SubTotal = booking.SubTotal;
        transaction.Taxes = booking.TotalTaxes;
        transaction.GrandTotal = booking.Total;
        transaction.Cash = booking.Cash;
        transaction.Debit = booking.Debit;
        transaction.Amex = booking.Amex;
        transaction.MasterCard = booking.MasterCard;
        transaction.Visa = booking.Visa;
        transaction.DiscoveryCard = booking.DiscoveryCard;
        transaction.UnionPay = booking.UnionPay;
        transaction.Diners = booking.Diners;
        transaction.Change = booking.Change;
        transaction.TotalUSDollars = booking.USDollars;
        transaction.RateTotal = booking.RateTotal;
        transaction.OrganizationId = bookingCreateDTO.OrganizationId;
        transaction.EditedByUserId = bookingCreateDTO.UserId;
        transaction.EditedDate = DateTime.Now;

        _unitOfWork.Repository<Booking>().Update(booking);
        _unitOfWork.Repository<Transaction>().Update(transaction);
        result = await _unitOfWork.Complete();
        if (result <= 0) return null!;
        // Map the Booking entity to a BookingDTO
        var bookingDTO = new BookingDTO
        {
            TransactionId = booking.TransactionId,
            CheckInTime = booking.CheckInTime,
            SubTotal = booking.SubTotal,
            TotalDeposits = booking.TotalDeposits,
            Refunded = booking.Refunded,
            Total = booking.Total,
            AmountPaid = booking.AmountPaid,
            Tendered = booking.Tendered,
            Change = booking.Change,
            Cash = booking.Cash,
            Debit = booking.Debit,
            Amex = booking.Amex,
            MasterCard = booking.MasterCard,
            Visa = booking.Visa,
            DiscoveryCard = booking.DiscoveryCard,
            UnionPay = booking.UnionPay,
            Diners = booking.Diners,
            TaxRate = booking.TaxRate,
            Comment = booking.Comment,
            RateDetails = booking.RateDetails,
            TotalTaxes = booking.TotalTaxes,
        };
        GenerateAndSaveInvoicePdf(booking.Id.ToString(), "Bos", booking.CheckInTime, DateTime.Now, desiredAccommodationType.TypeName!, accommodation.Number!, booking.Rate.ToString(), booking.TaxRate, booking.NetAmount, booking.TotalDeposits, booking.Total, booking.Tendered, booking.Disclaimer!, booking.ReceiptMessage1!);
        return bookingDTO;
    }

    public async Task<bool> CheckOutAccommodationAsync(int accommodationId)
    {
        // Step 1: Get the accommodation and update its status
        var accommodation = await _unitOfWork.Repository<Accommodation>().GetByIdAsync(accommodationId) ?? throw new Exception("Accommodation not found");
        if (accommodation.AccommodationTypeId == 1)
        {
            accommodation.AccommodationStatusId = 5;
        }
        else
        {
            accommodation.AccommodationStatusId = 4;
        }
        // Step 2: Get the booking data using booking details with the help of accommodationId
        var bookingDetails = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(bd => bd.AccommodationId == accommodationId && bd.BookingDetailStatusId == 5);
        var bookingDetail = bookingDetails.LastOrDefault() ?? throw new Exception("BookingDetail not found");
        bookingDetail.BookingDetailStatusId = 1;
        var bookingId = bookingDetail.BookingId;
        var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId) ?? throw new Exception("Booking not found for specified BookingDetail");
        booking.TimeCheckedOut = DateTime.Now;
        var transactionId = booking.TransactionId;
        var transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(transactionId) ?? throw new Exception("Transaction not found for specified Booking");
        transaction.TransacationCheckOutTime = DateTime.Now;
        var result = await _unitOfWork.Complete();
        if (result <= 0) return false;
        return true;
    }

    public async Task<bool> RefundAmountAsync(int organizationId, int accommodationId)
    {
        var allBookingList = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.AccommodationId == accommodationId && x.BookingDetailStatusId == 5);
        var bookingAccommodation = allBookingList.FirstOrDefault();
        if (bookingAccommodation == null) return false;
        var bookingId = bookingAccommodation.BookingId;
        var existingBookings = await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == bookingId);
        var existingBooking = existingBookings.FirstOrDefault();
        if (existingBooking == null) return false;
        existingBooking.Refunded = existingBooking.Total;
        existingBooking.EditedDate = DateTime.Now;
        var existingTransactions = await _unitOfWork.Repository<Transaction>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == existingBooking.TransactionId);
        var existingTransaction = existingTransactions.FirstOrDefault();
        if (existingTransaction == null) return false;
        existingTransaction.TotalRefunds = existingTransaction.GrandTotal;
        existingTransaction.EditedDate = DateTime.Now;
        _unitOfWork.Repository<Booking>().Update(existingBooking);
        _unitOfWork.Repository<Transaction>().Update(existingTransaction);
        var result = await _unitOfWork.Complete();
        if (result <= 0) return false!;
        var response = await CheckOutAccommodationAsync(accommodationId);
        if (response == false) return false;
        return true;
    }

    public async Task<bool> RenewAccommodationAsync(int organizationId, int accommodationId)
    {
        var allBookingList = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodationId);
        var bookingAccommodation = allBookingList.FirstOrDefault();
        if (bookingAccommodation == null) return false;
        bookingAccommodation.HoursOfStay = bookingAccommodation.HoursOfStay + 6;
        var bookingId = bookingAccommodation.BookingId;
        var accommodations = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodationId);
        var accommodation = accommodations.FirstOrDefault();
        if (accommodation == null) return false;
        var existingBookings = await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == bookingId);
        var existingBooking = existingBookings.FirstOrDefault();
        if (existingBooking == null) return false;
        var accommodationTypes = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodation.AccommodationTypeId);
        var desiredAccommodationType = accommodationTypes.FirstOrDefault();
        if (desiredAccommodationType == null) return false;
        string[] weekendDays = { "Saturday", "Sunday" };
        string dayName = DateTime.Now.DayOfWeek.ToString();
        var rateName = "";
        if(weekendDays.Contains(dayName))
        {
            rateName = "Weekend Standard - " + desiredAccommodationType.TypeName;
        }
        else
        {
            rateName = "Weekday Standard - " + desiredAccommodationType.TypeName;
        }
        var accommodationRates = await _unitOfWork.Repository<AccommodationRate>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.AccommodationTypeId == accommodation.AccommodationTypeId && x.RateName == rateName);
        var desiredAccommodationRate = accommodationRates.FirstOrDefault();
        if (desiredAccommodationRate == null) return false;
        existingBooking.CheckInTime = existingBooking.CheckInTime.AddHours(6);
        existingBooking.TimeCheckedOut = null;
        existingBooking.RateDetails = "Check-In " + existingBooking.TimeCheckedOut + " - Check-Out " + existingBooking.TimeCheckedOut + "\nRates:\n" +
                rateName + "" + bookingAccommodation.HoursOfStay + " hours $" + desiredAccommodationRate.TotalPrice;
        _unitOfWork.Repository<Booking>().Update(existingBooking);
        _unitOfWork.Repository<BookingDetail>().Update(bookingAccommodation);
        var result = await _unitOfWork.Complete();
        if (result <= 0) return false!;
        return true;
    }
    // Create a method for generating the PDF and saving it
    public static void GenerateAndSaveInvoicePdf(string invoiceNumber, string staffName, DateTime checkInTime, DateTime checkOutTime,
        string accommodationType, string accommodationNumber, string rate, decimal taxes, decimal netAmount, decimal totalDeposits,
        decimal grandTotal, decimal totalTendered, string disclaimer, string receiptMessage1)
    {
        double pageWidthInMm = 78;
        double pageHeightInMm = 150; // Adjust the height as needed

        // Create a new PDF document
        PdfDocument document = new();
        PdfPage page = document.AddPage();
        page.Width = XUnit.FromMillimeter(pageWidthInMm);
        page.Height = XUnit.FromMillimeter(pageHeightInMm);
        XGraphics gfx = XGraphics.FromPdfPage(page);

        var staticFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Content");

        // Load the logo image
        var logoPath = Path.Combine(staticFilePath, "images", "ClubHamiltonLogoSm.png");
        XImage logo = XImage.FromFile(logoPath);

        // Define fonts and styles
        XFont titleFont = new("Helvetica", 12, XFontStyle.Bold);
        XFont titleFontUnderline = new("Helvetica", 12, XFontStyle.Bold | XFontStyle.Underline);
        XFont contentFont = new("Helvetica", 10);
        XBrush brush = XBrushes.Black;

        double leftMargin = 5;
        double rightMargin = page.Width - 5;
        double textWidth, center;

        double desiredLogoWidth = 50;
        double desiredLogoHeight = 25;
        double x_img = (page.Width - XUnit.FromMillimeter(desiredLogoWidth)) / 2;
        double y_img = 5;
        gfx.DrawImage(logo, x_img, y_img, XUnit.FromMillimeter(desiredLogoWidth), XUnit.FromMillimeter(desiredLogoHeight));

        double y = 85;

        string now = DateTime.Now.ToString("MMddyyyy");
        invoiceNumber = now + invoiceNumber;

        // Calculate the X-coordinate to center the title
        textWidth = gfx.MeasureString("Booked", titleFontUnderline).Width;
        center = (page.Width - textWidth) / 2;
        // Draw the title in the center
        gfx.DrawString("Booked", titleFontUnderline, brush, center, y);
        y += 20;

        // Draw the invoice information
        textWidth = gfx.MeasureString(invoiceNumber, contentFont).Width;
        gfx.DrawString("Invoice Number: ", contentFont, brush, leftMargin, y);
        gfx.DrawString(invoiceNumber, contentFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(staffName, contentFont).Width;
        gfx.DrawString("Staff Name: ", contentFont, brush, leftMargin, y);
        gfx.DrawString(staffName, contentFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(checkInTime.ToString("yyyy-MM-dd HH:mm"), contentFont).Width;
        gfx.DrawString("Check-In Time: ", contentFont, brush, leftMargin, y);
        gfx.DrawString(checkInTime.ToString("yyyy-MM-dd HH:mm"), contentFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(checkOutTime.ToString("yyyy-MM-dd HH:mm"), contentFont).Width;
        gfx.DrawString("Check-Out Time: ", contentFont, brush, leftMargin, y);
        gfx.DrawString(checkOutTime.ToString("yyyy-MM-dd HH:mm"), contentFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(accommodationNumber, contentFont).Width;
        gfx.DrawString(accommodationType, contentFont, brush, leftMargin, y);
        gfx.DrawString(accommodationNumber, contentFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(rate, contentFont).Width;
        gfx.DrawString("Rate: ", contentFont, brush, leftMargin, y);
        gfx.DrawString(rate, contentFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(taxes.ToString("0.00"), contentFont).Width;
        gfx.DrawString("Taxes: $", contentFont, brush, leftMargin, y);
        gfx.DrawString(taxes.ToString("0.00"), contentFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(netAmount.ToString("0.00"), contentFont).Width;
        gfx.DrawString("Net Amount: $", contentFont, brush, leftMargin, y);
        gfx.DrawString(netAmount.ToString("0.00"), contentFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(totalDeposits.ToString("0.00"), contentFont).Width;
        gfx.DrawString("Total Deposits: $", contentFont, brush, leftMargin, y);
        gfx.DrawString(totalDeposits.ToString("0.00"), contentFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(grandTotal.ToString("0.00"), titleFont).Width;
        gfx.DrawString("Grand Total: $", titleFont, brush, leftMargin, y);
        gfx.DrawString(grandTotal.ToString("0.00"), titleFont, brush, rightMargin - textWidth, y);
        y += 15;

        textWidth = gfx.MeasureString(totalTendered.ToString("0.00"), contentFont).Width;
        gfx.DrawString("Total Tendered: $", contentFont, brush, leftMargin, y);
        gfx.DrawString(totalTendered.ToString("0.00"), contentFont, brush, rightMargin - textWidth, y);
        y += 10;

        double lineWidth = page.Width - 2 * leftMargin;
        XColor lineColor = XColors.Black;
        double lineThickness = 1.5; // Adjust the line thickness as needed

        gfx.DrawLine(new XPen(lineColor, lineThickness), leftMargin, y, leftMargin + lineWidth, y);
        y += 15;

        textWidth = gfx.MeasureString("Receipt Message", titleFontUnderline).Width;
        center = (page.Width - textWidth) / 2;
        // Draw the title in the center
        gfx.DrawString("Receipt Message", titleFontUnderline, brush, center, y);
        y += 15;
        // gfx.DrawString(receiptMessage1, contentFont, brush, leftMargin + 20, y);
        string[] receiptMessage1Lines = SplitTextIntoLines(receiptMessage1, contentFont, 5 * page.Width / 12, gfx);
        foreach (var line in receiptMessage1Lines)
        {
            gfx.DrawString(line, contentFont, brush, leftMargin, y);
            y += 10;
        }
        y += 10;

        gfx.DrawLine(new XPen(lineColor, lineThickness), leftMargin, y, leftMargin + lineWidth, y);
        y += 15;

        textWidth = gfx.MeasureString("Disclaimer", titleFontUnderline).Width;
        center = (page.Width - textWidth) / 2;
        gfx.DrawString("Disclaimer", titleFontUnderline, brush, center, y);
        y += 15;
        string[] disclaimerLines = SplitTextIntoLines(disclaimer, contentFont, rightMargin - leftMargin, gfx);
        foreach (var line in disclaimerLines)
        {
            gfx.DrawString(line, contentFont, brush, leftMargin, y);
            y += 12;
        }
        y += 10;

        // Define the path where you want to save the PDF
        // var invoiceDir = Path.Combine(Directory.GetCurrentDirectory(), "");
        string pdfFileName = "Invoice" + invoiceNumber + ".pdf";
        var pdfFilePath = Path.Combine(staticFilePath, pdfFileName);

        // Save the PDF to the specified location
        document.Save(pdfFilePath);
    }

    private static string[] SplitTextIntoLines(string text, XFont font, double maxWidth, XGraphics gfx)
    {
        List<string> lines = new List<string>();
        string[] words = text.Split(' ');

        StringBuilder currentLine = new StringBuilder();
        double currentLineWidth = 0;

        foreach (var word in words)
        {
            double wordWidth = gfx.MeasureString(word, font).Width;

            if (currentLineWidth + wordWidth <= maxWidth)
            {
                currentLine.Append(word);
                currentLine.Append(' ');
                currentLineWidth += wordWidth + gfx.MeasureString(" ", font).Width;
            }
            else
            {
                lines.Add(currentLine.ToString().Trim());
                currentLine.Clear();
                currentLine.Append(word);
                currentLine.Append(' ');
                currentLineWidth = wordWidth + gfx.MeasureString(" ", font).Width;
            }
        }

        if (currentLine.Length > 0)
        {
            lines.Add(currentLine.ToString().Trim());
        }

        return lines.ToArray();
    }

    private DayOfWeek GetDayOfWeekFromString(string? day)
    {
        Enum.TryParse(day, out DayOfWeek result);
        return result;
    }

    public async Task<BookingDetailDTO> TransferAccommodationAsync(int organizationId, int bookedAccommodationId)
    {
        //Already Booked Accommodation Details 
        var bookedAccommodationBookingDetails = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId  && x.AccommodationId == bookedAccommodationId && x.IsDeleted == false && x.BookingDetailStatusId == 5);
        var bookedAccommodationBookingDetail = bookedAccommodationBookingDetails.FirstOrDefault() ?? throw new Exception("Booking details not found for booked accommodation");

        var bookedAccommodationBookings = await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == bookedAccommodationBookingDetail.BookingId && x.IsDeleted == false);
        var bookedAccommodationBooking = bookedAccommodationBookings.FirstOrDefault() ?? throw new Exception("Booking not found for booked accommodation");

        // To Transfer Accommodation Details.
        //var bookingDetails = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.AccommodationId == accommodationId && x.IsDeleted == false);
        //var bookingDetail = bookingDetails.FirstOrDefault() ?? throw new Exception("Booking details not found");

        //var bookings = await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == bookingDetail.BookingId && x.IsDeleted == false);
        //var booking = bookings.FirstOrDefault() ?? throw new Exception("Booking not found"); 


        //_unitOfWork.Repository<BookingDetail>().GetByIdAsync(transferBookingDTO.AccommodationId)
        if (bookedAccommodationBookingDetail.CheckOutTimeRequested == null) return null;
        var checkOutTime = bookedAccommodationBookingDetail.CheckOutTimeRequested.Value.AddHours(-1);
        var currentDateTime = DateTime.Now;
        var accommodationLists = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.Id == bookedAccommodationId && x.OrganizationId == organizationId && x.IsDeleted == false);
        var accommodation = accommodationLists.FirstOrDefault() ?? throw new Exception("Accommodation not found");

        var accommodationTypesLists = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodation.AccommodationTypeId && x.IsDeleted == false);
        var desiredAccommodationType = accommodationTypesLists.FirstOrDefault() ?? throw new Exception("Accommodation Type not found");
        var bookedAccommodationWaitings = await _unitOfWork.Repository<WaitingList>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.AccommodationId == bookedAccommodationId && x.IsDeleted == false);
        //var accommodationRates = await _unitOfWork.Repository<AccommodationRate>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.IsActive == true);
        var currDate = DateTime.Now;

        //var accommodationRate = new AccommodationRateDTO();
        if (bookedAccommodationWaitings.Count <= 0)
        {
            if (bookedAccommodationBookingDetail.HoursOfStay <= 117 && currentDateTime <= checkOutTime)
            {
                //transferBookingDTO.RateTotal = bookedAccommodationBooking.RateTotal;
                //transferBookingDTO.RateDiscount = bookedAccommodationBooking.RateDiscount;
                //transferBookingDTO.TotalDiscount = bookedAccommodationBooking.TotalDiscount;
                //transferBookingDTO.NetAmount = bookedAccommodationBooking.NetAmount;
                //transferBookingDTO.TotalDeposits = bookedAccommodationBooking.TotalDeposits;
                //transferBookingDTO.Total = bookedAccommodationBooking.Total;
                //transferBookingDTO.IsTransfer = true;

                var bookingDetailDTO = new BookingDetailDTO
                {
                    BookingId = bookedAccommodationBookingDetail.BookingId,
                    AccommodationId = bookedAccommodationBookingDetail.AccommodationId,
                    AccommodationItemId=bookedAccommodationBookingDetail.AccommodationItemId,
                    AccommodationRateId=bookedAccommodationBookingDetail.AccommodationRateId,
                    BookingDetailStatusId=bookedAccommodationBookingDetail.BookingDetailStatusId,
                    CouponId=bookedAccommodationBookingDetail.CouponId,
                    PromopassId=bookedAccommodationBookingDetail.PromopassId,
                    MembershipId=bookedAccommodationBookingDetail.MembershipId,
                    HoursOfStay=bookedAccommodationBookingDetail.HoursOfStay,
                    Qty=bookedAccommodationBookingDetail.Qty,
                    Price=bookedAccommodationBookingDetail.Price,
                    HasDeposit = bookedAccommodationBookingDetail.HasDeposit,
                    Returned = bookedAccommodationBookingDetail.Returned,
                    Transfered = bookedAccommodationBookingDetail.Transfered,
                    AccommodationCouponDiscount = bookedAccommodationBookingDetail.AccommodationCouponDiscount,
                    AccommodationPromopassDiscount = bookedAccommodationBookingDetail.AccommodationPromopassDiscount,
                    TransferDepositDetail = bookedAccommodationBookingDetail.TransferDepositDetail,
                    TransferOut = bookedAccommodationBookingDetail.TransferOut,
                    TransferDepositsReturned = bookedAccommodationBookingDetail.TransferDepositsReturned,
                    AccommodationOtherDiscount = bookedAccommodationBookingDetail.AccommodationOtherDiscount,
                    AccommodationOtherDisPercent = bookedAccommodationBookingDetail.AccommodationOtherDisPercent,
                    CheckOutTimeRequested = bookedAccommodationBookingDetail.CheckOutTimeRequested,
                    ChargableDeposit = bookedAccommodationBookingDetail.ChargableDeposit,
                    DepositTransRefund = bookedAccommodationBookingDetail.DepositTransRefund,
                    IsKeyRefundedOnly = bookedAccommodationBookingDetail.IsKeyRefundedOnly,
                };

                //var checkOutSuccess = await CheckOutAccommodationAsync(bookedAccommodationId);
                //if (!checkOutSuccess)
                //{
                //    // Handle the case where checkout fails after transfer (optional)
                //    throw new Exception("Checkout of accommodation failed after transfer.");
                //}

                return bookingDetailDTO;
            }
        }

        throw new Exception("Transfer of accommodation cannot be processed becasue it is in waiting");

        //}
        ////return transferBookingDTO;

        //var bookingCreateDTO =new BookingCreateDTO
        //{
        //    AccommodationId = transferBookingDTO.AccommodationId,
        //    UserId = transferBookingDTO.UserId,
        //    Cash = 0,
        //    Debit = 0,
        //    Amex = 0,
        //    MasterCard = 0,
        //    Visa = 0,
        //    DiscoveryCard = 0,
        //    UnionPay = 0,
        //    Diners = 0,
        //    TotalUSDollars = 0,
        //    USExchangePaid = 0,
        //    UsExchangeReceived = 0,
        //    Change = 0,
        //    Comment = "",
        //    OtherDiscountDollar = 0,
        //    OtherDiscountReason = "",
        //    CouponId = 0,
        //    PromopassId = 0,
        //    CouponsDiscount = 0,
        //    PromoDiscount = 0,
        //    OrganizationId = transferBookingDTO.OrganizationId,
        //};
        //#region
        ////var bookingCreateDTO=new BookingCreateDTO
        ////{
        ////    AccommodationId = transferBookingDTO.AccommodationId,
        ////    EmployeeName = transferBookingDTO.EmployeeName,
        ////    Cash = (decimal)transferBookingDTO.Cash,
        ////    Debit = (decimal)transferBookingDTO.Debit,
        ////    Amex = (decimal)transferBookingDTO.Amex,
        ////    MasterCard = (decimal)transferBookingDTO.MasterCard,
        ////    Visa = (decimal)transferBookingDTO.Visa,
        ////    DiscoveryCard = (decimal)transferBookingDTO.DiscoveryCard,
        ////    UnionPay = (decimal)transferBookingDTO.UnionPay,
        ////    Diners = (decimal)transferBookingDTO.Diners,
        ////    TotalUSDollars = (decimal)transferBookingDTO.TotalUSDollars,
        ////    USExchangePaid = (decimal)transferBookingDTO.USExchangePaid,
        ////    UsExchangeReceived = (decimal)transferBookingDTO.UsExchangeReceived,
        ////    Change = (decimal)transferBookingDTO.Change,
        ////    Comment = transferBookingDTO.Comment,
        ////    OtherDiscountDollar = (decimal)transferBookingDTO.OtherDiscountDollar,
        ////    OtherDiscountReason = transferBookingDTO.OtherDiscountReason,
        ////    CouponId = (int)transferBookingDTO.CouponId,
        ////    PromopassId = (int)transferBookingDTO.PromopassId,
        ////    CouponsDiscount = (decimal)transferBookingDTO.CouponsDiscount,
        ////    PromoDiscount = (decimal)transferBookingDTO.PromoDiscount
        ////};
        //#endregion
        //var response = await CheckInAccommodationAsync(bookingCreateDTO);
        //if (response != null) transferBookingDTO.Response = true;
        ////return transferBookingDTO;
        //#region RAW CODE
        ////var bookingDetailList= await _unitOfWork.Repository<BookingDetail>().ListAllAsync();
        ////var bookingDetail= bookingDetailList.FirstOrDefault(x=>x.AccommodationId==transferBookingDTO.AccommodationId);
        ////if(bookingDetail == null)
        ////    return null;
        ////var bookingList= await _unitOfWork.Repository<Booking>().ListAllAsync();
        ////var booking= bookingList.FirstOrDefault(x=>x.BookingDetails.Where(x=>x.BookingId==bookingDetail.BookingId);
        ////var ser= bookingList.Where(x=>x.BookingDetails.Select(x=>x.BookingId)==bookingDetail.BookingId)
        ////if(booking == null)
        ////    return null;


        ////var accommodationTypes = await _unitOfWork.Repository<AccommodationType>().ListAllAsync();
        ////var desiredAccommodationType = accommodationTypes.FirstOrDefault(at => at.Id == accommodation.AccommodationTypeId);
        ////if(desiredAccommodationType == null)
        ////{
        ////    return null;
        ////};

        //// Get all Accommodation

        //// Get all Available Accommodations

        ////if(accommodation.AccommodationTypeId == 1)
        ////{

        ////}
        ////switch(accommodation.AccommodationTypeId)
        ////{
        ////    case 1:
        ////        break;
        ////}
        ////return transferBookingDTO;

        //#endregion
        //// Transfer Booking
        //#region Retreve Data from database
        //var accommodations = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.OrganizationId == transferBookingDTO.OrganizationId && x.Id == transferBookingDTO.AccommodationId && x.IsDeleted == false);
        //var accommodation = accommodations.FirstOrDefault() ?? throw new Exception("Waiting list not found");
        //if (accommodation == null) return null;
        //var accommodationTypes = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.OrganizationId == transferBookingDTO.OrganizationId && x.Id == accommodation.AccommodationTypeId && x.IsDeleted == false);
        //var desiredAccommodationType = accommodationTypes.FirstOrDefault() ?? throw new Exception("Accommodation type not found");
        //string[] weekendDays = { "Saturday", "Sunday" };
        //string dayName = DateTime.Now.DayOfWeek.ToString();
        //var rateName = "";
        //if(weekendDays.Contains(dayName))
        //{
        //    rateName = "Weekend Standard - " + desiredAccommodationType.TypeName;
        //}
        //else
        //{
        //    rateName = "Weekday Standard - " + desiredAccommodationType.TypeName;
        //}
        //var accommodationRates = await _unitOfWork.Repository<AccommodationRate>().ListAllWhereAsync(x => x.OrganizationId == transferBookingDTO.OrganizationId && x.AccommodationTypeId == accommodation.AccommodationTypeId && x.RateName == rateName && x.IsDeleted == false);
        //var desiredAccommodationRate = accommodationRates.FirstOrDefault() ?? throw new Exception("Accommodation rate not found");
        //var salesTypes = await _unitOfWork.Repository<SalesType>().ListAllWhereAsync(x => x.OrganizationId == transferBookingDTO.OrganizationId && x.SalesTypeName == "Booking" && x.IsDeleted == false);
        //var desiredSalesType = salesTypes.FirstOrDefault() ?? throw new Exception("Sales type not found");
        //var bookingDetailStatuses = await _unitOfWork.Repository<BookingDetailStatus>().ListAllWhereAsync(x => x.OrganizationId == transferBookingDTO.OrganizationId && x.BookingDetailStatusName == "Transfer" && x.IsDeleted == false);
        //var desiredBookingDetailStatus = bookingDetailStatuses.FirstOrDefault() ?? throw new Exception("Booking detail status not found");
        ////var accommodationItems = await _unitOfWork.Repository<AccommodationItem>().ListAllAsync();

        ////var firstAccommodationItem = accommodationItems.FirstOrDefault(ai => ai.Id == 1);
        ////if(firstAccommodationItem == null)
        ////{
        ////    return null;
        ////}

        ////var secondAccommodationItem = accommodationItems.FirstOrDefault(ai => ai.Id == 2);
        ////if(secondAccommodationItem == null)
        ////{
        ////    return null;
        ////}

        ////var thirdAccommodationItem = accommodationItems.FirstOrDefault(ai => ai.Id == 3);
        ////if(thirdAccommodationItem == null)
        ////{
        ////    return null;
        ////}
        //var creditCardPayments = transferBookingDTO.Amex + transferBookingDTO.MasterCard + transferBookingDTO.Visa + transferBookingDTO.DiscoveryCard + transferBookingDTO.UnionPay + transferBookingDTO.Diners;
        //var bookings= await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == transferBookingDTO.OrganizationId && x.IsDeleted == false);
        ////var booking = bookings.FirstOrDefault() ?? throw new Exception("Booking detail status not found");
        //#endregion
        //// Create a new Transaction instance
        //var transaction = new Transaction
        //{
        //    SalesTypeId = desiredSalesType.Id,
        //    UserId = transferBookingDTO.UserId,
        //    OrganizationId = transferBookingDTO.OrganizationId,
        //    Sequence = 0,
        //    NumOfGuests = 0,
        //    TransactionStartTime = DateTime.Now,
        //    TransactionCheckInTime = DateTime.Now,                                          // UpdateTransaction : Bookings.OrderBy(b => b.BookingId).First().CheckInTime;
        //    TransacationCheckOutTime = null,                                                // UpdateTransaction : Bookings.OrderBy(b => b.BookingId).Last().CheckOutTimeScheduled;
        //    TotalDeposits = 0,                                                              // UpdateTransaction : Bookings.Sum(b => b.TotalDeposits);
        //    TotalReturnedDeposits = 0,
        //    TotalNonReturnedDeposits = 0,                                        // UpdateTransaction : Bookings.Where(b => b.NonRefundedDeposits.HasValue).Sum(b => b.NonRefundedDeposits.Value);
        //    TotalCoupons = 0,                                                               // UpdateTransaction : Bookings.Sum(b => b.CouponsDiscount);
        //    TotalRefunds = 0,                                                               // UpdateTransaction : Bookings.Sum(b => b.Refunded);
        //    SubTotal = 0,                                                                   // UpdateTransaction : Bookings.Sum(b => b.SubTotal);
        //    Taxes = 0,                                                                      // UpdateTransaction : Bookings.Sum(b => b.TotalTaxes);
        //    GrandTotal = 0,                                                                 // UpdateTransaction : Bookings.Sum(b => b.Total);
        //    AmountTendered = 0,
        //    Change = 0,
        //    TotalTransfer = 0,
        //    TotalVoids = 0,
        //    TotalCancelled = 0,
        //    StoredOrder = 0,
        //    IsVoid = false,
        //    Cash = 0,                     // UpdateTransaction : if (Bookings.Count() > 0) => Bookings.Sum(b => b.Cash); else if (MembershipSales.Count() > 0) => MembershipSales.Sum(m => m.Cash);
        //    Debit = (decimal)transferBookingDTO.Debit,
        //    Amex = (decimal)transferBookingDTO.Amex,
        //    MasterCard = (decimal)transferBookingDTO.MasterCard,
        //    Visa = (decimal)transferBookingDTO.Visa,
        //    DiscoveryCard = (decimal)transferBookingDTO.DiscoveryCard,
        //    UnionPay = (decimal)transferBookingDTO.UnionPay,
        //    Diners = (decimal)transferBookingDTO.Diners,
        //    TotalUSDollars = (decimal)transferBookingDTO.TotalUSDollars,
        //    USExchangePaid = 0,
        //    USExchangeReceived = 0,
        //    RateTotal = 0,                                                                   // UpdateTransaction : Bookings.Sum(b => b.RateTotal);
        //    IsDeleted = false,
        //    CreatedByUserId = transferBookingDTO.UserId,
        //    CreatedDate = DateTime.Now,
        //};
        //// Save the Transaction to the database
        //_unitOfWork.Repository<Transaction>().Add(transaction);
        //var result = await _unitOfWork.Complete();
        //if(result <= 0) return null!;
        //int transactionId = transaction.Id;
        //// Create a new Booking instance
        //var bookingInstance = new Booking 
        //{
        //    TransactionId = transactionId,
        //    TimeCreated = DateTime.Now,
        //    CheckInTime = DateTime.Now,
        //    SubTotal = 0,                                                   // RecalculateTotatls : (BookingDetails.Where(d => !d.IsSubItem).Sum(d => d.Qty * (d.Price))) - Taxes;
        //    TotalDeposits = 0,                                              // RecalculateTotatls : BookingDetails.Where(d => d.IsSubItem & d.HasDeposit & d.ChargableDeposit).Sum(d => (d.ChargeableQty > 0) ? (d.ChargeableQty * d.Price) : (d.QtyToAdd > 0) ? (d.QtyToAdd * d.Price) : (d.Qty - d.Transfered) * d.Price);
        //    Refunded = 0,
        //    Total = 0,                                      // RecalculateTotatls : RateTotal + TotalDiscount + TotalDeposits + MembershipSales + returnedDeposits + TotalSundrySale;
        //    AmountPaid = 0,
        //    Tendered = 0,
        //    Change = 0,
        //    Cash = 0,
        //    Debit = (decimal)transferBookingDTO.Debit,
        //    Amex = (decimal)transferBookingDTO.Amex,
        //    MasterCard = (decimal)transferBookingDTO.MasterCard,
        //    Visa = (decimal)transferBookingDTO.Visa,
        //    DiscoveryCard = (decimal)transferBookingDTO.DiscoveryCard,
        //    UnionPay = (decimal)transferBookingDTO.UnionPay,
        //    Diners = (decimal)transferBookingDTO.Diners,
        //    TaxRate = 0.13m,
        //    Comment = transferBookingDTO.Comment,
        //    OtherDiscountDollar = (decimal)transferBookingDTO.OtherDiscountDollar,
        //    OtherDiscountReason = transferBookingDTO.OtherDiscountReason,
        //    RateDetails = "Check-In " + DateTime.Now + " - Check-Out " + DateTime.Now.AddHours(6) + "\nRates:\n" +
        //        rateName + "6 hours $" + desiredAccommodationRate.TotalPrice,
        //    TotalTaxes = 0,                                                 // RecalculateTotatls : Taxes + SundryTax;      Taxes = Math.Round(RateTotal - (RateTotal / (1 + TaxRate)), 2);
        //    TimeCheckedOut=DateTime.Now.AddHours(6),
        //    RateTotal = 0,                                                  // RecalculateTotatls : BookingDetailsAccommodations.Sum(d => d.Qty * d.Price);
        //    Rate = 0,                                                       // RecalculateTotatls : RateTotal - Taxes;
        //    ShiftBookedIn = 0,
        //    ShiftBookedOut = 0,
        //    USDollars = 0m,
        //    EuroDollars = 0m,
        //    USExchangeRate = 0m,
        //    EuroExchangeRate = 0m,
        //    NetAmount = 0,
        //    OrganizationId = transferBookingDTO.OrganizationId,
        //    IsDeleted = false,
        //    CreatedByUserId = transferBookingDTO.UserId,
        //    CreatedDate = DateTime.Now,
        //};
        //// Perform any additional operations or calculations here before saving the booking
        //// Save the booking to the database
        //_unitOfWork.Repository<Booking>().Add(bookingInstance);
        //accommodation.AccommodationStatusId = 2;
        //result = await _unitOfWork.Complete();
        //if(result <= 0) return null!;
        //int bookingId = bookingInstance.Id;
        ////var bookingDetails = new List<BookingDetail>();
        //var bookingDetailCreate = new BookingDetail
        //{
        //    BookingId = bookingId,
        //    AccommodationId = transferBookingDTO.AccommodationId,
        //    AccommodationRateId = desiredAccommodationRate.Id,
        //    BookingDetailStatusId = desiredBookingDetailStatus.Id,
        //    CouponId = transferBookingDTO.CouponId ?? null,
        //    PromopassId = transferBookingDTO.PromopassId ?? null,
        //    HoursOfStay = desiredAccommodationRate.Hours,
        //    Qty = 1,
        //    Price = desiredAccommodationRate.TotalPrice,
        //    HasDeposit = false,
        //    Returned = 0,
        //    Transfered = 1,
        //    AccommodationCouponDiscount = (decimal)transferBookingDTO.CouponsDiscount,
        //    AccommodationPromopassDiscount = (decimal)transferBookingDTO.PromoDiscount,
        //    TransferDepositDetail = 0m,
        //    TransferOut = 0m,
        //    TransferDepositsReturned = 0m,
        //    CheckOutTimeRequested = DateTime.Now.AddHours(6),
        //    ChargableDeposit = false,
        //    DepositTransRefund = 0m,
        //    IsKeyRefundedOnly = false,
        //    OrganizationId = transferBookingDTO.OrganizationId,
        //    IsDeleted = false,
        //    CreatedByUserId = transferBookingDTO.UserId,
        //    CreatedDate = DateTime.Now,
        //};
        //_unitOfWork.Repository<BookingDetail>().Add(bookingDetailCreate);
        //result = await _unitOfWork.Complete();
        //if(result <= 0) return null!;
        //return transferBookingDTO;
    }
}