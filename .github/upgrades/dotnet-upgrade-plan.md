# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade NitroxCalculatorMaui\NitroxCalculatorMaui.csproj
4. Upgrade NitroxCalculator\NitroxCalculator.csproj
5. Upgrade NitroxAndTrimixCalculatorTests\NitroxAndTrimixCalculatorTests.csproj

## Settings

### Excluded projects

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|
| NitroxandTrimixCalculatorLibrary\NitroxAndTrimixCalculatorLibrary.csproj | No target framework change required |
| Nitrox Calculator Installer\Nitrox Calculator Installer.vdproj | Not a .NET project |
| Installers\NitroxCalcMauiInstall\NitroxCalcMauiInstall.wapproj | Not a .NET project |

### Aggregate NuGet packages modifications across all projects

| Package Name                        | Current Version | New Version | Description                         |
|:------------------------------------|:---------------:|:-----------:|:------------------------------------|
| Microsoft.Extensions.Logging.Debug  |   9.0.8         |  10.0.0-rc.1.25451.107 | Recommended for .NET 10.0           |
| Newtonsoft.Json                     |   13.0.3        |  13.0.4     | Recommended for .NET 10.0           |

### Project upgrade details

#### NitroxCalculatorMaui\NitroxCalculatorMaui.csproj modifications

Project properties changes:
  - Target frameworks should be changed from `net9.0-android;net9.0-ios;net9.0-maccatalyst;net9.0-windows10.0.19041.0` to `net10.0-android;net10.0-ios;net10.0-maccatalyst;net10.0-windows10.0.19041.0`

NuGet packages changes:
  - Microsoft.Extensions.Logging.Debug should be updated from `9.0.8` to `10.0.0-rc.1.25451.107`
  - Newtonsoft.Json should be updated from `13.0.3` to `13.0.4`

Other changes:
  - Add NuGet package source mappings as recommended in NuGet.config

#### NitroxCalculator\NitroxCalculator.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0-windows` to `net10.0-windows`

Other changes:
  - Add NuGet package source mappings as recommended in NuGet.config

#### NitroxAndTrimixCalculatorTests\NitroxAndTrimixCalculatorTests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

Other changes:
  - Add NuGet package source mappings as recommended in NuGet.config

#### NitroxandTrimixCalculatorLibrary\NitroxAndTrimixCalculatorLibrary.csproj modifications

Other changes:
  - Add NuGet package source mappings as recommended in NuGet.config
