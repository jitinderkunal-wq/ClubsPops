using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Common;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Infrastructure.IRepositories;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<ShiftDTO>> GetShiftsAsync(int organizationId)
        {
            var shifts = await _unitOfWork.Repository<ShiftSetUp>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsActive == true && x.IsDeleted == false);
            var shiftDTOs = shifts.Select(shift => new ShiftDTO
            {
                ShiftId = shift.Id,
                ShiftNumber = shift.ShiftNumber,
                ShiftStart = shift.ShiftStart.ToString(),
                ShiftEnd = shift.ShiftEnd.ToString(),
                IsActive = shift.IsActive
            }).ToList();
            return shiftDTOs;
        }
        public async Task<IReadOnlyList<ShiftSessionDTO>> GetActiveSessionAsync(int organizationId, string userId)
        {
            var salesSessionsList = await _unitOfWork.Repository<SalesSession>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.SessionEndDate == null && x.AppUserId == userId);
            //if(salesSessionsList.Count <= 0) throw new Exception("No active session found for current logged in user");
            var shiftSessionDTO = salesSessionsList.Select(x => new ShiftSessionDTO
            {
                ShiftSessionId = x.Id,
                UserId = x.AppUserId,
                SessionDateCreated = x.SessionStartDate,
                SessionEndDate = x.SessionEndDate,
                ShiftNumber = x.ShiftNumber,
                BeginFloat = x.BeginFloat,
                DrawerNumber = x.DrawerNumber,
                BusinessDay = x.BusinessDay,
                IsCurrentlyInUse = x.IsCurrentlyInUse,
            }).ToList();
            return shiftSessionDTO;
        }

        public async Task<ShiftSessionDTO> StartNewSessionAsync(ShiftSessionDTO shiftSessionDTO) 
        {
            var salesSessionsList = await _unitOfWork.Repository<SalesSession>().ListAllWhereAsync(x => x.OrganizationId == shiftSessionDTO.OrganizationId && x.IsDeleted == false && x.AppUserId == shiftSessionDTO.UserId && x.IsCurrentlyInUse == true);
            //var activeSalesSession = salesSessionsList.FirstOrDefault();
            foreach (var activeSalesSession in salesSessionsList)
            {
                if (activeSalesSession == null) continue;
                activeSalesSession.IsCurrentlyInUse = false;
                // Update Sales Session and set IsCurrentlyInUse = false in the database
                _unitOfWork.Repository<SalesSession>().Update(activeSalesSession);
            }
            var salesSession = new SalesSession
            {
                AppUserId = shiftSessionDTO.UserId,
                SessionStartDate = DateTime.Now,
                ShiftNumber = shiftSessionDTO.ShiftNumber,
                BeginFloat = 0,
                DrawerNumber = shiftSessionDTO.DrawerNumber,
                BusinessDay = DateTime.Now,
                IsCurrentlyInUse = true,
                OrganizationId = shiftSessionDTO.OrganizationId,
                CreatedByUserId = shiftSessionDTO.UserId,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            // Saving New Sales Session in the database
            _unitOfWork.Repository<SalesSession>().Add(salesSession);
            var result = await _unitOfWork.Complete();
            if (result <= 0) throw new Exception("Failed to create new Session");
            return shiftSessionDTO;
        }

        public async Task<bool> StartExistingSessionAsync(string userId, int organizationId, int shiftSessionId) 
        {
            var salesSessionsList = await _unitOfWork.Repository<SalesSession>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.AppUserId == userId);
            if (salesSessionsList.Count > 0)
            {
                foreach (var activeSalesSession in salesSessionsList)
                {
                    if (activeSalesSession.IsCurrentlyInUse == true) activeSalesSession.IsCurrentlyInUse = false;
                    activeSalesSession.EditedByUserId = activeSalesSession.AppUserId;
                    activeSalesSession.EditedDate = DateTime.Now;
                    // Update Sales Session and set IsCurrentlyInUse = false where IsCurrentlyInUse = true
                    _unitOfWork.Repository<SalesSession>().Update(activeSalesSession);
                }
            }
            // Get all active session where IsCurrentlyInUse is true related from that user
            var salesSession = await _unitOfWork.Repository<SalesSession>().GetByIdAsync(shiftSessionId) ?? throw new Exception("Sales session not found");
            if (salesSession.IsCurrentlyInUse == false) salesSession.IsCurrentlyInUse = true;
            salesSession.EditedByUserId = salesSession.AppUserId;
            salesSession.EditedDate = DateTime.Now;
            // Update Sales Session and set IsCurrentlyInUse = false where IsCurrentlyInUse = true
            _unitOfWork.Repository<SalesSession>().Update(salesSession);
            var result = await _unitOfWork.Complete();
            if (result <= 0) throw new Exception("Failed to update existing session");
            return true;
        }

        public async Task<bool> SetIsCurrentlyInUseFalseAsync(string userId, int organizationId) 
        {
            var salesSessions = await _unitOfWork.Repository<SalesSession>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.AppUserId == userId && x.IsDeleted == false && x.IsCurrentlyInUse == true);
            var salesSession = salesSessions.FirstOrDefault();
            if(salesSession == null) return false;
            salesSession.IsCurrentlyInUse = false;
            // Update Sales Session and set IsCurrentlyInUse false.
            _unitOfWork.Repository<SalesSession>().Update(salesSession);
            var result = await _unitOfWork.Complete();
            if (result <= 0) throw new Exception("Failed to set false IsCurrentlyInUse");
            return true;
        }

        public async Task<DrawerSkimDTO> EndSessionAsync(DrawerSkimDTO drawerSkimDTO)
        {
            var salesSessions = await _unitOfWork.Repository<SalesSession>().ListAllWhereAsync(x => x.AppUserId == drawerSkimDTO.UserId && x.OrganizationId == drawerSkimDTO.OrganizationId && x.IsDeleted == false && x.SessionEndDate == null);
            var activeSalesSession = salesSessions.SingleOrDefault(x=> x.Id == drawerSkimDTO.SalesSessionId) ?? throw new Exception("No active session found for current logged in user.");
            activeSalesSession.SessionEndDate = DateTime.Now;
            activeSalesSession.BeginFloat = drawerSkimDTO.GrandTotal;
            activeSalesSession.IsCurrentlyInUse = false;
            activeSalesSession.EditedByUserId = drawerSkimDTO.UserId;
            activeSalesSession.EditedDate = DateTime.Now;

            #region DrawerSkim
            var drawerSkim = new DrawerSkim
            {
                SalesSessionId = activeSalesSession.Id,
                SkimDate = DateTime.Now,
                TotalCoin = drawerSkimDTO.Qty1Dollar + drawerSkimDTO.Qty2Dollar + drawerSkimDTO.Qty5Dollar + drawerSkimDTO.Qty10Dollar + drawerSkimDTO.Qty20Dollar + drawerSkimDTO.Qty50Dollar + drawerSkimDTO.Qty100Dollar,
                Qty1Dollar = drawerSkimDTO.Qty1Dollar,
                Qty2Dollar = drawerSkimDTO.Qty2Dollar,
                Qty5Dollar = drawerSkimDTO.Qty5Dollar,
                Qty10Dollar = drawerSkimDTO.Qty10Dollar,
                Qty20Dollar = drawerSkimDTO.Qty20Dollar,
                Qty50Dollar = drawerSkimDTO.Qty50Dollar,
                Qty100Dollar = drawerSkimDTO.Qty100Dollar,
                Total1Dollar = drawerSkimDTO.Qty1Dollar * 1,
                Total2Dollar = drawerSkimDTO.Qty2Dollar * 2,
                Total5Dollar = drawerSkimDTO.Qty5Dollar * 5,
                Total10Dollar = drawerSkimDTO.Qty10Dollar * 10,
                Total20Dollar = drawerSkimDTO.Qty20Dollar * 20,
                Total50Dollar = drawerSkimDTO.Qty50Dollar * 50,
                Total100Dollar = drawerSkimDTO.Qty100Dollar * 100,
                GrandTotal = drawerSkimDTO.GrandTotal,
                OrganizationId = drawerSkimDTO.OrganizationId,
                CreatedByUserId = drawerSkimDTO.UserId,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };
            #endregion
            #region Cashier
            var cashierReconciliation = new CashierReconciliation
            {
                SalesSessionId = activeSalesSession.Id,
                EosNickels = drawerSkimDTO.Nickels,
                EosDimes = drawerSkimDTO.Dimes,
                EosQuarters = drawerSkimDTO.Quarters,
                EosLoonies = drawerSkimDTO.Qty1Dollar,
                EosTwonies = drawerSkimDTO.Qty2Dollar,
                EosFives = drawerSkimDTO.Qty5Dollar,
                EosTens = drawerSkimDTO.Qty10Dollar,
                EosTwentys = drawerSkimDTO.Qty20Dollar,
                EosFiftys = drawerSkimDTO.Qty50Dollar,
                EosOneHundreds = drawerSkimDTO.Qty100Dollar,
                EosDepositTotal = drawerSkimDTO.CashDepositTotal,
                EosDebit = drawerSkimDTO.Debit,
                EosVisa = drawerSkimDTO.Visa,
                EosMasterCard = drawerSkimDTO.MasterCard,
                EosAmex = drawerSkimDTO.Amex,
                EosDiscover = drawerSkimDTO.Discover,
                EosOtherCreditCard1 = drawerSkimDTO.OtherCreditCard,
                EosUsDollars = drawerSkimDTO.UsDollars,
                EosEuroDollars = drawerSkimDTO.EuroDollars,
                EosQuickDebitCredit = drawerSkimDTO.DebitCreditTotal,
                EosFinalTotal = drawerSkimDTO.GrandTotal,
                SalesStartTime = activeSalesSession.SessionStartDate,
                SalesEndTime = activeSalesSession.SessionEndDate,
                OrganizationId = drawerSkimDTO.OrganizationId,
                CreatedByUserId = drawerSkimDTO.UserId,
                CreatedDate = DateTime.Now,
                DateCreated = DateTime.Now,
                IsDeleted = false
            };
            
            #endregion
            //Add Drawer Skim in the database
            _unitOfWork.Repository<DrawerSkim>().Add(drawerSkim);
            // Updating currently Sale Session in the database
            _unitOfWork.Repository<SalesSession>().Update(activeSalesSession);
            // Add Cash Reconcilation in the database
            _unitOfWork.Repository<CashierReconciliation>().Add(cashierReconciliation);
            var result = await _unitOfWork.Complete();
            if (result <= 0) throw new Exception("Failed to update Cashier Reconciliation");
            return drawerSkimDTO;
        }

        public async Task<bool> EndPreviousDayShiftAutomatically()  
        {
            var salesSessionsList = await _unitOfWork.Repository<SalesSession>().ListAllWhereAsync(x => x.IsDeleted == false && x.SessionEndDate == null);
            if (salesSessionsList.Count <= 0) throw new Exception("No active session found for current logged in user");
            foreach (var item in salesSessionsList)
            {
                item.IsCurrentlyInUse = false;
                item.SessionEndDate = DateTime.Now;
                item.EditedDate = DateTime.Now;
                _unitOfWork.Repository<SalesSession>().Update(item);
                var result = await _unitOfWork.Complete();
                if (result <= 0) throw new Exception("Failed to End previous day shift");
            }
            return true;
        }

        public async Task<ResponseStatus> CreateCashDropDrawerSkim(CashDropDrawerSkimDTO cashDropDrawerSkimDTO)
        {
            if (cashDropDrawerSkimDTO == null)
            {
                return new ResponseStatus
                {
                    Status = ResponseEnum.Error,
                    Message = "Invalid payload"
                };
            }

            var drawerSkim = new DrawerSkim
            {
                SalesSessionId = cashDropDrawerSkimDTO.SalesSessionId,
                OrganizationId = cashDropDrawerSkimDTO.OrganizationId,
                SkimDate = DateTime.Now,
                SkimShiftCount = cashDropDrawerSkimDTO.SkimShiftCount,
                TotalCoin = cashDropDrawerSkimDTO.TotalCoin,
                Qty1Dollar = cashDropDrawerSkimDTO.Qty1Dollar,
                Qty2Dollar = cashDropDrawerSkimDTO.Qty2Dollar,
                Qty5Dollar = cashDropDrawerSkimDTO.Qty5Dollar,
                Qty10Dollar = cashDropDrawerSkimDTO.Qty10Dollar,
                Qty20Dollar = cashDropDrawerSkimDTO.Qty20Dollar,
                Qty50Dollar = cashDropDrawerSkimDTO.Qty50Dollar,
                Qty100Dollar = cashDropDrawerSkimDTO.Qty100Dollar,
                TotalCheques = cashDropDrawerSkimDTO.TotalCheques,
                TotalOther = cashDropDrawerSkimDTO.TotalOther,
                TotalEuros = cashDropDrawerSkimDTO.TotalEuros,
                Qty1DollarUs = cashDropDrawerSkimDTO.Qty1DollarUs,
                Qty2DollarUs = cashDropDrawerSkimDTO.Qty2DollarUs,
                Qty5DollarUs = cashDropDrawerSkimDTO.Qty5DollarUs,
                Qty10DollarUs = cashDropDrawerSkimDTO.Qty10DollarUs,
                Qty20DollarUs = cashDropDrawerSkimDTO.Qty20DollarUs,
                Qty50DollarUs = cashDropDrawerSkimDTO.Qty50DollarUs,
                Qty100DollarUs = cashDropDrawerSkimDTO.Qty100DollarUs,
                CoinTotal = cashDropDrawerSkimDTO.CoinTotal,
                Total1Dollar = cashDropDrawerSkimDTO.Total1Dollar,
                Total2Dollar = cashDropDrawerSkimDTO.Total2Dollar,
                Total5Dollar = cashDropDrawerSkimDTO.Total5Dollar,
                Total10Dollar = cashDropDrawerSkimDTO.Total10Dollar,
                Total20Dollar = cashDropDrawerSkimDTO.Total20Dollar,
                Total50Dollar = cashDropDrawerSkimDTO.Total50Dollar,
                Total100Dollar = cashDropDrawerSkimDTO.Total100Dollar,
                Total1DollarUs = cashDropDrawerSkimDTO.Total1DollarUs,
                Total2DollarUs = cashDropDrawerSkimDTO.Total2DollarUs,
                Total5DollarUs = cashDropDrawerSkimDTO.Total5DollarUs,
                Total10DollarUs = cashDropDrawerSkimDTO.Total10DollarUs,
                Total20DollarUs = cashDropDrawerSkimDTO.Total20DollarUs,
                Total50DollarUs = cashDropDrawerSkimDTO.Total50DollarUs,
                Total100DollarUs = cashDropDrawerSkimDTO.Total100DollarUs,
                ChequeTotal = cashDropDrawerSkimDTO.ChequeTotal,
                OtherTotal = cashDropDrawerSkimDTO.OtherTotal,
                EuroTotal = cashDropDrawerSkimDTO.EuroTotal,
                TotalUsdollars = cashDropDrawerSkimDTO.TotalUsdollars,
                TotalCaddollar = cashDropDrawerSkimDTO.TotalCaddollar,
                GrandTotal = cashDropDrawerSkimDTO.GrandTotal,
                CreatedByUserId = cashDropDrawerSkimDTO.UserId,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            _unitOfWork.Repository<DrawerSkim>().Add(drawerSkim);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                return new ResponseStatus
                {
                    Status = ResponseEnum.Failed,
                    Message = "Failed to create cash drop drawer skim"
                };
            }
            return new ResponseStatus
            {
                Status = ResponseEnum.Success,
                Message = "Cash drop drawer skim created successfully."
            };
        }
    }
}
