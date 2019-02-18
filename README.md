# Screenshot Mockup Generator

C# Windows Application to create atractive screenshots using 4 elements:
- Background image
- Device frame (Android, iPhone, iPad, etc)
- Screenshot of your awesome game/app
- Text

<p align="center">
  <img src="Samples/out/sample01.png" width="400">
</p>

The app can be run from command line so you can create a .bat file with all the screenshots for every languaje, device type, etc


# Usage

# Command line

# Samples
```
w=1242 h=2208 o="..\..\..\Samples\out\test1.png" dfs="..\..\..\Assets\screens\sample1.jpg" tt="Play the classic\nGAME now!" dc=0.9 dp=0.5,0.35  bf="..\..\..\Assets\backs\back1.png" dfd="..\..\..\Assets\devices\generic_phone1.png" ds=Stretch tp=0.5,0.10 tc1=FFFFFFFF tc2=FF000000 ts=Outline tf="Cooper Black" tc=1.35```
<p align="left">
  <img src="docs/images/screen1.PNG" width="800">
</p>

```
w=1280 h=720 o="..\..\..\Samples\out\test2.png" dfs="..\..\..\Assets\screens\sample2.jpg" tt="The best game.\nPlay with friends" dc=0.9 dp=0.5,0.35  bf="..\..\..\Assets\backs\back2.png" dfd="..\..\..\Assets\devices\apple8_land.png" ds=Stretch tp=0.5,0.10 tc1=FFFFFFFF tc2=FF000000 ts=Outline tf="Cooper Black" tc=1.35```
<p align="left">
  <img src="docs/images/screen2.PNG" width="800">
</p>

```
w=1024 h=1366 o="..\..\..\Samples\out\test3.png" dfs="..\..\..\Assets\screens\sample3.png" tt="Text with accents\n¿Qué te parece?" dc=0.9 dp=0.5,0.35  bf="..\..\..\Assets\backs\back3.png" dfd="..\..\..\Assets\devices\generic_tablet1.png" ds=DrawTop tp=0.5,0.10 tc1=FF202020 tc2=80FFFFFF ts=Glow tf="Cooper Black" tc=1.35```
<p align="left">
  <img src="docs/images/screen3.PNG" width="800">
</p>

```
w=600 h=900 o="..\..\..\Samples\out\test4.png" dfd="..\..\..\Assets\screens\sample4.png" tt="Hello world!" dc=0.7 dp=0.5,0.35  bf="..\..\..\Assets\backs\back4.png" tp=0.5,0.10 tc1=FF202020 tc2=80202080 ts=Shadow tf="Arial" tc=1.35```
<p align="left">
  <img src="docs/images/screen4.PNG" width="800">
</p>
