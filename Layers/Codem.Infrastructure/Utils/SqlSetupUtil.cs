using Codem.Infrastructure.Entities.UserSnippetFk;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Tool.hbm2ddl;
using Сodem.Shared.Utils;

namespace Codem.Infrastructure.Utils;

public static class SqlSetupUtil
{
    public static void UpdateScheme(Configuration configuration)
    {
        SchemaUpdate update = new(configuration);
        update.Execute(true, BuildUtil.IsDevelop);
    }
    
    public static void LoadMappings(Configuration configuration)
    {
        ModelMapper mapper = new();
        
        mapper.AddMapping<SqlSnippetMap>();
        mapper.AddMapping<SqlFileMap>();
        mapper.AddMapping<SqlUserSnippetFkMap>();
        
        HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        configuration.AddMapping(mapping);
    }
    
    public static void SetupRepositories(IServiceCollection services)
    {
        services.AddScoped<ISnippetRepository, SqlSnippetRepository>();
    }
}