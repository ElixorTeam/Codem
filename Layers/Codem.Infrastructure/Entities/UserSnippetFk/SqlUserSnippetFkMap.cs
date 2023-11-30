namespace Codem.Infrastructure.Entities.UserSnippetFk;

public class SqlUserSnippetFkMap : ClassMapping<SqlUserSnippetFkEntity>
{
    public SqlUserSnippetFkMap()
    {
        Schema(SqlSchemasUtil.Dbo);
        Table(SqlTableNamesUtil.UserSnippetFk);
        
        Id(x => x.Id, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });
        
        Property(x => x.UserId, m =>
        {
            m.Column("USER_ID");
            m.Type(NHibernateUtil.String);
            m.Length(64);
            m.NotNullable(true);
        });
        
        ManyToOne(x => x.Snippet, m =>  
        {  
            m.Column("SNIPPET_UID");
            m.ForeignKey("FK_USER_SNIPPET_FK_SNIPPET_UID");
            m.Unique(true);
            m.NotNullable(true);
        });
        
    }
}