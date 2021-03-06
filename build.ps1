# dotnet sonarscanner begin /k:"NickHarvey2_Graph" /d:sonar.organization="nickharvey2-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="****" /d:sonar.cs.vstest.reportsPaths=./Graph.Test/TestResults/*.trx /d:sonar.cs.vscoveragexml.reportsPaths=**/*.coveragexml /d:sonar.exclusions="**/obj/**/*" /v:1.0.0

# Remove-Item -r .\Graph.Test\TestResults\
# dotnet clean
# dotnet build
# dotnet test --collect:"Code Coverage" --logger trx --results-directory ./TestResults/

Write-Host "Converting binary Test Result file to XML"
$coveragePath = (Get-ChildItem -Recurse -Path ./Graph.Test/TestResults -Name "*.coverage")[0]
Write-Host "binary file found: $coveragePath"
$coverageXmlFile = $coveragePath + "xml"
Write-Host "XML file location: $coverageXmlFile"
$vsInstallationPath = (&"${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere" | Where-Object {$_ -like 'installationPath: *'}).Replace("installationPath: ", "")

& "$vsInstallationPath\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" analyze /output:".\Graph.Test\TestResults\$coverageXmlFile" ".\Graph.Test\TestResults\$coveragePath"

# dotnet sonarscanner end /d:sonar.login="****"