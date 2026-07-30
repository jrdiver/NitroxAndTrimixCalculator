using System.Collections.Generic;

namespace NitroxCalculatorUI.Services;

public interface IAppSettings
{
    void DefaultAll();

    string SelectedUnit { get; set; }
    void DefaultSelectedUnit();

    string ColorMode { get; set; }
    void DefaultColorMode();

    double StartPressure { get; set; }
    void DefaultStartPressure();

    double EndPressure { get; set; }
    void DefaultEndPressure();

    double MaxOxygenPressure { get; set; }
    void DefaultMaxOxygenPressure();

    double StartMix { get; set; }
    void DefaultStartMix();

    double EndMix { get; set; }
    void DefaultEndMix();

    List<double> P02List { get; set; }
    void DefaultP02List();

    double EqualizationTankSize { get; set; }
    void DefaultTankSize();

    double EqualizationTankFullPressure { get; set; }
    void DefaultEqualizationTankFullPressure();

    double EqualizationTank1Pressure { get; set; }
    void DefaultEqualizationTank1Pressure();

    double EqualizationTank2Pressure { get; set; }
    void DefaultEqualizationTank2Pressure();
}
