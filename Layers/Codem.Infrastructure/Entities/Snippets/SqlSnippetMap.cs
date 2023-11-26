using NHibernate.Type;
using Сodem.Shared.Enums;

namespace Codem.Infrastructure.Entities.Snippets;

public class SqlSnippetMap : ClassMapping<SqlSnippetEntity>
{
    public SqlSnippetMap()
    {
        Schema(SqlSchemasUtil.Dbo);
        Table(SqlTableNamesUtil.CodeSnippets);
        
        Id(x => x.Id, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.ChangeDt, m =>
        {
            m.Column("CHANGE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });
        
        Property(x => x.Title, m =>
        {
            m.Column("TITLE");
            m.Type(NHibernateUtil.String);
            m.Length(30);
            m.NotNullable(true);
        });
            
        Property(x => x.Password, m =>
        {
            m.Column("PASSWORD");
            m.Type(NHibernateUtil.String);
            m.Length(24);
            m.NotNullable(true);
        });
        
        Property(x => x.Visibility, m =>
        {
            m.Column("VISIBILITY");
            m.NotNullable(true);
            m.Type<EnumStringType<SnippetVisibilityEnum>>();
            m.Length(8);
            m.NotNullable(true);
        });
        
        Bag(x => x.Files, m => {
            m.Key(k => k.Column("SNIPPET_UID")); 
            m.Cascade(Cascade.All | Cascade.DeleteOrphans);
            m.Inverse(true); 
            m.OrderBy("CREATE_DT ASC");
        }, 
        r =>  r.OneToMany());
    }
}