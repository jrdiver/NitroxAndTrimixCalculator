using System;
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

        private void SliderEndingPercentage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = SliderEndingPercentage.Value;
            TextEndPercent.Text = Math.Round(value).ToString(CultureInfo.InvariantCulture);
        }

        private void SliderStartingPercentage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = SliderStartingPercentage.Value;
            TextStartPercent.Text = Math.Round(value).ToString(CultureInfo.InvariantCulture);
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
        }
        #endregion

        private void TextPressure_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}