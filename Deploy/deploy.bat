rmdir Bin /s /q
mkdir Bin

rmdir Instal /s /q
mkdir Instal

rmdir Obfuscated /s /q
mkdir Obfuscated

cd ..\

xCOPY RealEstateDirectory\RealEstateDirectory\bin\Release Deploy\Bin /y /E /EXCLUDE:Deploy\Misc\restriction
COPY RealEstateDirectory\RealEstateDirectory\bin\Release\hibernate.cfg.xml Deploy\Bin\hibernate.cfg.xml 

cd Deploy

xCOPY Bin Obfuscated /y /E 

ILProtector\ILProtector.exe Misc\protector.ilproj  -nogui

cd Obfuscated

del *.bak

cd ..\

InnoSetup\compil32 /cc IstallerScript.iss

echo Ready
pause
