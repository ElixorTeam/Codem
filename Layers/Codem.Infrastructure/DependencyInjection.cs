using Codem.Infrastructure.Listeners;
using Codem.Infrastructure.Models;
using Codem.Infrastructure.Uow;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Event;
using UOW.Abstractions;
using Сodem.Shared.Utils;

namespace Codem.Infrastructure;

public static class DependencyInjection
{
    public static void AddNhibernate(this IServiceCollection services)
    {
        SqlSettings sqlSettings = LoadJsonConfig();
        Configuration configuration = LoadSqlConfig(sqlSettings);

        SqlSetupUtil.LoadMappings(configuration);
        SqlSetupUtil.UpdateScheme(configuration);
        SqlSetupUtil.SetupRepositories(services);
        
        ISessionFactory sessionFactory = configuration.BuildSessionFactory();
        
        TypeAdapterConfig.GlobalSettings.Scan(typeof(InfraMapperConfig).Assembly);
        
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
            db.LogFormattedSql = BuildUtil.IsDevelop;
        });
        
        configuration.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[]
        {
            new SqlCreateDtListener()
        };
        configuration.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[]
        {
            new SqlChangeDtListener()
        };

        return configuration;
    }
    
    #endregion
}