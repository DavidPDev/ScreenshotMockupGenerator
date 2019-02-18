
rem Copy this .bat file to ScreenshotMockupGenerator\bin\Debug or change the paths

rem NOTE1: IMPORTANT! use "chcp 1252" for special characters (for example: Spanish, portuguese, etc)
rem NOTE2: The .bat file MUST be saved as ANSI
chcp 1252

ScreenshotMockupGenerator.exe w=1242 h=2208 o="..\..\..\Samples\out.bat\test1.png" dfs="..\..\..\Assets\screens\sample1.jpg" tt="Play the classic\nGAME now!" dc=0.9 dp=0.5,0.35  bf="..\..\..\Assets\backs\back1.png" dfd="..\..\..\Assets\devices\generic_phone1.png" ds=Stretch tp=0.5,0.10 tc1=FFFFFFFF tc2=FF000000 ts=Outline tf="Cooper Black" tc=1.35
ScreenshotMockupGenerator.exe w=1280 h=720 o="..\..\..\Samples\out.bat\test2.png" dfs="..\..\..\Assets\screens\sample2.jpg" tt="The best game.\nPlay with friends" dc=0.9 dp=0.5,0.35  bf="..\..\..\Assets\backs\back2.png" dfd="..\..\..\Assets\devices\apple8_land.png" ds=Stretch tp=0.5,0.10 tc1=FFFFFFFF tc2=FF000000 ts=Outline tf="Cooper Black" tc=1.35
ScreenshotMockupGenerator.exe w=1024 h=1366 o="..\..\..\Samples\out.bat\test3.png" dfs="..\..\..\Assets\screens\sample3.png" tt="Text with accents\n�Qu� te parece?" dc=0.9 dp=0.5,0.35  bf="..\..\..\Assets\backs\back3.png" dfd="..\..\..\Assets\devices\generic_tablet1.png" ds=DrawTop tp=0.5,0.10 tc1=FF202020 tc2=80FFFFFF ts=Glow tf="Cooper Black" tc=1.35
ScreenshotMockupGenerator.exe w=600 h=900 o="..\..\..\Samples\out.bat\test4.png" dfd="..\..\..\Assets\screens\sample4.png" tt="Hello world!" dc=0.7 dp=0.5,0.35  bf="..\..\..\Assets\backs\back4.png" tp=0.5,0.10 tc1=FF202020 tc2=80202080 ts=Shadow tf="Arial" tc=1.35

pause