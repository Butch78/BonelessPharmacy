# Boneless Pharmacy

> Pharmacy Software for the Modern Era

## What is Boneless Pharmacy?

Boneless Pharmacy is a simple, portable pharmacy server solution for working with sales data,
especially in a pharmaceutical setting. Using Boneless Pharmacy, businesses are able to input,
read, edit and process retail data in an easy to understand, readable manner. Boneless Pharmacy
is made to work with your existing POS solution, taking only minutes to deploy. 

Out of the box, Boneless Pharmacy features

- A fully featured user interface for interacting with all parts of the solution
- Pre-configured employee account roles
- A secure, powerful, flexible set of API endpoints for interacting with the solution from any solution
- An easy to manage, file-based SQLite database system

[Further documentation can be found in our Wiki](https://github.com/Butch78/BonelessPharmacy/wiki)

## Getting Started

You'll need `nodejs` and `npm` to work with the frontend solution
and it's recommended you have `typescript` as well.

If you have chocolatey on Windows just run the following in an elevated shell;

```powershell
choco install nodejs -y
```

Linux can be a fickle beast with install nodejs so maybe look it up.

Once you have `nodejs` and `npm` you'll be able to run the following command (in an elevated shell) on any OS
to get typescript;

```powershell
npm install typescript -g
``` 

For the backend, install [.NET Core and the SDK](https://www.microsoft.com/net/core#windowscmd).

**Note that Boneless Pharmacy relies on ASP.Net Core 2.0, meaning that development at a minimum need .NET Core 2.0 installed**

## Building & Running the Solution (Frontend)

1. Run `npm install` in the root directory of the  frontend solution 
2. If you are using VSCode pressing *Ctrl + Shift + B* to run TypeScript's watcher compiler. If you are using the command line run : `tsc --watch`
3. Once this command has been run you should see a file called `bundle.js` in the `js` folder of your application. Running a server in the root directory will now show the home page if you go to `http://localhost/app`

## Building & Running the Solution (Backend)

1. Run `dotnet restore` in the root directory of the backend solution
2. Once restored, run `dotnet ef database update` to update the current state of the Entity Framework solution to the newest build
3. To run the solution in the built in dev server, just use `dotnet run`
    - Note that you can use `dotnet watch run` to have the code recompile on change rather than having to run the server again

## Running a Production Build (Backend)

Production builds are handled by a build system known as [Cake](https://cakebuild.net/). To edit the build script, a series of steps explaining what needs to be done 
for a build, just edit `backend/build.cake`. To build the solution using Cake just run the `build.ps1` script inside the backend folder.