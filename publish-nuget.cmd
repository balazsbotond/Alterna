@echo off

echo(
echo Creating version '%1'.
echo(
echo Please open Alterna.nuspec and update it manually.
echo(
pause

"C:\Program Files (x86)\MSBuild\14.0\bin\msbuild.exe" Alterna.sln /t:Build /p:Configuration=Release /p:TargetFramework=v4.5

echo Packing Alterna v%1...
nuget pack Alterna\Alterna.csproj -Prop Configuration=Release

echo(
echo Press any key to publish Alterna.%1.nupkg.
echo(
pause
nuget push Alterna.%1.nupkg -source nuget.org