# Only use this script if you want a fresh database or there has been
# serious changes to the DB Scheme, this will delete your database
Write-Output "Creating a fresh database..."
Remove-Item .\Main.db
dotnet ef database update