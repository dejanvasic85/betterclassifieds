# Getting Started #

### Development Requirements

- Windows 7 and higher with all IIS features installed and default website exists
- Visual Studio 2015 (ultimate preferred)
- SQL Server 2008 R2 or higher with Full text search feature enabled ( you may need Developer  )
- Ensure that SQL is running as the default instance (localhost)


#### Building the Solution

- Run VS as administrator
- Open the solution. This should automatically create the websites fo you in IIS.
- Open the package manager console (nuget console)
- Run .\build.ps1

Running the powershell build should setup the databases automatically under localhost.


### Deployment server requirements

When we deploy the application we generally require a single server with the following features:
- IIS 7.5+ feature enabled
- SQL Server with Full Text search
- .Net Framework 4.6.1+ installed


## Post Release Activities


Whenever we do a release to production, we need to ensure the following is done


1. Pin the build in Team City so that the artefacts are not lost. This will be important just in case next time we need to do a roll-back through Octopus Deploy.

- Increment the major.minor version number in team city found [here](http://build.paramountit.com.au/admin/editBuildParams.html?id=buildType:bt2).

- Run the SQL Agent db backup jobs for all the brands that were deployed to and copy them to the development server. This allows consistent upgrades check for new featuers. 
> The backup location on the dev server is: S:\SQL\Backup



## Troubleshooting
Here's a list of problems that we may run in to that are not available on google/stackoverflow.

#### Registration object Version property cannot be mapped 

The Linq-2-Sql tables that have a "Timestamp" column property, have to have that
property mapped to a byte[] so that the automapper will work.



