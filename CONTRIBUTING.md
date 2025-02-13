# Contributing

## Setting up development environment
Setting up development environment for compilation:

1. Download [BepInExPack for Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/) and extract the contents of the BepInExPack_Valheim folder of the zip file into your root Valheim directory.
2. Create a new props file `Environment.props` alongside the solution file to define some properties local to your system. Paste this snippet and configure the paths as they are present on your computer.
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <!-- Needs to be your path to the base Valheim folder -->
    <VALHEIM_INSTALL>X:\SteamLibrary\steamapps\common\Valheim</VALHEIM_INSTALL>
  </PropertyGroup>
</Project>
```
3. Create a new file `JotunnLib.csproj.user` in the JotunnLib folder. Paste this snippet and configure the start arguments to your needs.
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Condition="'$(VALHEIM_INSTALL)' == ''" Project="..\Environment.props" />
  <PropertyGroup Condition="'$(Configuration)|$(Platform)' == 'Debug|AnyCPU' And '$(ProjectName)' == 'JotunnLib'">
    <StartAction>Program</StartAction>
    <!-- Start valheim.exe after building Debug-->
    <StartProgram>$(VALHEIM_INSTALL)\valheim.exe</StartProgram>
    <!-- If you want to connect to a server automatically, add '+connect <ip-address>:<port>' as StartArguments -->
    <StartArguments>+console</StartArguments>
    <!-- Alternatively run Steam.exe opening Valheim after building debug -->
    <!-- Needs to be your local path to the Steam client -->
    <!-- StartProgram>C:\Program Files %28x86%29\Steam\steam.exe</StartProgram -->
    <!-- StartArguments>-applaunch 892970</StartArguments -->
  </PropertyGroup>
</Project>
```

## Commiting, branching, and versioning
- For commiting, we adhere to [Convetional Commits](https://www.conventionalcommits.org/en/v1.0.0/#summary).
- For branching, we use [Git Flow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow).
- For versioning, we will be using [Semantic Versioning](https://semver.org/)

## Naming conventions, code style, and formatting
For this project, we use the same naming conventions as Microsoft's .NET framework. A [summary can be found here](https://github.com/ktaranov/naming-convention/blob/master/C%23%20Coding%20Standards%20and%20Naming%20Conventions.md).  

_Note: **ALL** public methods/properties/variables defined in public classes require XML documentation comments._  
  
Additionally, we enforce the following naming rules:
- Visibility of classes, methods, properties, and variables should always be specified, and should never be left blank (even if meant to be internal)
- Message methods (ex. `Awake`, `Update`) inherited from Unity's `Monobehaviour` should be internal or private
- Manager type class names must be suffixed with `Manager`
- Config type class names must be suffixed with `Config`
- Util type classes consisting of mainly static methods should be suffixed with either `Util` or `Helper`, and be placed in the `Utils` namespace
- All extension methods for classes should be in the `Extensions` namespace, and the class names should be suffixed with `Extension`

This is not strict, however, methods and methods defined in classes should ideally be defined in the following order:
- (public, internal, private) const variables
- (public, internal, private) static properties
- (public, internal, private) readonly variables
- (public, internal, private) variables
- (public, internal, static) methods
- (public, internal, static) methods