using System;
using System.Collections.Generic;

namespace CoMute.Abstractions.Models;

public class CarPoolOpportunity : BaseEntity
{
    public CarPoolOpportunity()
    {
    }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Origin { get; set; }
    public string DaysAvailable { get; set; }
    public string Destination { get; set; }
    public string AvailableSeats { get; set; }
    public string Owner { get; set; }
    public string Notes { get; set; }
    public string UserId { get; set; }
}