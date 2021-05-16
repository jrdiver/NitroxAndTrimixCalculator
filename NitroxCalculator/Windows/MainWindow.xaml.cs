﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using NitroxAndTrimixCalculatorLibrary;
using NitroxAndTrimixCalculatorLibrary.Object;

namespace NitroxCalculator.Windows
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow
    {
        private MixCalculator calculator = new();

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
            calculator.SelectedUnit.SetPressure(endPressure);
            endPressure = calculator.SelectedUnit.PressureBar;


            double.TryParse(TextStartPressure.Text, out double startPressure);
            calculator.SelectedUnit.SetPressure(startPressure);
            startPressure = calculator.SelectedUnit.PressureBar;

            calculator.LoadUnit(newUnit);

            calculator.SelectedUnit.SetPressureInBars(endPressure);
            TextEndPressure.Text = calculator.SelectedUnit.GetPressure().ToString();

            calculator.SelectedUnit.SetPressureInBars(startPressure);
            TextStartPressure.Text = calculator.SelectedUnit.GetPressure().ToString();

            LabelEndPressure.Content = calculator.SelectedUnit.PressureName;
            LabelStartPressure.Content = calculator.SelectedUnit.PressureName;
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

        private void TextStartPercent_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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


        private void TextPressure_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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

            MixInputs input = new();
            double.TryParse(TextEndPressure.Text, out double endPressure);
            double.TryParse(TextStartPressure.Text, out double startPressure);
            double.TryParse(TextStartPercent.Text, out double startMix);
            double.TryParse(TextEndPercent.Text, out double endMix);
            input.SetEndPressure(endPressure);
            input.SetStartPressure(startPressure);
            input.SetStartMix(startMix);
            input.SetEndMix(endMix);

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
        }
    }
}