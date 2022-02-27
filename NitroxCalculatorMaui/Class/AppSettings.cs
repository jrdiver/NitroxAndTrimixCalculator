namespace NitroxCalculatorMaui.Class;

internal static class AppSettings
{
    public static string SelectedUnit
    {
        get => Preferences.Get("SelectedUnit", "Imperial");
        set => Preferences.Set("SelectedUnit", value);
    }

    public static double DefaultTankSize
    {
        get => Preferences.Get("DefaultTankSize", 2265.35);
        set => Preferences.Set("DefaultTankSize", value);
    }

    public static double DefaultStartPressure
    {
        get => Preferences.Get("DefaultStartPressure", 34.4738);
        set => Preferences.Set("DefaultStartPressure", value);
    }

    public static double DefaultEndPressure
    {
        get => Preferences.Get("DefaultEndPressure", 234.4217);
        set => Preferences.Set("DefaultEndPressure", value);
    }

    public static double DefaultMaxOxygenPressure
    {
        get => Preferences.Get("DefaultEndPressure", 310.2641);
        set => Preferences.Set("DefaultEndPressure", value);
    }

}