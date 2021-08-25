dotnet publish -c release -r win10-x64 --self-contained=false /p:PublishSingleFile=True -o "Output"

$hasPath = Test-Path "Output/A-SOUL点灯游戏.exe"
if ($hasPath) {
    Move-Item -Force "Output/A-SOUL点灯游戏.exe" "Output/A-SOUL点灯游戏.exe.old"
}
Copy-Item "Output/ASGame_FlipGame.exe" "Output/A-SOUL点灯游戏.exe"