# CheckNetVersions

Scans the current folder and all subfolders for .dll and .exe files. For each file found that uses the .NET Framework (not .NET Core or .NET 5.0 and above), it generates a line containing the full path and the required version. The collected entries are saved into the file *FrameworkReport.csv*.

I added a deployment profile to publish a self-contained executable for Windows x64.