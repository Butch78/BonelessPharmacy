# Start a backend dev server
Start-Process powershell {
    # $Host.UI.RawUI.WindowTitle = "Backend";
    Set-Location .\backend;
    dotnet watch run;
}
# Start a frontend dev server
Start-Process powershell {
    # $Host.UI.RawUI.WindowTitle = "Frontend";
    Set-Location .\frontend;
    tsc;
}

# Open VS Code in both folders
code .\frontend;
code .\backend;

# Kill this window
Stop-Process -Id $PID;