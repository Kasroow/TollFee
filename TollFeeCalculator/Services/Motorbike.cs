using TollFeeCalculator.Interfaces;

namespace TollFeeCalculator.Services
{
    public class Motorbike : IVehicle
    {
        public string GetVehicleType()
        {
            return "Motorbike";
        }
    }
}
