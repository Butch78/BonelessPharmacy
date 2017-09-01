# Boneless Pharmacy

> Pharmacy Software for the Modern Era

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
    