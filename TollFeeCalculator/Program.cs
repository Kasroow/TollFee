// See https://aka.ms/new-console-template for more information

using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Services;

IVehicle vehicle = new Car();
DateTime[] dates =
[
    new DateTime(2023, 6, 22, 6, 15, 0),
    new DateTime(2023, 6, 22, 6, 17, 0),
    new DateTime(2023, 6, 22, 6, 32, 0),
    new DateTime(2023, 6, 22, 6, 34, 0),
    new DateTime(2023, 6, 22, 6, 30, 0)
];

TollFee tollFee = new();
int totalFee = tollFee.GetTollFee(vehicle, dates);

Console.WriteLine($"Total toll fee for the vehicle is: {totalFee}");
