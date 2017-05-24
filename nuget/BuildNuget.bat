set PKG_VER=2017.1.9
nuget.exe pack -Prop Configuration=Release -Version %PKG_VER% "../ConfigDragon/ConfigDragon.nuspec"  -o  ../../NugetPackages
