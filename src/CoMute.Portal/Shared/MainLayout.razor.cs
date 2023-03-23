using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CoMute.Portal.Shared;

public class MainLayoutPageBase : LayoutComponentBase
{
    public bool _drawerOpen = true;
    public MudTheme CurrentTheme
    {
        get { return this._currentTheme; }
    }

    public void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private MudTheme _currentTheme = new()
    {
        Palette = new Palette()
        {
            Primary = "#3E9FF0",
            Secondary = "#050D4B",
            Tertiary = "#ADD8E6",
            AppbarBackground = "#FFFFFF",
            AppbarText = "#3E9FF0",
            Surface = "#FCFCFC",
            TextPrimary = "424242"
        }
    };
}