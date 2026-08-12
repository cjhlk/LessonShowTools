param($token, $version)
$APP = "CJH/LessonShowTools"

echo "Uploading AppCenter..."
echo "Version is $version, APP is $APP"

Copy-Item ./out/LessonShowTools_app_windows_x64_full_singleFile.zip -Destination ./out/LessonShowTools.zip -Force
appcenter distribute release --group Collaborators --token $token -a $APP -f ./out/LessonShowTools.zip -b $version
