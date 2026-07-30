# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade NitroxCalculatorMaui\NitroxCalculatorMaui.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|
| NitroxAndTrimixCalculatorTests\NitroxAndTrimixCalculatorTests.csproj | Explicitly excluded         |
| NitroxCalculator\NitroxCalculator.csproj       | Explicitly excluded         |

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                        | Current Version | New Version | Description                         |
|:------------------------------------|:---------------:|:-----------:|:------------------------------------|
| Microsoft.Extensions.Logging.Debug  |   9.0.8         |  10.0.0-rc.1.25451.107 | Recommended for .NET 10.0           |
| Newtonsoft.Json                     |   13.0.3        |  13.0.4     | Recommended for .NET 10.0           |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### NitroxCalculatorMaui\NitroxCalculatorMaui.csproj modifications

Project properties changes:
  - Target frameworks should be changed from `net9.0-android;net9.0-ios;net9.0-maccatalyst;net9.0-windows10.0.19041.0` to `net10.0-android;net10.0-ios;net10.0-maccatalyst;net10.0-windows10.0.19041.0`

NuGet packages changes:
  - Microsoft.Extensions.Logging.Debug should be updated from `9.0.8` to `10.0.0-rc.1.25451.107` (*recommended for .NET 10.0*)
  - Newtonsoft.Json should be updated from `13.0.3` to `13.0.4` (*recommended for .NET 10.0*)

