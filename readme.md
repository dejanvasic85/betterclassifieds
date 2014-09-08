## Getting Started ##

### Requirements ###

- Windows 7 and higher with all IIS features installed and default website exists
- Visual Studio 2013 (ultimate preferred)
- SQL Server 2008 R2 or higher with Full text search feature enabled ( you may need Developer  )
- Ensure that SQL is running as the default instance (localhost)

### Building the Solution ###

- Run VS as administrator
- Open the solution. This should automatically create the websites fo you in IIS.
- Open the package manager console (nuget console)
- Run .\build.ps1

Running the powershell build should setup the databases automatically under localhost.
