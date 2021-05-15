using System;
using System.Globalization;
using System.Windows;

namespace NitroxCalculator
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow
    {
        internal bool MetricMode;

        public MainWindow()
        {
            InitializeComponent();
            LoadUnitFromSettings();

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
        internal void LoadUnitFromSettings()
        {
            if (Properties.Settings.Default.MetricMode)
            {
                SelectUnitsMetric();
            }
            else
            {
                SelectUnitsImperial();
            }
        }

        internal void ButtonSelectUnitsImperial(object sender, RoutedEventArgs routedEventArgs)
        {
            SelectUnitsImperial();
        }
        internal void ButtonSelectUnitsMetric(object sender, RoutedEventArgs routedEventArgs)
        {
            SelectUnitsMetric();
        }

        internal void SelectUnitsImperial()
        {
            if (MetricMode)
            {
                double.TryParse(TextEndPressure.Text, out double pressure);
                pressure *= 14.5037738;
                TextEndPressure.Text = pressure.ToString("0");

                double.TryParse(TextStartPressure.Text, out pressure);
                pressure *= 14.5037738;
                TextStartPressure.Text = pressure.ToString("0");
            }

            MenuItemImperial.IsChecked = true;
            MenuItemMetric.IsChecked = false;
            MetricMode = false;
            LabelEndPressure.Content = "PSI";
            LabelStartPressure.Content = "PSI";
            Properties.Settings.Default.MetricMode = false;
            Properties.Settings.Default.Save();
        }

        internal void SelectUnitsMetric()
        {
            if (MetricMode == false)
            {
                double.TryParse(TextEndPressure.Text, out double pressure);
                pressure /= 14.5037738;
                TextEndPressure.Text = pressure.ToString("0.#");

                double.TryParse(TextStartPressure.Text, out pressure);
                pressure /= 14.5037738;
                TextStartPressure.Text = pressure.ToString("0.#");
            }
            MenuItemImperial.IsChecked = false;
            MenuItemMetric.IsChecked = true;
            MetricMode = true;
            LabelEndPressure.Content = "Bar";
            LabelStartPressure.Content = "Bar";
            Properties.Settings.Default.MetricMode = true;
            Properties.Settings.Default.Save();
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
            if(double.TryParse(TextStartPercent.Text, out double value))
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