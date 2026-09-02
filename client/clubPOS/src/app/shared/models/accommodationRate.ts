export interface AccommodationRate {
  accommodationRateId: number;
  accommodationTypeId: number;
  hours: number;
  rateName: string;
  price: number;
  startDayOfTheWeek: null | number;
  rateStartTime: null | string;
  endDayOfWeek: null | number;
  rateEndTime: null | string;
  tax1: number;
  tax2: null | number;
  tax3: null | number;
  tax4: null | number;
  tax5: null | number;
  isStandardRate: boolean;
  totalPrice: number;
  isActive: boolean;
}