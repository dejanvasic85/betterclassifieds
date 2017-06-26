# Getting Started #

### Development Requirements

- Windows 7 and higher with all IIS features installed and default website exists
- Visual Studio 2015 (ultimate preferred)
- SQL Server 2008 R2 or higher with Full text search feature enabled ( you may need Developer  )
- Ensure that SQL is running as the default instance (localhost)
- Windows service RpcLocator is running so you can avoid all the sql-powershell warnings


#### Building the Solution

- Open powershell as administrator
- Run .\build.ps1
- This should have built solution, created the databases, setup site with app pool and SSL, and setup your hosts file.
- Open http://betterclassifieds.local/ and it should render the site


### Deployment server requirements

When we deploy the application we generally require a single server with the following features:

- IIS 7.5+ feature enabled
- SQL Server with Full Text search
- .Net Framework 4.6.1+ installed
- A local identity 'ApplicationUser' exists and has read/write access to the database. This is used mostly for offline-tasks.
- Add ApplicationPool identity (once created in IIS) to SQL Server Security with access to the required databases.

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


## Conventions

### Automation Testing Tagging Elements

When we need to select elements in our automation tests
and they don't have and Id or a class name because they 
were not required for functional purposes, then we attach
a specific class prefixed with "tst". 

Example (ticket-selection.html):

``` 
<span class="ticket-info tst-ticket-free badge" data-bind="visible: price() === 0, text : 'Free'"></span>
```

This allows selenium web driver to select an element with class ```tst-ticket-free``` for any free ticket.


## Todo Function Test Coverage

- Event organiser management and access
- Promo codes and discounts
- Seating (when full solution is built)