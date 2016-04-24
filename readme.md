# Getting Started #

## Requirements ##

- Windows 7 and higher with all IIS features installed and default website exists
- Visual Studio 2013 (ultimate preferred)
- SQL Server 2008 R2 or higher with Full text search feature enabled ( you may need Developer  )
- Ensure that SQL is running as the default instance (localhost)
- Ensure Mongo DB is running on localhost and default port 27017. Read the mongo install instructions below. 

### Installing Mongo ###

We are now using MongoDb for storing the booking cart object. This seems to be 
a much better alternative to the session and using existing tables with a lot of 
foreign key constraints. 

To install, you can do so manually via the Mongo Website, or a neater solution 
would be to use chocolatey.

First, run the command to install chocolatey:

    @powershell -NoProfile -ExecutionPolicy unrestricted -Command "iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin

Now, you can use chocolatey to install many software packages available. Here's mongo:

    choco install mongodb

If you like to have a GUI for managing Mongo collections, RoboMongo is awesome tool:
 
    choco install RoboMongoUpda

## Building the Solution ##

- Run VS as administrator
- Open the solution. This should automatically create the websites fo you in IIS.
- Open the package manager console (nuget console)
- Run .\build.ps1

Running the powershell build should setup the databases automatically under localhost.

## Troubleshooting

#### Registration object Version property cannot be mapped 

The Linq-2-Sql tables that have a "Timestamp" column property, have to have that
property mapped to a byte[] so that the automapper will work.


# Post Release Activities #


Whenever we do a release to production, we need to ensure the following is done


1. Pin the build in Team City so that the artefacts are not lost. This will be important just in case next time we need to do a roll-back through Octopus Deploy.

- Increment the major.minor version number in team city found [here](http://build.paramountit.com.au/admin/editBuildParams.html?id=buildType:bt2).

- Run the SQL Agent db backup jobs for all the brands that were deployed to and copy them to the development server. This allows consistent upgrades check for new featuers. 
> The backup location on the dev server is: S:\SQL\Backup

