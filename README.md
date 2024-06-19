# DatabaseSecurity  

[![CodeFactor](https://www.codefactor.io/repository/github/matindewet/databasesecurity/badge)](https://www.codefactor.io/repository/github/matindewet/databasesecurity) 
[![NuGet Version](https://img.shields.io/nuget/v/MatinDeWet.DatabaseSecurity)](https://www.nuget.org/packages/MatinDeWet.DatabaseSecurity) 
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/MatinDeWet/DatabaseSecurity/dotnet.yml)](https://github.com/MatinDeWet/DatabaseSecurity)

Database Security was designed to be used as a Database data security project where a developer is able to effectively secure their data in an repository style of architecture.

## Setup and installation
DatabaseSecurity should be used as a library that can be imported into any C# project making use of Entity Framework Core and Microsoft Identity.

### Registering the library
To make use of the DatabaseSecurity library you need to register the library in the main project program file.
```C#
    services.RegisterDatabaseSecurity();
```

### Creating a custom UnitOfWork
To be able to save any data you will need to implement a custom class that will inheret from the IUnitOfWork interface.

```C#
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestContext _ctx;

        public UnitOfWork(TestContext ctx)
        {
            _ctx = ctx;
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _ctx.SaveChangesAsync(cancellationToken);
        }
    }
```

In this sample we created a simple UnitOfWork class that inherits from the IUnitOfWork interface.
You can expand this class to preform auditing, or preform any (pre or post) save opperations.

You will need to register this implementation in the main project program file.
For the above code snippet we can simply register it as follows:

```C#
    services.AddScoped<IUnitOfWork, UnitOfWork>();
```

### Creating a custom Repository
To be able to effectively make use of this library you will need to create a custom repository with a interface that will inherit from the IRepository interface.

```C#
	public interface IExampleRepository : IRepository
	{
	}
```
```C#
	public class ExampleRepository : LockedRepository<TestContext>, IExampleRepository
	{
		public ExampleRepository(TestContext ctx) : base(ctx)
		{
		}
	}
```

Within the repository you will need to inherit from the LockedRepository class where it takes in your context as a generic type.
You will need to register this repository in the main project program file.

```C#
    services.AddScoped<IExampleRepository, ExampleRepository>();
```

### Securing your data
To secure your data you will need to create locks on your data.
Currently the library makes use of a locking mechanism where there are t main functions: ClientLock /  UserLock. 
There are two methods that need to be implemented in the lock classes: (Secured and HasAccess)

- Secured is used when reading data
- HasAccess is used when saving data

They can be implemented as follows:

```C#
    public class ClientLock : Lock<Client>
    {
        private readonly TestContext _context;

        public ClientLock(TestContext context)
        {
            _context = context;
        }

        public override IQueryable<Client> Secured(int identityId, DataPermissionEnum requirement)
        {
            var qry = from c in _context.Clients
                      join ut in _context.UserTeams on c.TeamId equals ut.TeamId
                      where ut.UserId == identityId
                      && ut.DataRight.HasFlag(requirement)
                      select c;

            return qry;
        }

        public override async Task<bool> HasAccess(Client obj, int identityId, DataPermissionEnum requirement, CancellationToken cancellationToken)
        {
            var teamId = obj.TeamId;

            if (teamId == 0)
            {
                return false;
            }

            var query = from ut in _context.UserTeams
                        where ut.UserId == identityId
                        && ut.DataRight.HasFlag(requirement)
                        && ut.TeamId == teamId
                        select ut.TeamId;

            return await query.AnyAsync(cancellationToken);
        }
    }
```

You will also need to register the locks.

```C#
    services.AddScoped<IProtected, ClientLock>();
```

More detailed examples can be found in DatabaseSecurity.UnitTests project in the Locks folder.


### The Middleware
For the Library to work you will need to supply the library with the current identity. Specifically with a subjectid(sub) claim

The IdentityConstants Id will be used in the lock.
```C#
    new Claim("sub", "1")
```

You will need to pass the user claims through to the IInfoSetter interface and call the SetUser to pass the user claims through, this data is required data to the library.