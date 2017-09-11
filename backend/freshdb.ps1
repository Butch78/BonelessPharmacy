Write-Output "Creating a fresh database..."
Remove-Item .\Main.db
dotnet ef database update