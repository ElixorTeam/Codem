namespace Codem.Infrastructure.Entities.Files;

public class SqlFileMap : ClassMapping<SqlFileEntity>
{
    public SqlFileMap()
    {
        Schema(SqlSchemasUtil.Dbo);
        Table(SqlTableNamesUtil.Files);
        
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
        
        Property(x => x.Name, m =>
        {
            m.Column("TITLE");
            m.Type(NHibernateUtil.String);
            m.Length(30);
            m.NotNullable(true);
        });
        
        Property(x => x.Data, m =>
        {
            m.Column("DATA");
            m.Type(NHibernateUtil.String);
            m.Length(8192);
            m.NotNullable(true);
        });

        ManyToOne(x => x.Snippet, m => { 
            m.Cascade(Cascade.All | Cascade.DeleteOrphans); 
            m.Column("SNIPPET_UID"); 
            m.ForeignKey("FK_SNIPPET_FILES");
            m.NotNullable(true);
        });
    }
}