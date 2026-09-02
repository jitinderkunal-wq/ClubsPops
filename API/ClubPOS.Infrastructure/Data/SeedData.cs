using ClubPOS.Core.Entities;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ClubPOS.Infrastructure.Data;

public class SeedData
{
    public static async Task Seedasync(DataContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            //var currDirectory = Path.Combine(Directory.GetCurrentDirectory());
            //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!context.AccommodationItems?.Any() ?? false)
            {
                var accommodationItems = new List<AccommodationItem>
                {
                    new AccommodationItem { OrganizationId = 1, IsDeleted = false, ItemName = "Key + Towel", Price = 0.00m, DepositAmount = 0m, IsActive = true },
                    new AccommodationItem { OrganizationId = 1, IsDeleted = false, ItemName = "Extra Towel", Price = 0.00m, DepositAmount = 0m, IsActive = true },
                    //new AccommodationItem { OrganizationId = 1, IsDeleted = false, ItemName = "Robe", Price = 0.00m, DepositAmount = 10.00m, IsActive = true },
                    new AccommodationItem { OrganizationId = 1, IsDeleted = false, ItemName = "Remote", Price = 0.00m, DepositAmount = 0.00m, IsActive = true }
                    //new AccommodationItem {OrganizationId = 1, IsDeleted = false, ItemName = "Heater", Price = 0.00m, DepositAmount = 10.00m, IsActive = true },
                    //new AccommodationItem {OrganizationId = 1, IsDeleted = false, ItemName = "Blanket", Price = 0.00m, DepositAmount = 10.00m, IsActive = true }
                };
                await context.AccommodationItems!.AddRangeAsync(accommodationItems);
                await context.SaveChangesAsync();
            }

            if (!context.AccommodationStatuses?.Any() ?? false)
            {
                var accommodationStatuses = new List<AccommodationStatus>
                {
                    new AccommodationStatus { OrganizationId = 1, IsDeleted = false, StatusName = "Reserved" },
                    new AccommodationStatus { OrganizationId = 1, IsDeleted = false, StatusName = "In-Use" },
                    new AccommodationStatus { OrganizationId = 1, IsDeleted = false, StatusName = "Needs Maintenance" },
                    new AccommodationStatus { OrganizationId = 1, IsDeleted = false, StatusName = "Needs Cleaning" },
                    new AccommodationStatus { OrganizationId = 1, IsDeleted = false, StatusName = "Available" },
                    new AccommodationStatus { OrganizationId = 1, IsDeleted = false, StatusName = "Not Available" },
                    new AccommodationStatus { OrganizationId = 1, IsDeleted = false, StatusName = "Employee" },
                    new AccommodationStatus { OrganizationId = 1, IsDeleted = false, StatusName = "WaitList Request" }
                };
                await context.AccommodationStatuses!.AddRangeAsync(accommodationStatuses);
                await context.SaveChangesAsync();
            }

            if (!context.AccommodationTypes?.Any() ?? false)
            {
                var accommodationTypes = new List<AccommodationType>
            {
                new AccommodationType { OrganizationId = 1, IsDeleted = false, TypeName = "Locker", MaxOccupants = 1, CompatibilityValue = 1 },
                new AccommodationType { OrganizationId = 1, IsDeleted = false, TypeName = "Single Room", MaxOccupants = 1, CompatibilityValue = 2 },
                new AccommodationType { OrganizationId = 1, IsDeleted = false, TypeName = "Single Room + TV", MaxOccupants = 1, CompatibilityValue = 3 },
                new AccommodationType { OrganizationId = 1, IsDeleted = false, TypeName = "Double Room + TV", MaxOccupants = 2, CompatibilityValue = 5 }
            };
                await context.AccommodationTypes!.AddRangeAsync(accommodationTypes);
                await context.SaveChangesAsync();
            }

            if (!context.Accommodations?.Any() ?? false)
            {
                AccommodationType lockerType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Locker")).Select(at => at).First();
                AccommodationType singleRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Single Room")).Select(at => at).First();
                AccommodationType singleTVRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Single Room + TV")).Select(at => at).First();
                AccommodationType doubleTVRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Double Room + TV")).Select(at => at).First();
                AccommodationStatus availableStatus = context.AccommodationStatuses!.Where(s => s.StatusName!.Equals("Available")).First();
                AccommodationStatus employeeStatus = context.AccommodationStatuses!.Where(s => s.StatusName!.Equals("Employee")).First();

                var accommodations = new List<Accommodation>
                {
                    // Not Commented
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "A", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "B", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "C", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "D", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "E", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "F", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "G", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "H", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "I", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "J", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "K", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "M", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "N", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "O", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "P", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "Q", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "R", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "S", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "T", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "U", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "V", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "W", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "X", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "Y", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "Z", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L1", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L2", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L3", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L4", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L5", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L6", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L7", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L8", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L9", Floor = 2 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "L10", Floor = 2 },

                    // // Commented
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "37", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "38", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "39", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "40", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "41", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "42", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "43", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "44", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "45", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "46", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "47", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "48", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "49", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "50", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "51", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "52", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "53", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "54", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "55", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "56", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "57", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "58", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "59", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "60", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "61", Floor = 2 },                                     // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "62", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "63", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "64", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "65", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "66", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "67", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "68", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "69", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "70", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "71", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "72", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "73", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "74", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "75", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "76", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "77", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "78", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "79", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "80", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "81", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "82", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "83", Floor = 2 },                                     // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "84", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "85", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "86", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "87", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "88", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "89", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "90", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "91", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "92", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "93", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "94", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "95", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "96", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "97", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "98", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "99", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "100", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "101", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "102", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "103", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "104", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "105", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "106", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "107", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "108", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "109", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "110", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "111", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "112", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "113", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "114", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "115", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "116", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "117", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "118", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "119", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "120", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "121", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "122", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "123", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "124", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "125", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "126", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "127", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "128", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "129", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "130", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "131", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "132", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "133", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "134", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "135", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "136", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "137", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "138", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "139", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "140", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "141", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "142", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "143", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "144", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "145", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "146", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "147", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "148", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "149", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "150", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "151", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "152", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "153", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "154", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "155", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "156", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "157", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "158", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "159", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "160", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "161", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "162", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "601", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "602", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "603", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "604", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "605", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "606", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "607", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "608", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "609", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "610", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "611", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "612", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "613", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "614", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "615", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "616", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "617", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "618", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "619", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "620", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "621", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "622", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "623", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "624", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "625", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "626", Floor = 0 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "627", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "628", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "629", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = employeeStatus.Id, Number = "630", Floor = 0 },                                    // employeeStatus
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "800", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "801", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "802", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "803", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "804", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "805", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "806", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "807", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "808", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "809", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "810", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "811", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "812", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "813", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "814", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "815", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "816", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "817", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "818", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "819", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "820", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "821", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "822", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "823", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "824", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "825", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "826", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "827", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "828", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "829", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "830", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "831", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "832", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "833", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "834", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "835", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "836", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "837", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "838", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "839", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "840", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "841", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "842", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "843", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "844", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "845", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "846", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "847", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "848", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "849", Floor = 0, IsGhost = true },                   // IsGhost = true
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, AccommodationStatusId = availableStatus.Id, Number = "850", Floor = 0, IsGhost = true },                   // IsGhost = true

                    // Not commented
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "1", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "2", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "3", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "4", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "5", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "6", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "7", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "8", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "9", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "10", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "11", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "12", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "13", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "14", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "15", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "16", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "17", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "18", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "19", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "20", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "21", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "22", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "23", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "24", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "25", Floor = 1 },
                    new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "26", Floor = 1 },

                    // // Commented
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "221", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "222", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "223", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "224", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "225", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "226", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "227", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "228", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "229", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "300", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "301", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "302", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "303", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "304", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "305", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "306", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "307", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "308", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "400", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "401", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "402", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "403", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "404", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "405", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "406", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "407", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "408", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "409", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = suiteRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "410", Floor = 2, RequiresCreditCard = true },     // suiteRoomType
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "441", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "442", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "443", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "444", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "445", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "500", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "501", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "502", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "503", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "504", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "505", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "506", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "507", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "508", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "509", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "510", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "511", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "512", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "513", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "514", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "515", Floor = 2 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "901", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "902", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "903", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "904", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "905", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "906", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "907", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "908", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "909", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "910", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "911", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "912", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "913", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "914", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "915", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "916", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "917", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "918", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "919", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "920", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "921", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "922", Floor = 3 },
                    // new Accommodation { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, AccommodationStatusId = availableStatus.Id, Number = "923", Floor = 3 }
                };
                await context.Accommodations!.AddRangeAsync(accommodations);
                await context.SaveChangesAsync();
            }

            if (!context.Taxes?.Any() ?? false)
            {
                var taxes = new List<Tax>
                {
                    new Tax { OrganizationId = 1, IsDeleted = false, TaxName = "PST", TaxRate = 8.0m },
                    new Tax { OrganizationId = 1, IsDeleted = false, TaxName = "GST", TaxRate = 5.0m },
                    new Tax { OrganizationId = 1, IsDeleted = false, TaxName = "HST", TaxRate = 13.0m },
                    new Tax { OrganizationId = 1, IsDeleted = false, TaxName = "QST", TaxRate = 9.875m },
                    new Tax { OrganizationId = 1, IsDeleted = false, TaxName = "Hotel", TaxRate = 4.0m },
                    new Tax { OrganizationId = 1, IsDeleted = false, TaxName = "Gratuity", TaxRate = 0.0m },
                    new Tax { OrganizationId = 1, IsDeleted = false, TaxName = "No Tax", TaxRate = 0.0m }
                };
                await context.Taxes!.AddRangeAsync(taxes);
                await context.SaveChangesAsync();
            }

            if (!context.AccommodationRates?.Any() ?? false)
            {
                AccommodationType lockerType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Locker")).Select(at => at).First();
                AccommodationType singleRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Single Room")).Select(at => at).First();
                AccommodationType singleTVRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Single Room + TV")).Select(at => at).First();
                // AccommodationType doubleRoom = context.AccommodationTypes!.Where(t => t.TypeName!.Equals("Double Room")).First();
                AccommodationType doubleTVRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Double Room + TV")).Select(at => at).First();
                // AccommodationType suite = context.AccommodationTypes!.Where(t => t.TypeName!.Equals("Suite")).First();
                Tax HST = context.Taxes!.Where(t => t.TaxName!.Equals("HST")).First();

                DateTime currentDate = DateTime.Now;

                var accommodationRates = new List<AccommodationRate>
                {
                //     new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, Hours = 6, RateName = "Weekend Standard - Locker", Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 26.0m, IsActive = true },
                //     new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, Hours = 6, RateName = "Weekend Standard - Single Room", Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 30.25m, IsActive = true },
                //     new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, Hours = 6, RateName = "Weekend Standard - Single Room + TV", Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 36.0m, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleRoom.Id, Hours = 8, RateName = "Weekend Standard Double Room", IsStandardRate = true, TotalPrice = 42.0m, Tax1 = HST.Id, IsActive = true },
                //     new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, Hours = 6, RateName = "Weekend Standard - Double Room + TV", Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 41.5m, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = suite.Id, Hours = 8, RateName = "Weekend Standard Suite", IsStandardRate = true, TotalPrice = 69.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoom.Id, Hours = 12, RateName = "Weekend Standard - Single Room", IsStandardRate = true, TotalPrice = 41.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomTV.Id, Hours = 12, RateName = "Weekend Standard - Single Room + TV", IsStandardRate = true, TotalPrice = 42.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleRoom.Id, Hours = 12, RateName = "Weekend Standard Double Room", IsStandardRate = true, TotalPrice = 48.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleRoomTV.Id, Hours = 12, RateName = "Weekend Standard - Double Room + TV", IsStandardRate = true, TotalPrice = 52.0m, Tax1 = HST.Id, IsActive = true },
                //    // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = suite.Id, Hours = 12, RateName = "Weekend Standard Suite", IsStandardRate = true, TotalPrice = 82.0m, Tax1 = HST.Id, IsActive = true },
                //     new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, Hours = 6, RateName = "Weekday Standard - Locker", Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 25.0m, IsActive = true },
                //     new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, Hours = 6, RateName = "Weekday Standard - Single Room", Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 30.0m, IsActive = true },
                //     new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, Hours = 6, RateName = "Weekday Standard - Single Room + TV", Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 36.0m, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleRoom.Id, Hours = 8, RateName = "Weekday Standard Double Room", IsStandardRate = true, TotalPrice = 39.0m, Tax1 = HST.Id, IsActive = true },
                //     new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, Hours = 6, RateName = "Weekday Standard - Double Room + TV", Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 42.0m, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = suite.Id, Hours = 8, RateName = "Weekday Standard Suite", IsStandardRate = true, TotalPrice = 61.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoom.Id, Hours = 12, RateName = "Weekday Standard - Single Room", IsStandardRate = true, TotalPrice = 36.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomTV.Id, Hours = 12, RateName = "Weekday Standard - Single Room + TV", IsStandardRate = true, TotalPrice = 38.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleRoom.Id, Hours = 12, RateName = "Weekday Standard Double Room", IsStandardRate = true, TotalPrice = 46.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleRoomTV.Id, Hours = 12, RateName = "Weekday Standard - Double Room + TV", IsStandardRate = true, TotalPrice = 49.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = suite.Id, Hours = 12, RateName = "Weekday Standard Suite", IsStandardRate = true, TotalPrice = 79.0m, Tax1 = HST.Id, IsActive = true },
                //     // Coupon related availability Rates.
                //     new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, Hours = 6, RateName = "Young Gunz Lockers", IsStandardRate = false, TotalPrice = 0.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoom.Id, Hours = 8, RateName = "TNT Monthly Event Single Rooms", IsStandardRate = false, TotalPrice = 0.0m, Tax1 = HST.Id, IsActive = true },
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = locker.Id, Hours = 6, RateName = "TNT Friday Lockers", IsStandardRate = false, TotalPrice = 0.0m, Tax1 = HST.Id, IsActive = true }
                //     // new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoom.Id, Hours = 8, RateName = "TNT Friday Single Rooms", IsStandardRate = false, TotalPrice = 0.0m, Tax1 = HST.Id, IsActive = true }

                    
                    new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, Hours = 6, RateName = "Weekend Standard - Locker", StartDayOfTheWeek = "Friday", RateStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 20, 0, 0), EndDayOfWeek = "Sunday", RateEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0), Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 26.0m, IsActive = true },
                    new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, Hours = 6, RateName = "Weekend Standard - Single Room", StartDayOfTheWeek = "Friday", RateStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 20, 0, 0), EndDayOfWeek = "Sunday", RateEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0), Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 30.25m, IsActive = true },
                    new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, Hours = 6, RateName = "Weekend Standard - Single Room + TV",StartDayOfTheWeek = "Friday",  RateStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 20, 0, 0), EndDayOfWeek = "Sunday", RateEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0), Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 36.0m, IsActive = true },
                    new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, Hours = 6, RateName = "Weekend Standard - Double Room + TV", StartDayOfTheWeek = "Friday", RateStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 20, 0, 0), EndDayOfWeek = "Sunday", RateEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0), Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 41.5m, IsActive = true },
                    new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, Hours = 6, RateName = "Weekday Standard - Locker", StartDayOfTheWeek = "Monday", RateStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0), EndDayOfWeek = "Friday", RateEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 20, 0, 0), Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 25.0m, IsActive = true },
                    new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, Hours = 6, RateName = "Weekday Standard - Single Room", StartDayOfTheWeek = "Monday", RateStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0), EndDayOfWeek = "Friday", RateEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 20, 0, 0), Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 30.0m, IsActive = true },
                    new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, Hours = 6, RateName = "Weekday Standard - Single Room + TV", StartDayOfTheWeek = "Monday", RateStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0), EndDayOfWeek = "Friday", RateEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 20, 0, 0), Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 36.0m, IsActive = true },
                    new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, Hours = 6, RateName = "Weekday Standard - Double Room + TV", StartDayOfTheWeek = "Monday", RateStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0), EndDayOfWeek = "Friday", RateEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 20, 0, 0), Tax1 = HST.Id, IsStandardRate = true, TotalPrice = 42.0m, IsActive = true },
                    new AccommodationRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, Hours = 6, RateName = "Young Gunz Lockers", IsStandardRate = false, TotalPrice = 0.0m, StartDayOfTheWeek = "Wednesday", RateStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 17, 0, 0), EndDayOfWeek = "Wednesday", RateEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 21, 0, 0), Tax1 = HST.Id, IsActive = true },
                };

                // Calculate the Price for each AccommodationRate
                foreach (var rate in accommodationRates)
                {
                    if (rate.Tax1 != null)
                    {
                        // Retrieve the Tax object for Tax1
                        var tax = context.Taxes!.Find(rate.Tax1);

                        if (tax != null)
                        {
                            // Calculate Price based on TotalPrice and TaxRate
                            rate.Price = rate.TotalPrice / (1 + (tax.TaxRate * 0.01m));
                        }
                    }
                }

                await context.AccommodationRates!.AddRangeAsync(accommodationRates);
                await context.SaveChangesAsync();
            }



            if (!context.BookingDetailStatuses?.Any() ?? false)
            {
                var bookingDetailStatuses = new List<BookingDetailStatus>
                {
                    new BookingDetailStatus { OrganizationId = 1, IsDeleted = false, BookingDetailStatusName = "Void" },
                    new BookingDetailStatus { OrganizationId = 1, IsDeleted = false, BookingDetailStatusName = "Refund" },
                    new BookingDetailStatus { OrganizationId = 1, IsDeleted = false, BookingDetailStatusName = "Transfer" },
                    new BookingDetailStatus { OrganizationId = 1, IsDeleted = false, BookingDetailStatusName = "Reserved" },
                    new BookingDetailStatus { OrganizationId = 1, IsDeleted = false, BookingDetailStatusName = "Booked" },
                    new BookingDetailStatus { OrganizationId = 1, IsDeleted = false, BookingDetailStatusName = "In-Progress" },
                    new BookingDetailStatus { OrganizationId = 1, IsDeleted = false, BookingDetailStatusName = "Cancelled" },
                    new BookingDetailStatus { OrganizationId = 1, IsDeleted = false, BookingDetailStatusName = "Renewed" }
                };
                await context.BookingDetailStatuses!.AddRangeAsync(bookingDetailStatuses);
                await context.SaveChangesAsync();
            }

            if (!context.CleaningTasks?.Any() ?? false)
            {
                var cleaningTasks = new List<CleaningTask>
                {
                    new CleaningTask { OrganizationId = 1, IsDeleted = false, TaskName = "All Clean", IsActive = true }
                    //new CleaningTask { OrganizationId = 1, IsDeleted = false, TaskName = "Bedding Changed" },
                    //new CleaningTask { OrganizationId = 1, IsDeleted = false, TaskName = "Floor Swept" },
                    //new CleaningTask { OrganizationId = 1, IsDeleted = false, TaskName = "Floor Mopped" },
                    //new CleaningTask { OrganizationId = 1, IsDeleted = false, TaskName = "Mirror Cleaned" },
                    //new CleaningTask { OrganizationId = 1, IsDeleted = false, TaskName = "Pillow Replaced" },
                    //new CleaningTask { OrganizationId = 1, IsDeleted = false, TaskName = "Garbage Changed" }
                };
                await context.CleaningTasks!.AddRangeAsync(cleaningTasks);
                await context.SaveChangesAsync();
            }

            if (!context.Coupons?.Any() ?? false)
            {
                AccommodationType lockerType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Locker")).Select(at => at).First();
                AccommodationType singleRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Single Room")).Select(at => at).First();
                // AccommodationType doubleTVRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Double Room + TV")).Select(at => at).First();

                // AccommodationRate lockerWeekday6Hr = context.AccommodationRates!.Where(ar => ar.RateName!.Equals("Weekday Standard - Locker") && ar.Hours.Equals(6)).First();
                // AccommodationRate singleRoomWeekday6Hr = context.AccommodationRates!.Where(ar => ar.RateName!.Equals("Weekday Standard - Single Room") && ar.Hours.Equals(6)).First();
                // AccommodationRate singleRoomWeekend6Hr = context.AccommodationRates!.Where(ar => ar.RateName!.Equals("Weekend Standard - Single Room") && ar.Hours.Equals(6)).First();
                AccommodationRate singleTVRoomWeekday6Hr = context.AccommodationRates!.Where(ar => ar.RateName!.Equals("Weekday Standard - Single Room + TV") && ar.Hours.Equals(6)).First();
                AccommodationRate singleTVRoomWeekend6Hr = context.AccommodationRates!.Where(ar => ar.RateName!.Equals("Weekend Standard - Single Room + TV") && ar.Hours.Equals(6)).First();
                AccommodationRate doubleTVRoomWeekday6Hr = context.AccommodationRates!.Where(ar => ar.RateName!.Equals("Weekday Standard - Double Room + TV") && ar.Hours.Equals(6)).First();
                AccommodationRate doubleTVRoomWeekend6Hr = context.AccommodationRates!.Where(ar => ar.RateName!.Equals("Weekend Standard - Double Room + TV") && ar.Hours.Equals(6)).First();
                AccommodationRate lockerYoungGunz = context.AccommodationRates!.Where(ar => ar.RateName!.Equals("Young Gunz Lockers") && ar.Hours.Equals(6)).First();

                var coupons = new List<Coupon>
                {
                    // new Coupon { AccommodationTypeId = lockerType.Id, CouponName = "1/2 Price Lockers", Description = "1/2 Price Lockers", DiscountPercent = 0.5m, IsExclusive = true, IsActive = true, IsCoupon = true },
                    // new Coupon { AccommodationTypeId = singleRoomType.Id, CouponName = "1/2 Price Rooms", Description = "1/2 Price Single Rooms", DiscountPercent = 0.5m, IsExclusive = true, IsActive = true, IsCoupon = true },
                    // new Coupon { AccommodationTypeId = lockerType.Id, CouponName = "Weekend 1/2 Price Student", Description = "Weekend 1/2 Price Student Locker", DiscountPercent = 0.5m, IsExclusive = true, IsActive = true, IsCoupon = true },
                    // new Coupon { AccommodationTypeId = singleRoomType.Id, CouponName = "Weekend 1/2 Price Student", Description = "Weekend 1/2 Price Student Single Room", DiscountPercent = 0.5m, IsExclusive = true, IsActive = false, IsCoupon = true },
                    // new Coupon { AccommodationTypeId = lockerType.Id, CouponName = "Weekday 1/2 Price Student Locker", Description = "Weekday 1/2 Price Student Locker", DiscountPercent = 0.5m, IsExclusive = true, IsActive = true, IsCoupon = true,
                    //     CouponDetails = new List<CouponDetail>
                    //     {
                    //         new CouponDetail { AccommodationRateId = lockerWeekday6Hr.Id }
                    //     }
                    // },
                    // new Coupon { AccommodationTypeId = singleRoomType.Id, CouponName = "Weekday 1/2 Price Student Single Room", Description = "Weekday 1/2 Price Student Single Room", DiscountPercent = 0.5m, IsExclusive = true, IsActive = true, IsCoupon = true,
                    //     CouponDetails = new List<CouponDetail>
                    //     {
                    //         new CouponDetail { AccommodationRateId = singleRoomWeekday6Hr.Id }
                    //     }
                    // },
                    // new Coupon { AccommodationTypeId = lockerType.Id, CouponName = "Spearhead $3 Off", Description = "Spearhead 3$ off Locker", DiscountDollars = -3.00m, IsExclusive = true, IsActive = true, IsCoupon = true },
                    // new Coupon { AccommodationTypeId = singleRoomType.Id, CouponName = "VIP Pass", Description = "VIP Pass Single 8Hr Room on weekdays Only.", DiscountPercent = 1.0m, IsExclusive = true, IsActive = true, IsCoupon = true,
                    //     CouponDetails = new List<CouponDetail>
                    //     {
                    //         new CouponDetail { AccommodationRateId = singleRoomWeekday6Hr.Id }
                    //     } 
                    // },
                    // new Coupon { CouponName = "VIP Pass Override", Description = "VIP Pass Single 8Hr Room every day", DiscountPercent = 1.0m, IsExclusive = true, IsActive = true, IsCoupon = false,
                    //     CouponDetails = new List<CouponDetail>
                    //     {
                    //         new CouponDetail { AccommodationRateId = singleRoomWeekday6Hr.Id },
                    //         new CouponDetail { AccommodationRateId = singleRoomWeekend6Hr.Id }
                    //     }
                    // },
                    new Coupon { OrganizationId = 1, IsDeleted = false, CouponName = "VIP++ Person", Description = "VIP++ Person is available to ALL Room types every day.", DiscountPercent = 100.0m, IsExclusive = true, IsActive = true, IsCoupon = false },
                    // PROMOs
                    new Coupon { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, CouponName = "Birthday Pass", Description = "Birthday Pass 100% Off", DiscountPercent = 100.0m, IsExclusive = true, IsActive = true, IsCoupon = true },
                    new Coupon { OrganizationId = 1, IsDeleted = false, CouponName = "TV Not Working $3 Off", Description = "TV not working $3 Off Room price", DiscountDollars = 3.0m, IsExclusive = false, IsActive = true, IsCoupon = false,
                        CouponDetails = new List<CouponDetail>
                        {
                            new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = singleTVRoomWeekday6Hr.Id },
                            new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = singleTVRoomWeekend6Hr.Id },
                            new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = doubleTVRoomWeekday6Hr.Id },
                            new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = doubleTVRoomWeekend6Hr.Id },
                            // new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = suiteWkDay8Hr.AccommodationRateId },
                            // new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = suiteWkEnd8Hr.AccommodationRateId },
                            /// new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = singleTVRoomWkDay12Hr.AccommodationRateId },
                            /// new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = singleTVRoomWkEnd12Hr.AccommodationRateId },
                            /// new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = doubleTVRoomWkDay12Hr.AccommodationRateId },
                            /// new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = doubleTVRoomWkEnd12Hr.AccommodationRateId },
                            // new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = suiteWkDay12Hr.AccommodationRateId },
                            // new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = suiteWkEnd12Hr.AccommodationRateId }
                        }
                    },
                    // new Coupon { AccommodationTypeId = doubleTVRoomType.Id, CouponName = "TV Not Working $3 Off", Description = "TV not working $3 Off Double TV Room", DiscountDollars = -3.00m, IsExclusive = false, IsActive = true, IsCoupon = false },
                    // Fill these out from notes.
                    new Coupon
                    { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, CouponName = "Young Gunz Locker", Description = "Young Gunz Discount for Lockers from 5pm-12pm on Wednesdays", FlatRate = 3.0m, IsExclusive = true, IsActive = true, IsCoupon = true,
                        CouponDetails = new List<CouponDetail>
                        {
                            new CouponDetail { OrganizationId = 1, IsDeleted = false, AccommodationRateId = lockerYoungGunz.Id }
                        }
                    }
                    ////new Coupon { CouponName = "TNT Monthly Event Single Room", DiscountPercent = 0.5m, AccommodationType = singleRooms, AccommodationTypeId = singleRooms.AccommodationTypeId, Description = "TNT Monthly Event 50% Off Single Rooms every 2nd Tuesday of the Month", IsExclusive = true, IsCoupon = true, IsActive = true, CouponDetails =
                    ////new ObservableListSource<CouponDetail>
                    ////{
                    ////    new CouponDetail { AccommodationRateId = TNTMonthlyEventSingleRoom.AccommodationRateId }
                    ////} },
                    //////new Coupon { CouponName = "TNT Friday Free Locker", DiscountPercent = 1.0m, AccommodationType = lockers, AccommodationTypeId = lockers.AccommodationTypeId, Description = "TNT Friday Free Locker every friday", IsExclusive = true, IsCoupon = true, IsActive = true, CouponDetails =
                    //////new ObservableListSource<CouponDetail>
                    //////{
                    //////    new CouponDetail{ AccommodationRateId = TNTFridayLocker.AccommodationRateId }
                    //////} }
                    ////new Coupon { CouponName = "TNT Friday Single Roomr", DiscountPercent = 0.5m, AccommodationType = singleRooms, AccommodationTypeId = singleRooms.AccommodationTypeId, Description = "TNT Friday 50% Off Sinlge Room every friday", IsExclusive = true, IsCoupon = true, IsActive = true, CouponDetails =
                    ////new ObservableListSource<CouponDetail>
                    ////{
                    ////    new CouponDetail{ AccommodationRateId = TNTFridaySingleRoom.AccommodationRateId }
                    ////} },
                    ////new Coupon { CouponName = "Email Pass Free Room", DiscountPercent = 1.0m, AccommodationType = singleRooms, AccommodationTypeId = singleRooms.AccommodationTypeId, Description = "Email Event Pass Free Room, usually on Sundays from 7pm-8pm If no rooms are available you may substitute a locker until one is available", IsExclusive = true, IsCoupon = true, IsActive = true, CouponDetails = new ObservableListSource<CouponDetail>
                    ////{
                    ////    new CouponDetail { AccommodationRateId = lockerWkDay8Hr.AccommodationRateId },
                    ////    new CouponDetail { AccommodationRateId = singleRoomWkDay8Hr.AccommodationRateId }
                    ////} },
                    ////new Coupon { CouponName = "Bartender Pass Weekends 50% Off", DiscountPercent = 0.5m, AccommodationType = singleRooms, AccommodationTypeId = singleRooms.AccommodationTypeId, Description = "Bartender Pass Weekends 50% Off Lockers & Single Rooms", IsExclusive = true, IsCoupon = true, IsActive = true, CouponDetails = 
                    ////new ObservableListSource<CouponDetail>
                    ////{
                    ////    new CouponDetail{ AccommodationRateId = lockerWkEnd8Hr.AccommodationRateId },
                    ////    new CouponDetail{ AccommodationRateId = singleRoomWkEnd8Hr.AccommodationRateId }
                    ////} },
                    ////new Coupon { CouponName = "Bartender Pass Weekday 100% Off", DiscountPercent = 1.0m, AccommodationType = singleRooms, AccommodationTypeId = singleRooms.AccommodationTypeId, Description = "Bartender Pass Weekday 100% Off Lockers & Single Rooms", IsExclusive = true, IsCoupon = true, IsActive = true, CouponDetails = 
                    ////new ObservableListSource<CouponDetail>
                    ////{
                    ////    new CouponDetail { AccommodationRateId = lockerWkDay8Hr.AccommodationRateId },
                    ////    new CouponDetail { AccommodationRateId = singleRoomWkDay8Hr.AccommodationRateId }
                    ////} },
                    ////////new Coupon { CouponName = "1/2 Price Single Room", DiscountPercent = 0.5m, Description = "1/2 Price Single Room on Weekends and weekdays. Also available for lockers when no Sinlge Rooms are available.", IsActive = true, IsExclusive = true, IsCoupon = true, CouponDetails = 
                    ////////new ObservableListSource<CouponDetail>
                    ////////{
                    ////////    new CouponDetail { AccommodationRateId = lockerWkDay8Hr.AccommodationRateId },
                    ////////    new CouponDetail { AccommodationRateId = lockerWkEnd8Hr.AccommodationRateId },
                    ////////    new CouponDetail { AccommodationRateId = singleRoomWkDay8Hr.AccommodationRateId },
                    ////////    new CouponDetail { AccommodationRateId = singleRoomWkEnd8Hr.AccommodationRateId }
                    ////////}
                    ////////},
                    ////new Coupon
                    ////{
                    ////    CouponName = "1/2 Price Locker",
                    ////    DiscountPercent = 0.5m,
                    ////    Description = "1/2 Price Locker available on Weekdays and Weekends.",
                    ////    IsActive = true,
                    ////    IsExclusive = true,
                    ////    IsCoupon = true,
                    ////    CouponDetails =
                    ////new ObservableListSource<CouponDetail>
                    ////{
                    ////    new CouponDetail { AccommodationRateId = lockerWkDay8Hr.AccommodationRateId },
                    ////    new CouponDetail { AccommodationRateId = lockerWkEnd8Hr.AccommodationRateId }
                    ////}
                    ////}
                };
                await context.Coupons!.AddRangeAsync(coupons);
                await context.SaveChangesAsync();
            }

            if (!context.CreditCardTypes?.Any() ?? false)
            {
                var creditCardTypes = new List<CreditCardType>
                {
                    new CreditCardType { OrganizationId = 1, IsDeleted = false, CreditCardTypeName = "MasterCard" },
                    new CreditCardType { OrganizationId = 1, IsDeleted = false, CreditCardTypeName = "VISA" },
                    new CreditCardType { OrganizationId = 1, IsDeleted = false, CreditCardTypeName = "AMEX" }
                };
                await context.CreditCardTypes!.AddRangeAsync(creditCardTypes);
                await context.SaveChangesAsync();
            }

            if (!context.DiscountRates?.Any() ?? false)
            {
                AccommodationType lockerType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Locker")).Select(at => at).First();
                AccommodationType singleRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Single Room")).Select(at => at).First();
                AccommodationType singleTVRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Single Room + TV")).Select(at => at).First();
                AccommodationType doubleTVRoomType = context.AccommodationTypes!.Where(at => at.TypeName!.Equals("Double Room + TV")).Select(at => at).First();
                var discountRates = new List<DiscountRate>
                {
                    new DiscountRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = lockerType.Id, Hours = 6, RateName = "None", DiscountAmount = 0m, Optional = false, IsActive = true },
                    new DiscountRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleRoomType.Id, Hours = 6, RateName = "None", DiscountAmount = 0m, Optional = false, IsActive = true },
                    new DiscountRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = singleTVRoomType.Id, Hours = 6, RateName = "None", DiscountAmount = 0m, Optional = false, IsActive = true },
                    new DiscountRate { OrganizationId = 1, IsDeleted = false, AccommodationTypeId = doubleTVRoomType.Id, Hours = 6, RateName = "None", DiscountAmount = 0m, Optional = false, IsActive = true }
                };
                await context.DiscountRates!.AddRangeAsync(discountRates);
                await context.SaveChangesAsync();
            }

            if (!context.ExchangeRates?.Any() ?? false)
            {
                var exchangeRates = new List<ExchangeRate>
                {
                    new ExchangeRate { OrganizationId = 1, IsDeleted = false, RateName = "US Exchange Rate", Rate = 1.2m },
                    new ExchangeRate { OrganizationId = 1, IsDeleted = false, RateName = "Euro Exchange Rate", Rate = 1.3m }
                };
                await context.ExchangeRates!.AddRangeAsync(exchangeRates);
                await context.SaveChangesAsync();
            }

            if (!context.MaintenanceTasks?.Any() ?? false)
            {
                var maintenanceTasks = new List<MaintenanceTask>
                {
                    new MaintenanceTask { OrganizationId = 1, IsDeleted = false, TaskName = "Lost Key", IsEndUserFixable = true, IsActive = true},
                    new MaintenanceTask { OrganizationId = 1, IsDeleted = false, TaskName = "Broken Mirror", IsEndUserFixable = true, IsActive = true},
                    new MaintenanceTask { OrganizationId = 1, IsDeleted = false, TaskName = "Flooding", IsEndUserFixable = true, IsActive = true},
                    new MaintenanceTask { OrganizationId = 1, IsDeleted = false, TaskName = "Bio-Hazard", IsEndUserFixable = true, IsActive = true},
                    new MaintenanceTask { OrganizationId = 1, IsDeleted = false, TaskName = "Furniture Damage", IsEndUserFixable = true, IsActive = true},
                    new MaintenanceTask { OrganizationId = 1, IsDeleted = false, TaskName = "Extensive Cleaning", IsEndUserFixable = true, IsActive = true},
                    new MaintenanceTask { OrganizationId = 1, IsDeleted = false, TaskName = "Broken Lock", IsEndUserFixable = true, IsActive = true},
                    new MaintenanceTask { OrganizationId = 1, IsDeleted = false, TaskName = "Electrical Issue", IsEndUserFixable = true, IsActive = true}
                };
                await context.MaintenanceTasks!.AddRangeAsync(maintenanceTasks);
                await context.SaveChangesAsync();
            }

            if (!context.MembershipTypes?.Any() ?? false)
            {
                var membershipTypes = new List<MembershipType>
                {
                    new MembershipType { OrganizationId = 1, IsDeleted = false, MembershipTypeName = "None", DiscountFactor = 1.00m, DiscountAmount = 0m, DepositRequired = true, DisplayOrder = 6, CanReserve = false, CanTab = false, IsActive = true },
                    new MembershipType { OrganizationId = 1, IsDeleted = false, MembershipTypeName = "Bronze", DiscountFactor = 1.00m, DiscountAmount = 0m, DepositRequired = false, DisplayOrder = 5, CanReserve = false, CanTab = false, IsActive = true, MemberShipPrice = 15m },
                    // new MembershipType { OrganizationId = 1, IsDeleted = false, MembershipTypeName = "Silver", DiscountFactor = 1.00m, DiscountAmount = -1.0m, DepositRequired = false, DisplayOrder = 4, CanReserve = false, CanTab = false, IsActive = false },
                    // new MembershipType { OrganizationId = 1, IsDeleted = false, MembershipTypeName = "Gold", DiscountFactor = 1.00m, DiscountAmount = -1.0m, DepositRequired = false, DisplayOrder = 3, CanReserve = false, CanTab = false, IsActive = false },
                    new MembershipType { OrganizationId = 1, IsDeleted = false, MembershipTypeName = "Platinum", DiscountFactor = 1.00m, DiscountAmount = 0m, DepositRequired = false, DisplayOrder = 2, CanReserve = true, CanTab = true, IsActive = true, MemberShipPrice = 100m },
                    // new MembershipType { OrganizationId = 1, IsDeleted = false, MembershipTypeName = "New Member Bronze", DiscountFactor = 1.00m, DiscountAmount = -1.0m, DepositRequired = false, DisplayOrder = 7, CanReserve = false, CanTab = false, IsActive = true },
                    // new MembershipType { OrganizationId = 1, IsDeleted = false, MembershipTypeName = "New Member Platinum", DiscountFactor = 1.00m, DiscountAmount = -1.0m, DepositRequired = false, DisplayOrder = 7, CanReserve = true, CanTab = true, IsActive = true },
                    new MembershipType { OrganizationId = 1, IsDeleted = false, MembershipTypeName = "VIP", DiscountFactor = 1.00m, DiscountAmount = 0.0m, DepositRequired = false, DisplayOrder = 1, CanReserve = true, CanTab = false, IsActive = false },
                    new MembershipType { OrganizationId = 1, IsDeleted = false, MembershipTypeName = "Manager Override", DiscountFactor = 1.00m, DiscountAmount = 0.0m, DepositRequired = true, DisplayOrder = 0, CanReserve = false, CanTab = false, IsActive = true }
                };
                await context.MembershipTypes!.AddRangeAsync(membershipTypes);
                await context.SaveChangesAsync();
            }

            if (!context.Memberships?.Any() ?? false)
            {
                MembershipType membershipTypeBronze = context.MembershipTypes!.Where(mt => mt.MembershipTypeName!.Equals("Bronze")).First();
                MembershipType membershipTypePlatinum = context.MembershipTypes!.Where(mt => mt.MembershipTypeName!.Equals("Platinum")).First();

                CreditCardType creditCardTypeVISA = context.CreditCardTypes!.Where(cct => cct.CreditCardTypeName!.Equals("VISA")).First();

                var memberships = new List<Membership>
                {
                    //new Membership { OrganizationId = 1, IsDeleted = false, MemberTypeId = context.MembershipTypes.Local.Where(m => m.MembershipTypeName.Equals("Bronze")).First().MembershipTypeId, MembershipNum = 11223344, FirstName = "Dan", LastName = "Kalinich", Address = "1299 Danforth Rd.", Phone = "(416) 884 - 6040", CreditCardNumber = "4501-0000-0000-1234", CreditCardTypeId = context.CreditCardTypes.Local.Where(c => c.CreditCardTypeName.Equals("MasterCard")).First().CreditCardTypeId, CreditCardExpiry = new DateTime(2019, 12, 12), MembershipStartDate = new DateTime(2017, 7, 7), MembershipEndDate = new DateTime(2018, 7, 6), IsActive = true },
                    //new Membership { OrganizationId = 1, IsDeleted = false, MemberTypeId = context.MembershipTypes.Local.Where(m => m.MembershipTypeName.Equals("Platinum")).First().MembershipTypeId, MembershipNum = 11223221, FirstName = "Jeff", LastName = "Lay", Address = "1299 Danforth Rd.", Phone = "(647) 529 - 1195", CreditCardNumber = "5446-0000-0000-3214", CreditCardTypeId = context.CreditCardTypes.Local.Where(c => c.CreditCardTypeName.Equals("VISA")).First().CreditCardTypeId, CreditCardExpiry = new DateTime(2020, 1, 15), MembershipStartDate = new DateTime(2017, 1, 7), MembershipEndDate = new DateTime(2020, 1, 6), IsActive = true },
                    //new Membership { OrganizationId = 1, IsDeleted = false, MemberTypeId = context.MembershipTypes.Local.Where(m => m.MembershipTypeName.Equals("Bronze")).First().MembershipTypeId, MembershipNum = 22113324, FirstName = "John", LastName = "Doe", Address = "123 HumpMe Blvd.", Phone = "(289) 999 - 9999", CreditCardNumber = "6200-0000-0000-4321", CreditCardTypeId = context.CreditCardTypes.Local.Where(c => c.CreditCardTypeName.Equals("AMEX")).First().CreditCardTypeId, CreditCardExpiry = new DateTime(2022, 10, 22), MembershipStartDate = new DateTime(2017, 9, 23), MembershipEndDate = new DateTime(2018, 9, 22), IsActive = false },
                    //new Membership { OrganizationId = 1, IsDeleted = false, MemberTypeId = context.MembershipTypes.Local.Where(m => m.MembershipTypeName.Equals("Platinum")).First().MembershipTypeId, MembershipNum = 55123421, FirstName = "James", LastName = "Rpbonspn", Address = "55 Popcorn Drive", Phone = "(647) 888 - 8888", CreditCardNumber = "4501-0000-0000-5577", CreditCardTypeId = context.CreditCardTypes.Local.Where(c => c.CreditCardTypeName.Equals("VISA")).First().CreditCardTypeId, CreditCardExpiry = new DateTime(2021, 9, 4), MembershipStartDate = new DateTime(2017, 3, 3), MembershipEndDate = new DateTime(2018, 3, 2), IsActive = true },
                    //new Membership { OrganizationId = 1, IsDeleted = false, MemberTypeId = context.MembershipTypes.Local.Where(m => m.MembershipTypeName.Equals("Platinum")).First().MembershipTypeId, MembershipNum = 9956545, FirstName = "Toby", LastName = "Tompson", Address = "89 Yonge St.", Phone = "(416) 222 - 9888", CreditCardNumber = "4456-0000-0000-9956", CreditCardTypeId = context.CreditCardTypes.Local.Where(c => c.CreditCardTypeName.Equals("VISA")).First().CreditCardTypeId, CreditCardExpiry = new DateTime(2022, 10, 1), MembershipStartDate = new DateTime(2017, 10, 21), MembershipEndDate = new DateTime(2018, 10, 20), IsActive = true },
                    //new Membership { OrganizationId = 1, IsDeleted = false, MemberTypeId = context.MembershipTypes.Local.Where(m => m.MembershipTypeName.Equals("Bronze")).First().MembershipTypeId, MembershipNum = 5533244, FirstName = "Rob", LastName = "Zaina", Address = "312 Keele St", Phone = "(905) 998 - 1223", CreditCardNumber = "5545-0000-0000-0115", CreditCardTypeId = context.CreditCardTypes.Local.Where(c => c.CreditCardTypeName.Equals("MasterCard")).First().CreditCardTypeId, CreditCardExpiry = new DateTime(2019, 7, 24), MembershipStartDate = new DateTime(2017, 11, 17), MembershipEndDate = new DateTime(2018, 11, 16), IsActive = true },
                    //new Membership { OrganizationId = 1, IsDeleted = false, MemberTypeId = context.MembershipTypes.Local.Where(m => m.MembershipTypeName.Equals("Platinum")).First().MembershipTypeId, MembershipNum = 5544123, FirstName = "Doug", LastName = "Ghost", Address = "5554 Shppard Rd.", Phone = "(416) 443 - 6654", CreditCardNumber = "4454-0000-0000-4657", CreditCardTypeId = context.CreditCardTypes.Local.Where(c => c.CreditCardTypeName.Equals("AMEX")).First().CreditCardTypeId, CreditCardExpiry = new DateTime(2020, 10, 1), MembershipStartDate = new DateTime(2017, 2, 10), MembershipEndDate = new DateTime(2018, 2, 9), IsActive = true },
                    new Membership { OrganizationId = 1, IsDeleted = false, MembershipTypeId = membershipTypeBronze.Id, MembershipNum = 00000000, FirstName = "New Member", LastName = "Bronze", Address = "0000 None Rd.", Phone = "(000) 000 - 0000", CreditCardNumber = "0000-0000-0000-0000", CreditCardTypeId = creditCardTypeVISA.Id, CreditCardExpiry = new DateTime(2038, 1, 1), MembershipStartDate = new DateTime(2018, 1, 1), MembershipEndDate = new DateTime(2038, 1, 1), IsActive = true },
                    new Membership { OrganizationId = 1, IsDeleted = false, MembershipTypeId = membershipTypePlatinum.Id, MembershipNum = 00000001, FirstName = "New Member", LastName = "Platinum", Address = "0001 None Rd.", Phone = "(000) 000 - 0001", CreditCardNumber = "0000-0000-0000-0001", CreditCardTypeId = creditCardTypeVISA.Id, CreditCardExpiry = new DateTime(2038, 1, 1), MembershipStartDate = new DateTime(2018, 1, 1), MembershipEndDate = new DateTime(2038, 1, 1), IsActive = true }
                };
                await context.Memberships!.AddRangeAsync(memberships);
                await context.SaveChangesAsync();
            }

            if (!context.Organizations?.Any() ?? false)
            {
                var organizations = new List<Organization>
                {
                    new Organization {
                        OrganizationName = "Club Hamilton",
                        Street = "11th street",
                        City = "Mohali",
                        Province = "Punjab",
                        PostalCode = "140307",
                        Country = "India",
                        RegistrationNumber = "DJ28H843",
                        Logo = "Logo/ClubHamiltonLogoSm.png",
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    }
                };
                await context.Organizations!.AddRangeAsync(organizations);
                await context.SaveChangesAsync();
            }

            if (!context.ProblemCategories?.Any() ?? false)
            {
                var problemCategories = new List<ProblemCategory>
                {
                    new ProblemCategory { OrganizationId = 1, IsDeleted = false, ProblemCategoryName = "System" },
                    new ProblemCategory { OrganizationId = 1, IsDeleted = false, ProblemCategoryName = "Invoice" },
                    new ProblemCategory { OrganizationId = 1, IsDeleted = false, ProblemCategoryName = "Membership Sale" },
                    new ProblemCategory { OrganizationId = 1, IsDeleted = false, ProblemCategoryName = "Member" }
                };
                await context.ProblemCategories!.AddRangeAsync(problemCategories);
                await context.SaveChangesAsync();
            }

            if (!context.ReceiptMessagesAndCoupons?.Any() ?? false)
            {
                var receiptMessagesAndCoupons = new List<ReceiptMessagesAndCoupon>
                {
                    new ReceiptMessagesAndCoupon
                    {
                        OrganizationId = 1,
                        IsDeleted = false,
                        Disclaimer = "By signing you have agreed to the terms and conditions set out by this facility eg: The return of Keys, Towels and other items. Please see the front desk for a complete list of details",
                        Message1 = "Free Wifi User: SpaSx Password: toronto1"
                    },
                };

                await context.ReceiptMessagesAndCoupons!.AddRangeAsync(receiptMessagesAndCoupons);
                await context.SaveChangesAsync();
            }

            if (!context.SalesTypes?.Any() ?? false)
            {
                var salesTypes = new List<SalesType>
                {
                    new SalesType { OrganizationId = 1, IsDeleted = false, SalesTypeName = "Booking" },
                    new SalesType { OrganizationId = 1, IsDeleted = false, SalesTypeName = "Bar Sales" },
                    new SalesType { OrganizationId = 1, IsDeleted = false, SalesTypeName = "Membership Sale" }
                };
                await context.SalesTypes!.AddRangeAsync(salesTypes);
                await context.SaveChangesAsync();
            }

            if (!context.ShiftSetUps?.Any() ?? false)
            {
                var shiftSetUps = new List<ShiftSetUp>
                {
                    new ShiftSetUp { OrganizationId = 1, IsDeleted = false, ShiftNumber = 1, ShiftStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0), ShiftEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0), IsActive = true },
                    new ShiftSetUp { OrganizationId = 1, IsDeleted = false, ShiftNumber = 2, ShiftStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0), ShiftEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0), IsActive = true },
                     new ShiftSetUp { OrganizationId = 1, IsDeleted = false, ShiftNumber = 3, ShiftStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0), ShiftEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0), IsActive = true }
                    //new ShiftSetUp { OrganizationId = 1, IsDeleted = false, ShiftNumber = 1, ShiftStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0), ShiftEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0), IsActive = true },
                    //new ShiftSetUp { OrganizationId = 1, IsDeleted = false, ShiftNumber = 2, ShiftStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0), ShiftEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0), IsActive = true }
                };
                await context.ShiftSetUps!.AddRangeAsync(shiftSetUps);
                await context.SaveChangesAsync();
            }

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<SeedData>();
            logger.LogError(ex, "An error occured during seeding");
        }
    }
}