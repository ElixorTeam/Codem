using NHibernate.Mapping.ByCode.Conformist;
using SqlCore.Utils;
namespace SqlCore.Tables.CodeSnippets;

public class CodeSnippetMap : ClassMapping<CodeSnippetEntity>
{
    public CodeSnippetMap()
    {
        Schema(SchemasUtil.Main);
        Table(TableNamesUtil.CodeSnippets);
    }
}