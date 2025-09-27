dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package DotNetEnv

dotnet add package Swashbuckle
dotnet add package Swashbuckle.AspNetCore
dotnet add package Serilog.Sinks.File

dotnet new tool-manifest
dotnet tool install --local dotnet-ef
dotnet ef dbcontext
dotnet ef dbcontext scaffold <ConnectionString> <Provider> -o <FolderForClasses> -c <ContextClassName>
ConnectionString= Server=<IP_Address>,<Port>;Database=<Database>;User Id=<User>;Password=<Password>;TrustServerCertificate=True
*Remove the connection string from <OutputFolderName>/<ContextClass>.cs


dotnet ef migrations
dotnet ef migrations add Initial
dotnet ef migrations remove
dotnet ef database update
