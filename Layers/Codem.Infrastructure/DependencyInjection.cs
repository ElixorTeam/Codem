using Codem.Infrastructure.Common;
using Codem.Infrastructure.Models;
using Codem.Infrastructure.Uow;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using UOW.Abstractions;
using Сodem.Shared.Utils;

namespace Codem.Infrastructure;

public static class DependencyInjection
{
    public static void AddNhibernate(this IServiceCollection services)
    {
        SqlSettings sqlSettings = LoadJsonConfig();
        Configuration configuration = LoadSqlConfig(sqlSettings);   
        
        configuration.AddAssembly(typeof(SqlEntity).Assembly);
        
        SqlSetupUtil.LoadMappings(configuration);
        SqlSetupUtil.UpdateScheme(configuration);
        SqlSetupUtil.SetupRepositories(services);
        
        ISessionFactory sessionFactory = configuration.BuildSessionFactory();
        services.AddScoped<ISession>(_ => sessionFactory.OpenSession());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    
    #region Private

    private static SqlSettings LoadJsonConfig()
    {
        IConfigurationRoot sqlConfiguration = new ConfigurationBuilder()
            .AddJsonFile("sqlconfig.json", optional: false, reloadOnChange: false)
            .Build();
        
        SqlSettings sqlSettings = new();
        sqlConfiguration.GetSection("SqlSettings").Bind(sqlSettings);
        return sqlSettings;
    }
    private static Configuration LoadSqlConfig(SqlSettings settings)
    {
        Configuration configuration = new();
        
        configuration.DataBaseIntegration(db =>
        {
            db.ConnectionString = settings.GetConnectionString();
            db.Dialect<MsSql2012Dialect>();
            db.Driver<SqlClientDriver>();
            db.LogSqlInConsole = BuildUtil.IsDevelop;
        });
        return configuration;
    }
    
    #endregion
}