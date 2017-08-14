# Boneless Pharmacy

> Pharmacy Software for the Modern Era

## Getting Started

You'll need `nodejs` and `npm` to work with this solution
and it's recommended you have `typescript` as well.

If ya have chocolatey on Windows just run the following in an elevated shell;

```powershell
choco install nodejs -y
```

Linux can be a fickle beast with install nodejs so maybe look it up.

Once you have `nodejs` and `npm` you'll be able to run the following command (in an elevated shell) on any OS
to get typescript;

```powershell
npm install typescript -g
``` 

## Building & Running the Solution

1. Run `npm install` in the root directory of the solution 
2. If you are using VSCode pressing *Ctrl + Shift + B* to run TypeScript's watcher compiler. If you are using the command line run : `tsc --watch`
3. Once this command has been run you should see a file called `bundle.js` in the `js` folder of your application. Running a server in the root directory will now show the home page if you go to `http://localhost/app`