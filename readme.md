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

