rmdir Bin /s /q
mkdir Bin

rmdir Instal /s /q
mkdir Instal

rmdir Obfuscated /s /q
mkdir Obfuscated

cd ..\

xCOPY RealEstateDirectory\RealEstateDirectory\bin\Release Deploy\Bin /y /E /EXCLUDE:Deploy\restriction
COPY RealEstateDirectory\RealEstateDirectory\bin\Release\hibernate.cfg.xml Deploy\Bin\hibernate.cfg.xml 

cd Deploy

xCOPY Bin Obfuscated /y /E 

ILProtector\ILProtector.exe protector.ilproj  -nogui

cd Obfuscated

del *.bak

cd ..\
pause
