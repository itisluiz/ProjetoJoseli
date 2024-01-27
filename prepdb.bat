sqlcmd -S "DESKTOP-QPN2S96\SQLEXPRESS" -E -i ".\dropdb.sql"
del /F /Q ".\Migrations\*"
dotnet ef migrations add Inicial
dotnet ef database update
