using TollFeeCalculator.Interfaces;

namespace TollFeeCalculator.Services
{
    public class Car : IVehicle
    {
        public string GetVehicleType()
        {
            return "Car";
        }
    }
}
