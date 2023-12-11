# Leo: A toy project for learning programming.
![](mainwindow.png)
![](mainwindow.zh-CN.png)

```txt
$ tree  -L 2
.
©À©¤©¤ global.json
©À©¤©¤ leo.sln
©À©¤©¤ LICENSE
©À©¤©¤ mainwindow.png
©À©¤©¤ mainwindow.zh-CN.png
©À©¤©¤ README.md
©À©¤©¤ src
©¦?? ©À©¤©¤ Leo.Data.Domain
©¦?? ©À©¤©¤ Leo.UI.Services
©¦?? ©À©¤©¤ Leo.Web
©¦?? ©À©¤©¤ Leo.Web.Api
©¦?? ©À©¤©¤ Leo.Web.Data
©¦?? ©À©¤©¤ Leo.Web.Data.SQLite
©¦?? ©À©¤©¤ Leo.Web.Host
©¦?? ©À©¤©¤ Leo.Windows.App
©¦?? ©¸©¤©¤ Leo.Wpf.App
©¸©¤©¤ test
    ©À©¤©¤ Leo.Data.Domain.Tests
    ©¸©¤©¤ Leo.Web.Api.Tests

14 directories, 6 files
```

* `Leo.Windows.App` is a WinForms-based application.
* `Leo.Wpf.App` is a WPF-based application.
* `Leo.Web.Host` is a ASP.NET Core application.

NOTE: For simplify, the desktop application also launchs the Web API application. You can see more information at `Leo.Windows.Program.Main` and `Leo.Wpf.App.App`.

When the application starts, it requires to login with a Microsoft account (an organization or personal account).

![](login.png)

After login succeeded, we will enter the main window. On the main window, we can create an new customer or click the `Find` menu at the toolbar to find a and load a existed customer.