using TollFeeCalculator.Exceptions;
using TollFeeCalculator.Interfaces;

namespace TollFeeCalculator.Services
{
    public class TollFee
    {
        private const int Year = 2023;

        /// <summary>
        ///  Calculate the total toll fee for one day
        /// </summary>
        /// <param name="vehicle">Interface of vehicle</param>
        /// <param name="dates">Date of times </param>
        /// <returns>Total cost of the day</returns>
        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            if (vehicle == null)
            {
                throw new VehicleNullException("Vehicle can not be empty");
            }

            if (dates == null || dates.Length == 0)
            {
                throw new ArgumentException("Dates cannot be null or empty");
            }

            DateTime intervalStart = dates[0];
            int maxFee = 60;
            int totalFee = 0;

            foreach (DateTime date in dates)
            {
                int nextFee = GetFee(vehicle, date);
                int tempFee = GetFee(vehicle, date: intervalStart);

                TimeSpan diff = date - intervalStart;
                long minutes = (long)diff.TotalMinutes;

                if (minutes <= 59)
                {
                    if (totalFee > 0)
                    {
                        totalFee -= tempFee;
                    }

                    if (nextFee >= tempFee)
                    {
                        tempFee = nextFee;
                    }

                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }

            return totalFee > maxFee ? maxFee : totalFee;
        }

        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            if (vehicle == null)
            {
                return false;
            }

            List<string> freeVehicleTypes =
            [
            "Motorbike",
            "Tractor",
            "Emergency",
            "Diplomat",
            "Foreign",
            "Military"
            ];

            string vehicleType = vehicle.GetVehicleType();
            return freeVehicleTypes.Contains(vehicleType);
        }

        private int GetFee(IVehicle vehicle, DateTime date)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
            {
                return 0;
            }

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute <= 29) return 8;
            if (hour == 6 && minute >= 30) return 13;
            if (hour == 7) return 18;
            if (hour == 8 && minute <= 29) return 13;
            if (hour >= 8 && hour <= 14 && minute <= 59) return 8;
            if (hour == 15 && minute <= 29) return 13;
            if (hour == 15 && minute >= 30 || hour == 16) return 18;
            if (hour == 17) return 13;
            if (hour == 18 && minute <= 29) return 8;

            return 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            if (year == Year)
            {
                if (month == 1 && day == 1 ||
                    month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    month == 7 ||
                    month == 11 && day == 1 ||
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
