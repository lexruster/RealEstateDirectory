SET PGPASSFILE=C:\Users\Alexander\AppData\Roaming\postgresql\pgpass.conf

  For /f "tokens=1-3 delims=/." %%a in ('date /t') do (set mydate=%%a_%%b_%%c)
  set mydate=%mydate: =%

  set BACKUP_FILE=%mydate%_%TIME:~0,2%-%TIME:~3,2%.backup

"C:\Program Files\PostgreSQL\9.1\bin\pg_dump.exe" --host localhost --port 5432 --username postgres --format custom --blobs --no-password --file "D:\Document\BUISNES\Coding\RealEstateDirectory\Deploy\BackUp\RED_%BACKUP_FILE%" RealEstateDirectory