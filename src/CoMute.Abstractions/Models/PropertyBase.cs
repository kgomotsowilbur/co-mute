using System;
namespace CoMute.Abstractions.Models;

public class PropertyBase
{
    public PropertyBase()
    {
        this.Other = "N/A";
    }
    public string Other { get; set; }
}

