$DeployPath = "\\10.0.0.109\WebViewable\Apps\NitroxCalculator"
$WindowsDeployPath = "$DeployPath\Windows"
$AndroidDeployPath = "$DeployPath\Android"

Write-Host "=========================================="
Write-Host " Building Windows Bundle (x64/x86/arm64)"
Write-Host "=========================================="
# This will build the .msixbundle because of our <RuntimeIdentifiers> in the csproj
dotnet publish NitroxCalculatorMaui.csproj -f net10.0-windows10.0.19041.0 -c Release

if ($LASTEXITCODE -ne 0) {
    Write-Error "Windows build failed."
    Pause
    exit $LASTEXITCODE
}

Write-Host "Locating Windows AppPackages folder..."
# Find the latest generated AppPackages Test folder
$appPackagesPath = "bin\Release\net10.0-windows10.0.19041.0\win-x64\AppPackages"
$latestFolder = Get-ChildItem -Path $appPackagesPath -Directory -Filter "*_Test" | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if (-not $latestFolder) {
    Write-Error "Could not find the generated AppPackages folder."
    Pause
    exit 1
}

$msixBundle = Get-ChildItem -Path $latestFolder.FullName -Filter "*.msixbundle" | Select-Object -First 1
if (-not $msixBundle) {
    $msixBundle = Get-ChildItem -Path $latestFolder.FullName -Filter "*.msix" | Select-Object -First 1
}

if (-not $msixBundle) {
    Write-Error "Could not find any .msix or .msixbundle file to deploy."
    Pause
    exit 1
}

$cert = Get-ChildItem -Path $latestFolder.FullName -Filter "*.cer" | Select-Object -First 1
$appInstaller = Get-ChildItem -Path $latestFolder.FullName -Filter "*.appinstaller" | Select-Object -First 1

Write-Host "Deploying Windows files to $WindowsDeployPath..."
if (-not (Test-Path $WindowsDeployPath)) { New-Item -ItemType Directory -Force -Path $WindowsDeployPath | Out-Null }

Copy-Item $msixBundle.FullName -Destination $WindowsDeployPath -Force
Copy-Item $cert.FullName -Destination $WindowsDeployPath -Force
if ($appInstaller) { Copy-Item $appInstaller.FullName -Destination $WindowsDeployPath -Force }

Write-Host "Generating Windows index.html..."
$bundleName = $msixBundle.Name
$certName = $cert.Name

$html = @"
<!DOCTYPE html>
<html>
<head>
    <title>Install Jrdiver's Nitrox Calculator</title>
    <style>
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; text-align: center; margin-top: 50px; background-color: #f3f3f4; color: #333; }
        .container { background-color: white; padding: 40px; border-radius: 10px; display: inline-block; box-shadow: 0 10px 20px rgba(0,0,0,0.1); max-width: 600px; width: 100%; }
        .btn { display: inline-block; padding: 15px 30px; font-size: 20px; color: white; background-color: #0078D7; text-decoration: none; border-radius: 5px; margin: 20px 0; font-weight: 600; transition: background-color 0.2s; }
        .btn:hover { background-color: #005a9e; }
        .cert-link { color: #d83b01; font-weight: bold; text-decoration: none; font-size: 18px; }
        .cert-link:hover { text-decoration: underline; }
        .instructions { text-align: left; background: #fff8f0; padding: 15px; border-left: 4px solid #d83b01; margin-top: 10px; border-radius: 4px; }
    </style>
</head>
<body>
    <div class="container">
        <h1 style="margin-bottom: 5px;">Jrdiver's Nitrox Calculator (Windows)</h1>
        
        <div style="margin-top: 40px;">
            <p style="font-size: 20px;"><strong>Step 1: Install the Security Certificate</strong></p>
            <p style="color: #666;">Because this app is self-published, Windows requires you to trust it first.</p>
            <a href="$certName" class="cert-link">Download Certificate File (.cer)</a>
            
            <div class="instructions">
                <strong>How to install:</strong><br>
                1. Double-click the downloaded .cer file.<br>
                2. Click <strong>Install Certificate...</strong><br>
                3. Choose <strong>Local Machine</strong> and click Next.<br>
                4. Select <strong>Place all certificates in the following store</strong> and click Browse.<br>
                5. Select <strong>Trusted People</strong> and click OK, then Finish.
            </div>
        </div>
        
        <hr style="margin: 40px 0; border: 0; border-top: 1px solid #eee;">
        
        <div>
            <p style="font-size: 20px;"><strong>Step 2: Install the App</strong></p>
            <p style="color: #666;">Once the certificate is in your Trusted People store, you can install the app.</p>
            <a href="$bundleName" class="btn">Download &amp; Install App</a>
        </div>
    </div>
</body>
</html>
"@

Set-Content -Path "$WindowsDeployPath\index.html" -Value $html

Write-Host ""
Write-Host "=========================================="
Write-Host " Building Android APK"
Write-Host "=========================================="
dotnet publish NitroxCalculatorMaui.csproj -f net10.0-android -c Release

if ($LASTEXITCODE -ne 0) {
    Write-Error "Android build failed."
    Pause
    exit $LASTEXITCODE
}

Write-Host "Deploying Android files to $AndroidDeployPath..."
if (-not (Test-Path $AndroidDeployPath)) { New-Item -ItemType Directory -Force -Path $AndroidDeployPath | Out-Null }

$apkPath = "bin\Release\net10.0-android\com.Jrdiver.NitroxCalculator-Signed.apk"
Copy-Item $apkPath -Destination $AndroidDeployPath -Force

Write-Host "Generating Android index.html..."
$apkName = Split-Path $apkPath -Leaf

$androidHtml = @"
<!DOCTYPE html>
<html>
<head>
    <title>Install Jrdiver's Nitrox Calculator</title>
    <style>
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; text-align: center; margin-top: 50px; background-color: #f3f3f4; color: #333; }
        .container { background-color: white; padding: 40px; border-radius: 10px; display: inline-block; box-shadow: 0 10px 20px rgba(0,0,0,0.1); max-width: 600px; width: 100%; }
        .btn { display: inline-block; padding: 15px 30px; font-size: 20px; color: white; background-color: #0078D7; text-decoration: none; border-radius: 5px; margin: 20px 0; font-weight: 600; transition: background-color 0.2s; }
        .btn:hover { background-color: #005a9e; }
        .instructions { text-align: left; background: #fff8f0; padding: 15px; border-left: 4px solid #d83b01; margin-top: 10px; border-radius: 4px; }
    </style>
</head>
<body>
    <div class="container">
        <h1 style="margin-bottom: 5px;">Jrdiver's Nitrox Calculator (Android)</h1>
        
        <div style="margin-top: 40px;">
            <p style="font-size: 20px;"><strong>Install on your phone</strong></p>
            <p style="color: #666;">Make sure you open this page on your Android device to install.</p>
            <a href="$apkName" class="btn">Download APK</a>
            
            <div class="instructions">
                <strong>How to install:</strong><br>
                1. Tap the Download button above.<br>
                2. When the download finishes, tap <strong>Open</strong>.<br>
                3. If prompted, click <strong>Settings</strong> and switch on <strong>Allow from this source</strong>.<br>
                4. Press Back, then tap <strong>Install</strong>.
            </div>
        </div>
    </div>
</body>
</html>
"@

Set-Content -Path "$AndroidDeployPath\index.html" -Value $androidHtml

Write-Host ""
Write-Host "=========================================="
Write-Host " Deployment Complete!"
Write-Host " Windows files deployed to: $WindowsDeployPath"
Write-Host " Android files deployed to: $AndroidDeployPath"
Write-Host "=========================================="
Pause
