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
# "--disable-gpu" 
# becase of the GPU issue with electorn :/
code .\frontend --disable-gpu;
code .\backend --disable-gpu;

# Kill this window
Stop-Process -Id $PID;