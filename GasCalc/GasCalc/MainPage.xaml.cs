using Microsoft.Maui.Controls;
using System;

namespace GasCalc
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private int count;
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            CounterLabel.Text = $"Current count: {count}";
        }
    }
}
