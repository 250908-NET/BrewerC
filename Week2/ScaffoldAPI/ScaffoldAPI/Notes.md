dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package DotNetEnv

dotnet tool install --local dotnet-ef
dotnet ef dbcontext
dotnet ef dbcontext scaffold <Connection> <Provider>
dotnet ef dbcontext scaffold "Server=135.148.150.144,35430;Database=Chinook;User Id=sa;Password=iuAsdhiouhqw@iHoeuh109823n9as8xyuinj109198234yn!baDsdf;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o ./Models -c ChinookContext
*Remove the connection string from Models/ChinookContext.cs



