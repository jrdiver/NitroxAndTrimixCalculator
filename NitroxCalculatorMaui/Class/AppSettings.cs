using NitroxAndTrimixCalculatorLibrary;

namespace NitroxCalculatorMaui.Class;

internal static class AppSettings
{
    public static string SelectedUnit
    {
        get => Preferences.Get("DefaultUnit", "Imperial");
        set => Preferences.Set("DefaultUnit", value);
    }
}