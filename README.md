# 简介

A-SOUL主题化的点灯小游戏



# 音频添加方法

在游戏同目录下创建一个名为ASSounds的文件夹（或者，如果你想使用其他文件名作为音频文件夹，则可以在游戏启动参数中加入**-sound 你的文件夹名**即可），接下来将你想要使用的音频文件移动到该文件夹下，并按下述指定命名规则命名MP3音频文件即可

1：以**flip**开头的mp3音乐将作为点击音效，若有多个flip开头的音频，则单击时随机播放

2：以**music**开头的mp3音乐将作为游戏背景音乐，若有多个music开头的音频，则启动时随机播放



# 启动参数

* -row n 设置初始行数为n

* -col n 设置初始列数为n

* -mine n 设置初始“寄”数位n

* -sound n 载入文件夹名为n中的音效

  

# 编译/生成说明

安装.Net 5.0 SDK后，运行目录下的make.ps1脚本即可

运行完成后**Output**文件夹下的**A-SOUL点灯游戏.exe**即为输出结果

.Net 5.0 SDK下载地址：[Download .NET 5.0 (Linux, macOS, and Windows) (microsoft.com)](https://dotnet.microsoft.com/download/dotnet/5.0)



-----



ps：目前还在迭代修改优化中（虽然进度是甚至连注释都还没补全）

pps：其他的说明之后再写（bushi