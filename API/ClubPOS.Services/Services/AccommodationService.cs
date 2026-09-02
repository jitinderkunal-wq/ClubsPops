using ClubPOS.Core.Entities;
using ClubPOS.Infrastructure.IRepositories;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClubPOS.Services.Services;

public class AccommodationService : IAccommodationService
{
    private readonly IUnitOfWork _unitOfWork;
    public AccommodationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private DayOfWeek GetDayOfWeekFromString(string? day)
    {
        Enum.TryParse(day, out DayOfWeek result);
        return result;
    }

    public async Task<AccommodationStatusTypeMaintenanceRateAndCouponsDTO?> GetAccommodationByIdAsync(int organizationId, int accommodationId)
    {
        var accommodationLists = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.Id == accommodationId && x.OrganizationId == organizationId && x.IsDeleted == false);
        var accommodation = accommodationLists.FirstOrDefault() ?? throw new Exception("Accommodation not found");

        var accommodationTypesLists = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodation.AccommodationTypeId && x.IsDeleted == false);
        var desiredAccommodationType = accommodationTypesLists.FirstOrDefault() ?? throw new Exception("Accommodation Type not found");

        var accommodationRates = await _unitOfWork.Repository<AccommodationRate>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.IsActive == true);
        DateTime currDate = DateTime.Now;

        //var desiredAccommodationRate = accommodationRates.FirstOrDefault(ar =>
        //    ar.AccommodationTypeId == accommodation.AccommodationTypeId &&
        //    (
        //        (
        //            ar.IsStandardRate == false && // For Non Standard Accommodation Rate
        //            currDate.DayOfWeek == GetDayOfWeekFromString(ar.StartDayOfTheWeek) &&
        //            currDate.DayOfWeek == GetDayOfWeekFromString(ar.EndDayOfWeek) &&
        //            currDate.TimeOfDay >= ar.RateStartTime?.TimeOfDay &&
        //            currDate.TimeOfDay <= ar.RateEndTime?.TimeOfDay
        //        )
        //        ||
        //        (
        //            ar.IsStandardRate == true && // For Standard Accommodation Rate
        //            currDate.DayOfWeek > GetDayOfWeekFromString(ar.StartDayOfTheWeek) &&
        //            currDate.DayOfWeek < GetDayOfWeekFromString(ar.EndDayOfWeek) &&
        //            (
        //                (
        //                    currDate.DayOfWeek == GetDayOfWeekFromString(ar.StartDayOfTheWeek) ||
        //                    currDate.DayOfWeek == GetDayOfWeekFromString(ar.EndDayOfWeek)
        //                ) &&
        //                    currDate.TimeOfDay >= ar.RateStartTime?.TimeOfDay &&
        //                    currDate.TimeOfDay <= ar.RateEndTime?.TimeOfDay
        //                ||
        //                (currDate.DayOfWeek != GetDayOfWeekFromString(ar.StartDayOfTheWeek) &&
        //                currDate.DayOfWeek != GetDayOfWeekFromString(ar.EndDayOfWeek))
        //            )
        //        )
        //    )
        //);

        var desiredAccommodationRate = accommodationRates.FirstOrDefault(ar =>
        {
            if (ar.AccommodationTypeId == accommodation.AccommodationTypeId)
            {
                if (ar.IsStandardRate == false)
                {
                    // For Non-Standard Accommodation Rate
                    return (
                        currDate.DayOfWeek == GetDayOfWeekFromString(ar.StartDayOfTheWeek) &&
                        currDate.DayOfWeek == GetDayOfWeekFromString(ar.EndDayOfWeek) &&
                        currDate.TimeOfDay >= ar.RateStartTime?.TimeOfDay &&
                        currDate.TimeOfDay <= ar.RateEndTime?.TimeOfDay
                    );
                }
                else if (ar.IsStandardRate == true)
                {
                    // For Standard Accommodation Rate
                    if (currDate.DayOfWeek > GetDayOfWeekFromString(ar.StartDayOfTheWeek) && currDate.DayOfWeek < GetDayOfWeekFromString(ar.EndDayOfWeek))
                    {
                        return true;
                    }
                    else if (currDate.DayOfWeek == GetDayOfWeekFromString(ar.StartDayOfTheWeek))
                    {
                        if (currDate.TimeOfDay >= ar.RateStartTime?.TimeOfDay)
                        {
                            return true;
                        }
                    }
                    else if (currDate.DayOfWeek == GetDayOfWeekFromString(ar.EndDayOfWeek))
                    {
                        if (currDate.TimeOfDay <= ar.RateEndTime?.TimeOfDay)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    //return (
                    //    (currDate.DayOfWeek > GetDayOfWeekFromString(ar.StartDayOfTheWeek) && currDate.DayOfWeek < GetDayOfWeekFromString(ar.EndDayOfWeek)) ||
                    //    (currDate.DayOfWeek == GetDayOfWeekFromString(ar.StartDayOfTheWeek) || currDate.DayOfWeek == GetDayOfWeekFromString(ar.EndDayOfWeek)) &&
                    //        currDate.TimeOfDay >= ar.RateStartTime?.TimeOfDay && currDate.TimeOfDay <= ar.RateEndTime?.TimeOfDay
                    //        ||
                    //        (currDate.DayOfWeek != GetDayOfWeekFromString(ar.StartDayOfTheWeek) &&
                    //         currDate.DayOfWeek != GetDayOfWeekFromString(ar.EndDayOfWeek))
                    //    )
                    //);
                }
            }

            return false; // Accommodation type doesn't match
        });

        if (desiredAccommodationRate == null) return null;

        var coupons = await _unitOfWork.Repository<Coupon>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);

        if (accommodation.AccommodationTypeId == 1)
        {
            if (IsWednesdayBetween5pmAnd12pm())
            {
                coupons = coupons.Where(coupon => coupon.Id == 1 || coupon.Id == 4).ToList();
            }
            else
            {
                coupons = coupons.Where(coupon => coupon.Id == 1).ToList();
            }
        }
        else if (accommodation.AccommodationTypeId == 2)
        {
            coupons = coupons.Where(coupon => coupon.Id == 1 || coupon.Id == 2).ToList();
        }
        else if (accommodation.AccommodationTypeId == 3 || accommodation.AccommodationTypeId == 4)
        {
            coupons = coupons.Where(coupon => coupon.Id == 1 || coupon.Id == 3).ToList();
        }

        var accommodationStatusTypeMaintenanceRateAndCouponsDTO = new AccommodationStatusTypeMaintenanceRateAndCouponsDTO
        {
            AccommodationId = accommodation.Id,
            AccommodationTypeId = accommodation.AccommodationTypeId,
            TypeName = desiredAccommodationType.TypeName,
            AccommodationStatusId = accommodation.AccommodationStatusId,
            StatusName = null!,
            Number = accommodation.Number,
            Floor = accommodation.Floor,
            DeActivate = accommodation.DeActivate,
            RequiresCreditCard = accommodation.RequiresCreditCard,
            HasMaintenanceReservation = accommodation.HasMaintenanceReservation,
            EmployeeLocker = accommodation.EmployeeLocker,
            IsGhost = accommodation.IsGhost,
            HasGloryHole = accommodation.HasGloryHole,
            AccommodationRate = new AccommodationRateDTO
            {
                AccommodationRateId = desiredAccommodationRate.Id,
                AccommodationTypeId = desiredAccommodationRate.AccommodationTypeId,
                Hours = desiredAccommodationRate.Hours,
                RateName = desiredAccommodationRate.RateName,
                Price = desiredAccommodationRate.Price,
                StartDayOfTheWeek = desiredAccommodationRate.StartDayOfTheWeek,
                RateStartTime = desiredAccommodationRate.RateStartTime,
                EndDayOfWeek = desiredAccommodationRate.EndDayOfWeek,
                RateEndTime = desiredAccommodationRate.RateEndTime,
                Tax1 = desiredAccommodationRate.Tax1,
                Tax2 = desiredAccommodationRate.Tax2,
                Tax3 = desiredAccommodationRate.Tax3,
                Tax4 = desiredAccommodationRate.Tax4,
                Tax5 = desiredAccommodationRate.Tax5,
                IsStandardRate = desiredAccommodationRate.IsStandardRate,
                TotalPrice = desiredAccommodationRate.TotalPrice,
                IsActive = desiredAccommodationRate.IsActive
            },
            Coupons = coupons?.Select(coupon => new CouponDTO
            {
                AccommodationTypeId = accommodation.AccommodationTypeId,
                CouponId = coupon.Id,
                DateCreated = coupon.DateCreated,
                CouponName = coupon.CouponName,
                Description = coupon.Description,
                CouponNumber = coupon.CouponNumber,
                FlatRate = coupon.FlatRate,
                DiscountDollars = coupon.DiscountDollars,
                DiscountPercent = coupon.DiscountPercent,
                StartDate = coupon.StartDate,
                ExpiryDate = coupon.ExpiryDate,
                IsExclusive = coupon.IsExclusive,
                IsActive = coupon.IsActive,
                IsCoupon = coupon.IsCoupon,
                ReturnOnCheckOut = coupon.ReturnOnCheckOut
            }).ToList()
        };

        if (accommodationStatusTypeMaintenanceRateAndCouponsDTO.AccommodationStatusId.HasValue)
        {
            int accommodationStatusId = accommodationStatusTypeMaintenanceRateAndCouponsDTO.AccommodationStatusId.Value;
            var accommodationStatus = await _unitOfWork.Repository<AccommodationStatus>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodationStatusId && x.IsDeleted == false);
            accommodationStatusTypeMaintenanceRateAndCouponsDTO.StatusName = accommodationStatus.First().StatusName;
        }

        return accommodationStatusTypeMaintenanceRateAndCouponsDTO;
    }

    public async Task<IReadOnlyList<AccommodationStatusTypeMaintenanceLogWaitingListDTO>> GetAccommodationsAsync(int organizationId, int? accommodationTypeId, int? accommodationStatusId)
    {
        var accommodations = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.DeActivate == false);
        var accommodationTypes = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var accommodationStatuses = await _unitOfWork.Repository<AccommodationStatus>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var maintenanceTasks = await _unitOfWork.Repository<MaintenanceTask>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var maintenanceLogs = await _unitOfWork.Repository<MaintenanceLog>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var maintenanceLogDetails = await _unitOfWork.Repository<MaintenanceLogDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var waitingLists = await _unitOfWork.Repository<WaitingList>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);

        var accommodationStatusTypeMaintenanceLogWaitingListDTO = from a in accommodations
                                                                  join at in accommodationTypes on a.AccommodationTypeId equals at.Id
                                                                  join ast in accommodationStatuses on a.AccommodationStatusId equals ast.Id
                                                                  select new AccommodationStatusTypeMaintenanceLogWaitingListDTO
                                                                  {
                                                                      AccommodationId = a.Id,
                                                                      AccommodationTypeId = at.Id,
                                                                      TypeName = at.TypeName!,
                                                                      AccommodationStatusId = ast.Id,
                                                                      StatusName = ast.StatusName!,
                                                                      Number = a.Number,
                                                                      Floor = a.Floor,
                                                                      DeActivate = a.DeActivate,
                                                                      RequiresCreditCard = a.RequiresCreditCard,
                                                                      HasMaintenanceReservation = a.HasMaintenanceReservation,
                                                                      EmployeeLocker = a.EmployeeLocker,
                                                                      IsGhost = a.IsGhost,
                                                                      HasGloryHole = a.HasGloryHole,
                                                                      OrganizationId = a.OrganizationId,
                                                                  };

        if (accommodationTypeId.HasValue)
        {
            accommodationStatusTypeMaintenanceLogWaitingListDTO = accommodationStatusTypeMaintenanceLogWaitingListDTO.Where(a => a.AccommodationTypeId == accommodationTypeId.Value);
        }

        if (accommodationStatusId.HasValue)
        {
            accommodationStatusTypeMaintenanceLogWaitingListDTO = accommodationStatusTypeMaintenanceLogWaitingListDTO.Where(a => a.AccommodationStatusId == accommodationStatusId.Value);
        }

        // if (!string.IsNullOrEmpty(sort))
        // {
        //     switch (sort)
        //     {
        //         case "accommodationStatusAsc":
        //             accommodationStatusTypeMaintenanceLogWaitingListDTO = accommodationStatusTypeMaintenanceLogWaitingListDTO.OrderBy(a => a.StatusName);
        //             break;
        //         case "accommodationStatusDesc":
        //             accommodationStatusTypeMaintenanceLogWaitingListDTO = accommodationStatusTypeMaintenanceLogWaitingListDTO.OrderByDescending(a => a.StatusName);
        //             break;
        //     }
        // }

        var accommodationStatusTypeMaintenanceLogWaitingList_List = accommodationStatusTypeMaintenanceLogWaitingListDTO.ToList();

        // Retrieve the last MaintenanceLog and its MaintenanceLogDetails for AccommodationStatusId == 5
        foreach (var accommodationStatusTypeMaintenanceLogWaitingList in accommodationStatusTypeMaintenanceLogWaitingList_List)
        {
            var accommodationId = accommodationStatusTypeMaintenanceLogWaitingList.AccommodationId;
            if (accommodationStatusTypeMaintenanceLogWaitingList.AccommodationStatusId == 3)
            {
                var lastMaintenanceLog = maintenanceLogs
                    .Where(ml => ml.AccommodationId == accommodationId)
                    .OrderByDescending(ml => ml.LogDate)
                    .FirstOrDefault();

                if (lastMaintenanceLog != null)
                {
                    var lastMaintenanceLogDTO = new MaintenanceLogDTO
                    {
                        MaintenanceLogId = lastMaintenanceLog.Id,
                        UserId = lastMaintenanceLog.UserId,
                        LogDate = lastMaintenanceLog.LogDate,
                        BackInServiceDate = lastMaintenanceLog.BackInServiceDate,
                        Comment = lastMaintenanceLog.Comment,
                        ResolvedComment = lastMaintenanceLog.ResolvedComment,
                        NeedsCleaning = lastMaintenanceLog.NeedsCleaning,
                        ResolvedUserId = lastMaintenanceLog.ResolvedUserId,
                        MaintenanceLogDetails = maintenanceLogDetails
                        .Where(mld => mld.MaintenanceLogId == lastMaintenanceLog.Id)
                        .Select(mld => new MaintenanceLogDetailDTO
                        {
                            MaintenanceLogDetailId = mld.Id,
                            MaintenanceTaskId = mld.MaintenanceTaskId,
                            TaskName = maintenanceTasks.FirstOrDefault(mt => mt.Id == mld.MaintenanceTaskId)?.TaskName,
                            IsResolved = mld.IsResolved,
                            DateResolved = mld.DateResolved
                        }).ToList()
                    };

                    accommodationStatusTypeMaintenanceLogWaitingList.LastMaintenanceLog = lastMaintenanceLogDTO;
                }
            }

            if (accommodationStatusTypeMaintenanceLogWaitingList.AccommodationStatusId == 2)
            {

                var activeWaitingList = waitingLists
                    .Where(wl => wl.AccommodationId == accommodationId && wl.IsActive && wl.IsDeleted == false).FirstOrDefault();

                if (activeWaitingList != null)
                {
                    var activeWaitingListDTO = new WaitingListDTO
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
                    };

                    accommodationStatusTypeMaintenanceLogWaitingList.ActiveWaitingList = activeWaitingListDTO;
                }
            }
        }

        return accommodationStatusTypeMaintenanceLogWaitingList_List;
    }

    public async Task<IReadOnlyList<BookedAccommodationDTO>> GetAllBookedAccommodationsAsync(int organizationId)
    {
        // Get All booking details
        var bookingDetails = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        // Get all accommodations with AccommodationStatusId == 2 (Booked)
        var accommodationStatusTypeMaintenanceLogWaitingListDTO = await GetAccommodationsAsync(organizationId, null, 2);
        // Create a list to store the results
        var bookedAccommodations = new List<BookedAccommodationDTO>();

        // Iterate through each booked accommodation and retrieve its booking details
        foreach (var bookedAccommodationDTO in accommodationStatusTypeMaintenanceLogWaitingListDTO)
        {
            var accommodationId = bookedAccommodationDTO.AccommodationId;

            // Filter booking details for the current accommodation
            var bookingDetailForAccommodation = bookingDetails.FirstOrDefault(bd => bd.OrganizationId == organizationId && bd.AccommodationId == accommodationId && bd.IsDeleted == false) ?? throw new Exception("BookingDetail not found");

            var bookingId = bookingDetailForAccommodation.BookingId;

            var bookingLists = await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == bookingId && x.IsDeleted == false);
            var booking = bookingLists.FirstOrDefault() ?? throw new Exception("Booking not found for specified BookingDetail");

            // Create a BookingDetailDTO using bookingDetailForAccommodation
            var bookingDetailDTO = new BookingDetailDTO
            {
                BookingId = bookingDetailForAccommodation.BookingId,
                AccommodationId = bookingDetailForAccommodation.AccommodationId,
                AccommodationItemId = bookingDetailForAccommodation.AccommodationItemId,
                AccommodationRateId = bookingDetailForAccommodation.AccommodationRateId,
                BookingDetailStatusId = bookingDetailForAccommodation.BookingDetailStatusId,
                CouponId = bookingDetailForAccommodation.CouponId,
                PromopassId = bookingDetailForAccommodation.PromopassId,
                MembershipId = bookingDetailForAccommodation.MembershipId,
                HoursOfStay = bookingDetailForAccommodation.HoursOfStay,
                Qty = bookingDetailForAccommodation.Qty,
                Price = bookingDetailForAccommodation.Price,
                HasDeposit = bookingDetailForAccommodation.HasDeposit,
                Returned = bookingDetailForAccommodation.Returned,
                Transfered = bookingDetailForAccommodation.Transfered,
                AccommodationCouponDiscount = bookingDetailForAccommodation.AccommodationCouponDiscount,
                AccommodationPromopassDiscount = bookingDetailForAccommodation.AccommodationPromopassDiscount,
                TransferDepositDetail = bookingDetailForAccommodation.TransferDepositDetail,
                TransferOut = bookingDetailForAccommodation.TransferOut,
                TransferDepositsReturned = bookingDetailForAccommodation.TransferDepositsReturned,
                AccommodationOtherDiscount = bookingDetailForAccommodation.AccommodationOtherDiscount,
                AccommodationOtherDisPercent = bookingDetailForAccommodation.AccommodationOtherDisPercent,
                CheckOutTimeRequested = bookingDetailForAccommodation.CheckOutTimeRequested,
                ChargableDeposit = bookingDetailForAccommodation.ChargableDeposit,
                DepositTransRefund = bookingDetailForAccommodation.DepositTransRefund,
                IsKeyRefundedOnly = bookingDetailForAccommodation.IsKeyRefundedOnly,
            };

            // Create a BookedAccommodationDTO object to store the result
            var bookedAccommodation = new BookedAccommodationDTO
            {
                BookedAccommodationId = accommodationId,
                BookedAccommodation = bookedAccommodationDTO,
                // Assign the booking details to the Booking property
                Booking = new BookingDTO
                {
                    TransactionId = booking.TransactionId,
                    TimeCreated = booking.TimeCreated,
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
                    TotalDiscount = booking.TotalDiscount,
                    TaxRate = booking.TaxRate,
                    Comment = booking.Comment,
                    RateDetails = booking.RateDetails,
                    TotalTaxes = booking.TotalTaxes,
                    TimeCheckedOut = booking.TimeCheckedOut,
                    WakeUpCall = booking.WakeUpCall,
                    NonRefundedDeposits = booking.NonRefundedDeposits,
                    RefundComment = booking.RefundComment,
                    RateTotal = booking.RateTotal,
                    RateDiscount = booking.RateDiscount,
                    MemberDiscount = booking.MemberDiscount,
                    OtherDiscountDollar = booking.OtherDiscountDollar,
                    OtherDiscountReason = booking.OtherDiscountReason,
                    Rate = booking.Rate,
                    CouponsDiscount = booking.CouponsDiscount,
                    PromoDiscount = booking.PromoDiscount,
                    TimeCompleted = booking.TimeCompleted,
                    CheckedOutByEmployee = booking.CheckedOutByEmployee,
                    NewCreditCardNumber = booking.NewCreditCardNumber,
                    NewCardExpiryDate = booking.NewCardExpiryDate,
                    ShiftBookedIn = booking.ShiftBookedIn,
                    ShiftBookedOut = booking.ShiftBookedOut,
                    ReceiptMessage1 = booking.ReceiptMessage1,
                    ReceiptMessage2 = booking.ReceiptMessage2,
                    ReceiptCouponText = booking.ReceiptCouponText,
                    Disclaimer = booking.Disclaimer,
                    USDollars = booking.USDollars,
                    EuroDollars = booking.EuroDollars,
                    USExchangeRate = booking.USExchangeRate,
                    EuroExchangeRate = booking.EuroExchangeRate,
                    MembershipSales = booking.MembershipSales,
                    CheckInLoginId = booking.CheckInLoginId,
                    CheckOutLoginId = booking.CheckOutLoginId,
                    NetAmount = booking.NetAmount,
                    // Assign the BookingDetails list with a single item
                    BookingDetails = new List<BookingDetailDTO> { bookingDetailDTO }
                }
            };

            // Add the booked accommodation to the list
            bookedAccommodations.Add(bookedAccommodation);
        }

        return bookedAccommodations;
    }

    public async Task<BookedAccommodationDetailDTO> GetBookedAccommodationDetailAsync(int organizationId, int accommodationId)
    {
        var accommodationLists = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodationId && x.AccommodationStatusId == 2 && x.IsDeleted == false);
        var accommodation = accommodationLists.FirstOrDefault() ?? throw new Exception("Accommodation not found");
        var accommodationTypesLists = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodation.AccommodationTypeId && x.IsDeleted == false);
        var accommodationTypes = accommodationTypesLists.FirstOrDefault() ?? throw new Exception("Accommodation Type not found");
        var bookingDetailLists = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.AccommodationId == accommodation.Id && x.IsDeleted == false && x.BookingDetailStatusId == 5);
        var bookedAccommodationDetail = new BookedAccommodationDetailDTO();
        foreach (var bookingDetail in bookingDetailLists)
        {
            //var bookingDetail = bookingDetailLists.FirstOrDefault() ?? throw new Exception("Booking Detail not found");
            var bookingLists = await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == bookingDetail.BookingId && x.IsDeleted == false && x.TimeCheckedOut == null);
            var booking = bookingLists.FirstOrDefault();
            if (booking == null) continue;
            var transactionLists = await _unitOfWork.Repository<Transaction>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == booking.TransactionId && x.IsDeleted == false);
            var transaction = transactionLists.FirstOrDefault() ?? throw new Exception("Transaction not found");
            int hoursOfStay = (int)DateTime.Now.Subtract((DateTime)booking.CheckInTime!).TotalHours;



            var accommodationRates = await _unitOfWork.Repository<AccommodationRate>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.IsActive == true);
            DateTime currDate = DateTime.Now;
            var desiredAccommodationRate = accommodationRates.FirstOrDefault(ar =>
            {
                if (ar.AccommodationTypeId == accommodation.AccommodationTypeId)
                {
                    if (ar.IsStandardRate == false)
                    {
                        // For Non-Standard Accommodation Rate
                        return (
                            currDate.DayOfWeek == GetDayOfWeekFromString(ar.StartDayOfTheWeek) &&
                            currDate.DayOfWeek == GetDayOfWeekFromString(ar.EndDayOfWeek) &&
                            currDate.TimeOfDay >= ar.RateStartTime?.TimeOfDay &&
                            currDate.TimeOfDay <= ar.RateEndTime?.TimeOfDay
                        );
                    }
                    else if (ar.IsStandardRate == true)
                    {
                        // For Standard Accommodation Rate
                        if (currDate.DayOfWeek > GetDayOfWeekFromString(ar.StartDayOfTheWeek) && currDate.DayOfWeek < GetDayOfWeekFromString(ar.EndDayOfWeek))
                        {
                            return true;
                        }
                        else if (currDate.DayOfWeek == GetDayOfWeekFromString(ar.StartDayOfTheWeek))
                        {
                            if (currDate.TimeOfDay >= ar.RateStartTime?.TimeOfDay)
                            {
                                return true;
                            }
                        }
                        else if (currDate.DayOfWeek == GetDayOfWeekFromString(ar.EndDayOfWeek))
                        {
                            if (currDate.TimeOfDay <= ar.RateEndTime?.TimeOfDay)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return false; // Accommodation type doesn't match
            });



            if (desiredAccommodationRate == null) return null!;



            bookedAccommodationDetail = new BookedAccommodationDetailDTO
            {
                AccommodationNumber = accommodation.Number!,
                AccommodationType = accommodationTypes.TypeName!,
                HoursOfStay = hoursOfStay,
                RateHours = desiredAccommodationRate.Hours,
                CheckInTime = booking.CheckInTime.ToString("H:mm, dd MMM yyyy"),
                CheckOutTime = DateTime.Now.ToString("H:mm, dd MMM yyyy"),
                TotalDepositCollected = booking.TotalDeposits,
                DepositsTransferred = bookingDetail.TransferDepositDetail,
                DepositsReturned = bookingDetail.TransferDepositsReturned,
                DepositsBalance = booking.TotalDeposits - bookingDetail.TransferDepositDetail - bookingDetail.TransferDepositsReturned,
                ChangeDue = booking.TotalDeposits - (booking.TotalDeposits - bookingDetail.TransferDepositDetail - bookingDetail.TransferDepositsReturned),
                Comment = booking.Comment
            };
        }
        return bookedAccommodationDetail;
    }

    public async Task<IReadOnlyList<AccommodationTypeDTO>> GetAccommodationTypesAsync(int organizationId)
    {
        var accommodationTypes = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.OrganizationId == organizationId);
        var accommodationTypesToReturn = accommodationTypes.Select(accommodationType => new AccommodationTypeDTO
        {
            AccommodationTypeId = accommodationType.Id,
            TypeName = accommodationType.TypeName,
            MaxOccupants = accommodationType.MaxOccupants,
            CompatibilityValue = accommodationType.CompatibilityValue,
            isActive = (bool)accommodationType.IsDeleted! ? false : true
        }).ToList();
        return accommodationTypesToReturn;
    }

    public async Task<AccommodationTypeDTO> CreateAccommodationTypeAsync(AccommodationTypeDTO accommodationTypeDTO)
    {
        if (accommodationTypeDTO == null) throw new ArgumentNullException(nameof(accommodationTypeDTO));
        var accommodationType = new AccommodationType
        {
            TypeName = accommodationTypeDTO.TypeName,
            MaxOccupants = accommodationTypeDTO.MaxOccupants,
            CompatibilityValue = accommodationTypeDTO.CompatibilityValue,
            IsDeleted = accommodationTypeDTO.isActive ? false : true,
            OrganizationId = accommodationTypeDTO.OrganizationId,
            CreatedByUserId = accommodationTypeDTO.UserId,
            CreatedDate = DateTime.Now,
        };
        _unitOfWork.Repository<AccommodationType>().Add(accommodationType);
        var result = await _unitOfWork.Complete();
        if (result <= 0) return null!;
        return accommodationTypeDTO;
    }

    public async Task<bool> UpdateAccommodationTypeAsync(AccommodationTypeDTO accommodationTypeDTO)
    {
        var existingAccommodationTypes = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.OrganizationId == accommodationTypeDTO.OrganizationId && x.Id == accommodationTypeDTO.AccommodationTypeId);
        var existingAccommodationType = existingAccommodationTypes.FirstOrDefault();
        if (existingAccommodationType == null) return false;
        existingAccommodationType.TypeName = accommodationTypeDTO.TypeName;
        existingAccommodationType.MaxOccupants = accommodationTypeDTO.MaxOccupants;
        existingAccommodationType.CompatibilityValue = accommodationTypeDTO.CompatibilityValue;
        existingAccommodationType.IsDeleted = accommodationTypeDTO.isActive ? false : true;
        existingAccommodationType.EditedByUserId = accommodationTypeDTO.UserId;
        existingAccommodationType.EditedDate = DateTime.Now;
        _unitOfWork.Repository<AccommodationType>().Update(existingAccommodationType);
        var result = await _unitOfWork.Complete();
        if (result <= 0) return false;
        return true;
    }

    public async Task<IReadOnlyList<AccommodationStatusDTO>> GetAccommodationStatusesAsync(int organizationId)
    {
        var accommodationStatuses = await _unitOfWork.Repository<AccommodationStatus>().ListAllWhereAsync(x => x.OrganizationId == organizationId);
        var accommodationStatusesToReturn = accommodationStatuses.Select(accommodationStatus => new AccommodationStatusDTO
        {
            AccommodationStatusId = accommodationStatus.Id,
            StatusName = accommodationStatus.StatusName,
            isActive = (bool)accommodationStatus.IsDeleted! ? false : true
        }).ToList();
        return accommodationStatusesToReturn;
    }

    public async Task<AccommodationStatusDTO> CreateAccommodationStatusAsync(AccommodationStatusDTO accommodationStatusDTO)
    {
        if (accommodationStatusDTO == null) throw new ArgumentNullException(nameof(accommodationStatusDTO));
        var accommodationStatus = new AccommodationStatus
        {
            StatusName = accommodationStatusDTO.StatusName,
            IsDeleted = accommodationStatusDTO.isActive ? false : true,
            OrganizationId = accommodationStatusDTO.OrganizationId,
            CreatedByUserId = accommodationStatusDTO.UserId,
            CreatedDate = DateTime.Now,
        };
        _unitOfWork.Repository<AccommodationStatus>().Add(accommodationStatus);
        var result = await _unitOfWork.Complete();
        if (result <= 0) return null!;
        return accommodationStatusDTO;
    }

    public async Task<bool> UpdateAccommodationStatusAsync(AccommodationStatusDTO accommodationStatusDTO)
    {
        var existingAccommodationStatuses = await _unitOfWork.Repository<AccommodationStatus>().ListAllWhereAsync(x => x.OrganizationId == accommodationStatusDTO.OrganizationId && x.Id == accommodationStatusDTO.AccommodationStatusId);
        var existingAccommodationStatus = existingAccommodationStatuses.FirstOrDefault();
        if (existingAccommodationStatus == null) return false;
        existingAccommodationStatus.StatusName = accommodationStatusDTO.StatusName;
        existingAccommodationStatus.IsDeleted = accommodationStatusDTO.isActive ? false : true;
        existingAccommodationStatus.EditedByUserId = accommodationStatusDTO.UserId;
        existingAccommodationStatus.EditedDate = DateTime.Now;
        _unitOfWork.Repository<AccommodationStatus>().Update(existingAccommodationStatus);
        var result = await _unitOfWork.Complete();
        if (result <= 0) return false;
        return true;
    }

    public async Task<bool> EditAccommodationStatusAsync(string userId, int organizationId, int accommodationId, int newAccommodationStatusId)
    {
        var existingAccommodationList = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodationId && x.IsDeleted == false);
        var existingAccommodation = existingAccommodationList.FirstOrDefault();
        if (existingAccommodation == null) return false;

        existingAccommodation.AccommodationStatusId = newAccommodationStatusId;
        existingAccommodation.EditedByUserId = userId;
        existingAccommodation.EditedDate = DateTime.Now;

        _unitOfWork.Repository<Accommodation>().Update(existingAccommodation);
        var result = await _unitOfWork.Complete();

        if (result <= 0) return false!;

        return true;
    }

    public async Task<AccommodationStatusTypeMaintenanceLogWaitingListDTO> CreateAccommodationAsync(AccommodationCreateDTO accommodationCreateDTO)
    {
        if (accommodationCreateDTO == null) throw new ArgumentNullException(nameof(accommodationCreateDTO));

        var accommodation = new Accommodation
        {
            AccommodationTypeId = accommodationCreateDTO.AccommodationTypeId,
            AccommodationStatusId = accommodationCreateDTO.AccommodationStatusId,
            Number = accommodationCreateDTO.Number,
            Floor = accommodationCreateDTO.Floor,
            DeActivate = accommodationCreateDTO.DeActivate,
            RequiresCreditCard = accommodationCreateDTO.RequiresCreditCard,
            HasMaintenanceReservation = accommodationCreateDTO.HasMaintenanceReservation,
            EmployeeLocker = accommodationCreateDTO.EmployeeLocker,
            IsGhost = accommodationCreateDTO.IsGhost,
            HasGloryHole = accommodationCreateDTO.HasGloryHole,
            OrganizationId = accommodationCreateDTO.OrganizationId,
            CreatedByUserId = accommodationCreateDTO.UserId,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        _unitOfWork.Repository<Accommodation>().Add(accommodation);

        var result = await _unitOfWork.Complete();

        if (result <= 0) return null!;

        var accommodationTypeStatusDTO = new AccommodationStatusTypeMaintenanceLogWaitingListDTO
        {
            AccommodationId = accommodation.Id,
            AccommodationTypeId = accommodation.AccommodationTypeId,
            TypeName = null!,
            AccommodationStatusId = accommodation.AccommodationStatusId,
            StatusName = null!,
            Number = accommodation.Number,
            Floor = accommodation.Floor,
            DeActivate = accommodation.DeActivate,
            RequiresCreditCard = accommodation.RequiresCreditCard,
            HasMaintenanceReservation = accommodation.HasMaintenanceReservation,
            EmployeeLocker = accommodation.EmployeeLocker,
            IsGhost = accommodation.IsGhost,
            HasGloryHole = accommodation.HasGloryHole,
            OrganizationId = accommodation.OrganizationId
        };

        var accommodationType = await _unitOfWork.Repository<AccommodationType>().ListAllWhereAsync(x => x.Id == accommodationTypeStatusDTO.AccommodationTypeId && x.OrganizationId == accommodationCreateDTO.OrganizationId && x.IsDeleted == false);
        accommodationTypeStatusDTO.TypeName = accommodationType.First().TypeName!;

        if (accommodationTypeStatusDTO.AccommodationStatusId.HasValue)
        {
            int accommodationStatusId = accommodationTypeStatusDTO.AccommodationStatusId.Value;
            var accommodationStatus = await _unitOfWork.Repository<AccommodationStatus>().ListAllWhereAsync(x => x.Id == accommodationStatusId && x.OrganizationId == accommodationCreateDTO.OrganizationId && x.IsDeleted == false);
            accommodationTypeStatusDTO.StatusName = accommodationStatus.First().StatusName!;
        }

        return accommodationTypeStatusDTO;
    }

    public async Task<bool> EditAccommodationAsync(AccommodationDTO accommodationDTO)
    {
        var existingAccommodationLists = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.OrganizationId == accommodationDTO.OrganizationId && x.Id == accommodationDTO.AccommodationId && x.IsDeleted == false);
        var existingAccommodation = existingAccommodationLists.FirstOrDefault();
        if (existingAccommodation == null) return false;

        existingAccommodation.AccommodationTypeId = accommodationDTO.AccommodationTypeId;
        existingAccommodation.AccommodationStatusId = accommodationDTO.AccommodationStatusId;
        existingAccommodation.Number = accommodationDTO.Number;
        existingAccommodation.Floor = accommodationDTO.Floor;
        existingAccommodation.DeActivate = accommodationDTO.DeActivate;
        existingAccommodation.RequiresCreditCard = accommodationDTO.RequiresCreditCard;
        existingAccommodation.HasMaintenanceReservation = accommodationDTO.HasMaintenanceReservation;
        existingAccommodation.EmployeeLocker = accommodationDTO.EmployeeLocker;
        existingAccommodation.IsGhost = accommodationDTO.IsGhost;
        existingAccommodation.DeActivate = accommodationDTO.DeActivate;
        existingAccommodation.HasGloryHole = accommodationDTO.HasGloryHole;
        existingAccommodation.EditedByUserId = accommodationDTO.UserId;
        existingAccommodation.EditedDate = DateTime.Now;

        _unitOfWork.Repository<Accommodation>().Update(existingAccommodation);
        var result = await _unitOfWork.Complete();
        if (result <= 0) return false!;

        return true;
    }

    public async Task<bool> DeleteAccommodationAsync(string userId, int organizationId, int accommodationId)
    {
        var existingAccommodationLists = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodationId && x.IsDeleted == false && x.DeActivate == false);
        var existingAccommodation = existingAccommodationLists.FirstOrDefault() ?? throw new ArgumentException("Accommodation not found");
        existingAccommodation.IsDeleted = true;
        existingAccommodation.DeActivate = true;
        existingAccommodation.DeletedByUserId = userId;
        existingAccommodation.DeletedDate = DateTime.Now;

        _unitOfWork.Repository<Accommodation>().Update(existingAccommodation);
        //_unitOfWork.Repository<Accommodation>().Delete(existingAccommodation);            // Use for delete Accommodation from table.
        var result = await _unitOfWork.Complete();

        return result > 0;
    }

    public async Task<IReadOnlyList<BookedAccommodationDTO>> GetOverDueAccommodationsAsync(int organizationId)
    {
        // Get All booking details
        var bookingDetails = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        // Get all accommodations with AccommodationStatusId == 2 (Booked)
        var accommodationStatusTypeMaintenanceLogWaitingListDTO = await GetAccommodationsAsync(organizationId, null, 2);
        // Create a list to store the results
        var overDueBookedAccommodations = new List<BookedAccommodationDTO>();

        // Iterate through each booked accommodation and retrieve its booking details
        foreach (var bookedAccommodationDTO in accommodationStatusTypeMaintenanceLogWaitingListDTO)
        {
            var accommodationId = bookedAccommodationDTO.AccommodationId;

            // Filter booking details for the current accommodation
            var bookingDetailForAccommodation = bookingDetails.FirstOrDefault(bd => bd.OrganizationId == organizationId && bd.AccommodationId == accommodationId && bd.IsDeleted == false && bd.BookingDetailStatusId == 5);
            if (bookingDetailForAccommodation == null) return overDueBookedAccommodations;

            var bookingId = bookingDetailForAccommodation.BookingId;

            var bookingLists = await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == bookingId && x.IsDeleted == false && x.TimeCheckedOut == null);
            var booking = bookingLists.FirstOrDefault();
            if (booking == null) continue;
            double totalStayedHour = DateTime.Now.Subtract((DateTime)booking.CheckInTime!).TotalHours;
            bool isOverDue = totalStayedHour > bookingDetailForAccommodation.HoursOfStay ? true : false;
            if (isOverDue)
            {
                // Create a BookingDetailDTO using bookingDetailForAccommodation
                var bookingDetailDTO = new BookingDetailDTO
                {
                    BookingId = bookingDetailForAccommodation.BookingId,
                    AccommodationId = bookingDetailForAccommodation.AccommodationId,
                    AccommodationItemId = bookingDetailForAccommodation.AccommodationItemId,
                    AccommodationRateId = bookingDetailForAccommodation.AccommodationRateId,
                    BookingDetailStatusId = bookingDetailForAccommodation.BookingDetailStatusId,
                    CouponId = bookingDetailForAccommodation.CouponId,
                    PromopassId = bookingDetailForAccommodation.PromopassId,
                    MembershipId = bookingDetailForAccommodation.MembershipId,
                    HoursOfStay = bookingDetailForAccommodation.HoursOfStay,
                    Qty = bookingDetailForAccommodation.Qty,
                    Price = bookingDetailForAccommodation.Price,
                    HasDeposit = bookingDetailForAccommodation.HasDeposit,
                    Returned = bookingDetailForAccommodation.Returned,
                    Transfered = bookingDetailForAccommodation.Transfered,
                    AccommodationCouponDiscount = bookingDetailForAccommodation.AccommodationCouponDiscount,
                    AccommodationPromopassDiscount = bookingDetailForAccommodation.AccommodationPromopassDiscount,
                    TransferDepositDetail = bookingDetailForAccommodation.TransferDepositDetail,
                    TransferOut = bookingDetailForAccommodation.TransferOut,
                    TransferDepositsReturned = bookingDetailForAccommodation.TransferDepositsReturned,
                    AccommodationOtherDiscount = bookingDetailForAccommodation.AccommodationOtherDiscount,
                    AccommodationOtherDisPercent = bookingDetailForAccommodation.AccommodationOtherDisPercent,
                    CheckOutTimeRequested = bookingDetailForAccommodation.CheckOutTimeRequested,
                    ChargableDeposit = bookingDetailForAccommodation.ChargableDeposit,
                    DepositTransRefund = bookingDetailForAccommodation.DepositTransRefund,
                    IsKeyRefundedOnly = bookingDetailForAccommodation.IsKeyRefundedOnly,
                };

                // Create a BookedAccommodationDTO object to store the result
                var bookedAccommodation = new BookedAccommodationDTO
                {
                    BookedAccommodationId = accommodationId,
                    BookedAccommodation = bookedAccommodationDTO,
                    // Assign the booking details to the Booking property
                    Booking = new BookingDTO
                    {
                        TransactionId = booking.TransactionId,
                        TimeCreated = booking.TimeCreated,
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
                        TotalDiscount = booking.TotalDiscount,
                        TaxRate = booking.TaxRate,
                        Comment = booking.Comment,
                        RateDetails = booking.RateDetails,
                        TotalTaxes = booking.TotalTaxes,
                        TimeCheckedOut = booking.TimeCheckedOut,
                        WakeUpCall = booking.WakeUpCall,
                        NonRefundedDeposits = booking.NonRefundedDeposits,
                        RefundComment = booking.RefundComment,
                        RateTotal = booking.RateTotal,
                        RateDiscount = booking.RateDiscount,
                        MemberDiscount = booking.MemberDiscount,
                        OtherDiscountDollar = booking.OtherDiscountDollar,
                        OtherDiscountReason = booking.OtherDiscountReason,
                        Rate = booking.Rate,
                        CouponsDiscount = booking.CouponsDiscount,
                        PromoDiscount = booking.PromoDiscount,
                        TimeCompleted = booking.TimeCompleted,
                        CheckedOutByEmployee = booking.CheckedOutByEmployee,
                        NewCreditCardNumber = booking.NewCreditCardNumber,
                        NewCardExpiryDate = booking.NewCardExpiryDate,
                        ShiftBookedIn = booking.ShiftBookedIn,
                        ShiftBookedOut = booking.ShiftBookedOut,
                        ReceiptMessage1 = booking.ReceiptMessage1,
                        ReceiptMessage2 = booking.ReceiptMessage2,
                        ReceiptCouponText = booking.ReceiptCouponText,
                        Disclaimer = booking.Disclaimer,
                        USDollars = booking.USDollars,
                        EuroDollars = booking.EuroDollars,
                        USExchangeRate = booking.USExchangeRate,
                        EuroExchangeRate = booking.EuroExchangeRate,
                        MembershipSales = booking.MembershipSales,
                        CheckInLoginId = booking.CheckInLoginId,
                        CheckOutLoginId = booking.CheckOutLoginId,
                        NetAmount = booking.NetAmount,
                        // Assign the BookingDetails list with a single item
                        BookingDetails = new List<BookingDetailDTO> { bookingDetailDTO }
                    }
                };

                // Add the booked accommodation to the list
                overDueBookedAccommodations.Add(bookedAccommodation);
            }
        }
        return overDueBookedAccommodations;
    }

    private bool IsWednesdayBetween5pmAnd12pm()
    {
        var now = DateTime.Now;

        if (now.DayOfWeek == DayOfWeek.Wednesday)
        {
            var startTime = new TimeSpan(17, 0, 0); // 5:00 PM
            var endTime = new TimeSpan(0, 0, 0);   // 12:00 AM (midnight)

            // Adjust the endTime to represent 12:00 AM of the next day
            if (now.TimeOfDay.Hours < 5)
            {
                endTime = new TimeSpan(24, 0, 0); // 12:00 AM of the next day
            }

            var currentTime = now.TimeOfDay;

            return currentTime >= startTime && currentTime < endTime;
        }

        return false;
    }
}