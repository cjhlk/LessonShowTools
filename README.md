<h1 align="center">
  <a href="#">LessonShowTools 课程表小工具</a>
</h1>

## 关于本项目

基于CI二改的课程表小工具

LessonShowTools 是一款适用于班级多媒体屏幕的课表信息显示工具，可以在 Windows 屏幕上显示各种信息。

修改的内容：

- 移除了 Sentry 遥测数据收集
- 移除了插件市场
- 禁用了自动化
- 移除了更新功能
- 添加课程表强制顶置功能（能顶过顶置程序）
- 移除了天气API
- 默认隐藏组件，提醒设置（可以使用Uri调用）
- 移除所有对服务器发送数据的操作
- 隐藏了集控设置的入口（可以使用Uri调用）
- 移除了自动备份
- 移除HoYoStickers，改为使用AppIcon，移除回声洞
- 其他一些修改：Assets\Documents\ChangeLog.md和Privacy_.md，Assets\AsciiLogo.txt，AsciiLogo.txt，default-subjects.json等
- 移除了EdgeTTS
- 添加任务计划自动启动和Run注册表自动启动，移除原来通过在启动文件夹放快捷方式启动方式
- 优化Log记录
- 1.0.1.24061 添加5s检测终止CI进程功能

### 课表信息显示

- [x] 显示当天的课表、当前进行课程的信息
- [x] 在上下课等重要时间点发出
- [x] 自选课表隐藏条件、临时隐藏与鼠标穿透，不影响授课

### 课表编辑与管理

- [x] 简洁直观的课表编辑工具
- [x] 从 Excel 表格、[CSES](https://github.com/cses-org/cses) 或其他软件
- [x] 多周轮换、快速录入时间表、自定义设置
- [x] 单日/跨天临时调课
- [x] 提前预定要临时启用的课表

### 自定义

- [x] 通过主题系统高度定制应用主界面外观

### 其它功能

- [x] 自动同步软件时间、手动对齐铃声
- [x] 丝滑、流畅的过渡动画
- [x] 自动获取与系统配色搭配的主题色
- [ ] ……

## 开始使用

**请确保您的设备满足以下推荐需求：**

- Windows 10 及以上版本的系统，x64 架构
- 安装 [.NET 8.0 桌面运行时](https://dotnet.microsoft.com/zh-cn/download/dotnet/thank-you/runtime-desktop-8.0.7-windows-x64-installer)
