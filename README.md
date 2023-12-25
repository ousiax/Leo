# Leo: A toy project for learning programming.
![](mainwindow.png)
![](mainwindow.zh-CN.png)

```console
$ tree -L 2 .
.
├── global.json
├── leo.sln
├── LICENSE
├── login.png
├── mainwindow.png
├── mainwindow.zh-CN.png
├── README.md
├── src
│   ├── Leo.Data.Domain
│   ├── Leo.UI.Services
│   ├── Leo.Web
│   ├── Leo.Web.Api
│   ├── Leo.Web.Data
│   ├── Leo.Web.Data.SQLite
│   ├── Leo.Web.Data.SqlServer
│   ├── Leo.Web.Host
│   ├── Leo.Windows.App
│   └── Leo.Wpf.App
└── test
    ├── Leo.Data.Domain.Tests
    ├── Leo.Web.Api.Tests
    └── Leo.Wpf.App.Tests

16 directories, 7 files
```

* `Leo.Windows.App` is a WinForms-based application.
* `Leo.Wpf.App` is a WPF-based application.
* `Leo.Web.Host` is a ASP.NET Core application.

NOTE: For simplify, the desktop application also launchs the Web API application in the same process.

NOTE: You can see more information about how to launch both the Desktop application and a backend Web API service application in one process at `Leo.Windows.Program.Main` and `Leo.Wpf.App.App`.

TIP: By default, the SQLite is used as the backend database. Here is the code snippet at the `Startup` class in the project `Leo.Web.Api`:

```cs
namespace Leo.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var dbEngine = Configuration.GetValue<string>("Database:Engine");
            if (string.Equals(dbEngine, "mssql", StringComparison.OrdinalIgnoreCase))
            {
                Leo.Web.Data.SqlServer.ServiceCollectionExtensions.AddDataServices(services);
            }
            else // "sqlite"
            {
                Leo.Web.Data.SQLite.ServiceCollectionExtensions.AddDataServices(services);
            }
```

The SQL Server migration scripts are located at `src/Leo.Web.Data.SqlServer/SQL/Scripts/`. You need to apply them manually.

When the application starts, it requires to login with a Microsoft account (an organization or personal account).

![](login.png)

After login succeeded, we will enter the main window. On the main window, we can create an new customer or click the `Find` menu at the toolbar to find a and load a existed customer.