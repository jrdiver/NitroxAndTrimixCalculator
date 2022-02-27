using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxCalculator.Windows;

/// <summary> Interaction logic for MainWindow.xaml </summary>
public partial class MainWindow
{
    private readonly NitroxMixCalculator calculator = new();

    public MainWindow()
    {
        InitializeComponent();

    }

    private void WinLoaded(object sender, RoutedEventArgs e)
    {
        calculator.LoadUnit("Imperial");
        LoadUnits(true);

        SliderStartingPercentage.Value = 32;
        TextStartPercent.Text = SliderStartingPercentage.Value.ToString(CultureInfo.InvariantCulture);
        SliderEndingPercentage.Value = 32;
        TextEndPercent.Text = SliderEndingPercentage.Value.ToString(CultureInfo.InvariantCulture);

        MessageBoxResult result = MessageBox.Show("This software may generate incorrect results and should only be followed if you are trained in mixing gasses.  The Author of this software is not liable for any damages or injuries related to the use of this software and the calculations it made." +
                                                  Environment.NewLine + Environment.NewLine + "ALL RESULTS ARE FOR REFERENCE ONLY AND USE AT YOUR OWN RISK" + Environment.NewLine + Environment.NewLine + "Click yes if you agree to these conditions, No to close the Application", "Nitrox Calculator", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);

        if (result == MessageBoxResult.No)
        {
            Exit();
        }
    }

    #region AppFunctions
    internal void MenuItemExit(object sender, RoutedEventArgs e)
    {
        Exit();
    }

    internal void Exit()
    {
        Close();
        Application.Current.Shutdown();
    }
    #endregion

    #region Unit Selection
    internal void LoadUnits(bool initialLoad = false)
    {
        if (initialLoad)
        {
            ConvertDisplayUnit(Properties.Settings.Default.Unit);
        }

        MenuUnits.Items.Clear();
        foreach (Unit unit in calculator.UnitList)
        {
            MenuItem newUnit = new()
            {
                Name = unit.Name,
                Header = unit.Name
            };
            newUnit.Click += SelectUnit;
            if (unit.Name == calculator.SelectedUnit.Name)
            {
                newUnit.IsChecked = true;
            }

            MenuUnits.Items.Add(newUnit);
        }
        CalculateMix();
    }

    internal void SelectUnit(object sender, RoutedEventArgs routedEventArgs)
    {
        string unit = string.Empty;
        if (routedEventArgs.Source is MenuItem mi)
            unit = mi.Name;

        Properties.Settings.Default.Unit = unit;
        Properties.Settings.Default.Save();
        ConvertDisplayUnit(unit);
        calculator.LoadUnit(unit);
        LoadUnits();


    }

    internal void ConvertDisplayUnit(string newUnit)
    {
        double.TryParse(TextEndPressure.Text, out double endPressure);
        calculator.SelectedUnit.Pressure = endPressure;
        endPressure = calculator.SelectedUnit.PressureBar;

        double.TryParse(TextStartPressure.Text, out double startPressure);
        calculator.SelectedUnit.Pressure = startPressure;
        startPressure = calculator.SelectedUnit.PressureBar;

        double.TryParse(TextMaxOxygenPressure.Text, out double oxygenPressure);
        calculator.SelectedUnit.Pressure = oxygenPressure;
        oxygenPressure = calculator.SelectedUnit.PressureBar;

        calculator.LoadUnit(newUnit);

        calculator.SelectedUnit.PressureBar = endPressure;
        TextEndPressure.Text = calculator.SelectedUnit.Pressure.ToString(CultureInfo.InvariantCulture);

        calculator.SelectedUnit.PressureBar = startPressure;
        TextStartPressure.Text = calculator.SelectedUnit.Pressure.ToString(CultureInfo.InvariantCulture);

        calculator.SelectedUnit.PressureBar = oxygenPressure;
        TextMaxOxygenPressure.Text = calculator.SelectedUnit.Pressure.ToString(CultureInfo.InvariantCulture);

        LabelEndPressure.Content = calculator.SelectedUnit.PressureName;
        LabelStartPressure.Content = calculator.SelectedUnit.PressureName;
        LabelOxygenPressure.Content = calculator.SelectedUnit.PressureName;
    }
    #endregion

    private void SliderEndingPercentage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        double value = SliderEndingPercentage.Value;
        TextEndPercent.Text = Math.Round(value).ToString(CultureInfo.InvariantCulture);
        CalculateMix();
    }

    private void SliderStartingPercentage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        double value = SliderStartingPercentage.Value;
        TextStartPercent.Text = Math.Round(value).ToString(CultureInfo.InvariantCulture);
        CalculateMix();
    }

    private void TextStartPercent_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (double.TryParse(TextStartPercent.Text, out double value))
        {
            if (value < 0)
            {
                TextStartPercent.Text = "0";
                TextStartPercent.SelectAll();
                value = 0;
            }
            else if (value > 100)
            {
                TextStartPercent.Text = "100";
                TextStartPercent.SelectAll();
                value = 100;
            }
            SliderStartingPercentage.Value = value;
        }
        else
        {
            TextStartPercent.Text = "0";
            TextStartPercent.SelectAll();
        }
        CalculateMix();
    }

    private void TextEndPercent_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (double.TryParse(TextEndPercent.Text, out double value))
        {
            if (value < 0)
            {
                TextEndPercent.Text = "0";
                TextEndPercent.SelectAll();
                value = 0;
            }
            else if (value > 100)
            {
                TextEndPercent.Text = "100";
                TextEndPercent.SelectAll();
                value = 100;
            }
            SliderEndingPercentage.Value = value;
        }
        else
        {
            TextEndPercent.Text = "0";
            TextEndPercent.SelectAll();
        }
        CalculateMix();
    }

    private void TextPressure_TextChanged(object sender, TextChangedEventArgs e)
    {
        CalculateMix();
    }

    private void CalculateMix()
    {
        if (Mix1 == null) return;

        Mix1.Content = string.Empty;
        Mix2.Content = string.Empty;
        Mix3.Content = string.Empty;
        Mix4.Content = string.Empty;
        Mod1.Content = string.Empty;
        Mod2.Content = string.Empty;
        Mod3.Content = string.Empty;
        Mod4.Content = string.Empty;

        double.TryParse(TextEndPressure.Text, out double endPressure);
        double.TryParse(TextStartPressure.Text, out double startPressure);
        double.TryParse(TextMaxOxygenPressure.Text, out double oxygenPressure);
        double.TryParse(TextStartPercent.Text, out double startMix);
        double.TryParse(TextEndPercent.Text, out double endMix);
        MixInputs input = new()
        {
            EndPressure = endPressure,
            MaxOxygenPressure = oxygenPressure,
            StartPressure = startPressure,
            StartMix = startMix,
            EndMix = endMix,
            TopOffMix = 21,
            EnableMaxOxygenPressure = true
        };

        MixResult result = calculator.CalculateMix(input);
        double pressure = Math.Round(result.Inputs.StartPressure);

        if (result.RemoveGas > 0)
        {
            pressure -= Math.Round(result.RemoveGas);
            Mix1.Content = "Drain " + Math.Round(result.RemoveGas) + " " + calculator.SelectedUnit.PressureName + " from the tank to " + pressure + " " + calculator.SelectedUnit.PressureName;
        }
        if (result.AddOxygen > 0)
        {
            pressure += Math.Round(result.AddOxygen);
            Mix2.Content = "Add " + Math.Round(result.AddOxygen) + " " + calculator.SelectedUnit.PressureName + " of Oxygen to " + pressure + " " + calculator.SelectedUnit.PressureName;
        }
        if (result.AddTopOffGas() > 0)
        {
            Mix3.Content = "Add " + Math.Round(result.AddTopOffGas()) + " " + calculator.SelectedUnit.PressureName + " of " + result.Inputs.TopOffMix + "% to " + result.Inputs.EndPressure + " " + calculator.SelectedUnit.PressureName;
        }

        if (result.ValidMix())
        {
            Mix1.Content = "Not Possible with selected Inputs";
            Mix2.Content = string.Empty;
            Mix3.Content = string.Empty;
            Mix4.Content = string.Empty;
        }
        else
        {
            Mod1.Content = "Max Depth at 1.3: " + calculator.MaxOperatingDepthCalculator(result.Inputs.EndMix, 1.3) + " " + result.SelectedUnit.DepthName;
            Mod2.Content = "Max Depth at 1.4: " + calculator.MaxOperatingDepthCalculator(result.Inputs.EndMix, 1.4) + " " + result.SelectedUnit.DepthName;
            Mod3.Content = "Max Depth at 1.5: " + calculator.MaxOperatingDepthCalculator(result.Inputs.EndMix, 1.5) + " " + result.SelectedUnit.DepthName;
            Mod4.Content = "Max Depth at 1.6: " + calculator.MaxOperatingDepthCalculator(result.Inputs.EndMix, 1.6) + " " + result.SelectedUnit.DepthName;
        }
    }
}