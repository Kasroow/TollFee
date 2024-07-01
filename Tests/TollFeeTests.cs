using TollFeeCalculator.Exceptions;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Services;

namespace Tests;

public class TollFeeTests
{
    private const int Year = 2023;

    [Fact]
    public void GetTollFee_VehicleIsNull_ThrowsVehicleNullException()
    {
        // Arrange
        TollFee tollFeeService = new();
        DateTime[] dates = [];

        // Act & Assert
        Assert.Throws<VehicleNullException>(() => tollFeeService.GetTollFee(null, dates));
    }

    [Fact]
    public void GetTollFee_DatesIsNull_ThrowsArgumentException()
    {
        // Arrange
        TollFee tollFeeService = new();
        IVehicle vehicle = new Motorbike();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => tollFeeService.GetTollFee(vehicle, null));
    }

    [Fact]
    public void GetTollFee_DatesIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        TollFee tollFeeService = new();
        IVehicle vehicle = new Motorbike();
        DateTime[] dates = [];

        // Act & Assert
        Assert.Throws<ArgumentException>(() => tollFeeService.GetTollFee(vehicle, dates));
    }

    [Fact]
    public void GetTollFee_SingleEntry_CalculatesCorrectFee()
    {
        // Arrange
        TollFee tollFeeService = new();
        IVehicle vehicle = new Car();
        DateTime[] dates = [
            new(Year, 6, 22, 6, 15, 0)
        ];

        // Act
        int fee = tollFeeService.GetTollFee(vehicle, dates);

        // Assert
        Assert.Equal(8, fee);
    }

    [Fact]
    public void GetTollFee_EntriesWithinSameHour_CalculatesCorrectFee()
    {
        // Arrange
        TollFee tollFeeService = new();
        IVehicle vehicle = new Car();
        DateTime[] dates = [
            new(Year, 6, 22, 6, 0, 0),
                new(Year, 6, 22, 6, 15, 0),
                new(Year, 6, 22, 6, 30, 0)
        ];

        // Act
        int fee = tollFeeService.GetTollFee(vehicle, dates);

        // Assert
        Assert.Equal(13, fee);
    }

    [Fact]
    public void GetTollFee_EntriesAcrossHours_CalculatesCorrectFee()
    {
        // Arrange
        TollFee tollFeeService = new();
        IVehicle vehicle = new Car();
        DateTime[] dates = [
            new(Year, 6, 22, 6, 0, 0),
                new(Year, 6, 22, 7, 0, 0),
                new(Year, 6, 22, 9, 0, 0)
        ];

        // Act
        int fee = tollFeeService.GetTollFee(vehicle, dates);

        // Assert
        Assert.Equal(34, fee);
    }

    [Fact]
    public void GetTollFee_CorrectFee_ForTollFreeDate()
    {
        // Arrange
        TollFee tollFeeService = new();
        IVehicle vehicle = new Car();
        DateTime[] dates = [
            new(Year, 6, 21, 15, 0, 0)
        ];

        // Act
        int fee = tollFeeService.GetTollFee(vehicle, dates);

        // Assert
        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_CorrectFee_ForTollFreeVehicle()
    {
        // Arrange
        TollFee tollFeeService = new();
        IVehicle vehicle = new Motorbike();
        DateTime[] dates = [
            new(Year, 6, 21, 15, 0, 0)
        ];

        // Act
        int fee = tollFeeService.GetTollFee(vehicle, dates);

        // Assert
        Assert.Equal(0, fee);
    }

}