{{~
if ProjectInfo.TemplateType == 'Module'
    dbContextName = "I" + ProjectInfo.Name + "DbContext"
else
    dbContextName = ProjectInfo.Name + "DbContext"
end
~}}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using {{ ProjectInfo.FullName }}.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace {{ EntityInfo.Namespace }};

{{~
    if EntityInfo.CompositeKeyName
        primaryKeyText = ""
    else
        primaryKeyText = ", " + EntityInfo.PrimaryKey
    end
~}}
{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】仓储
/// </summary>
{{~ end ~}}
public class {{ EntityInfo.Name }}(IDbContextProvider<{{ dbContextName }}> dbContextProvider)
    : EfCoreRepository<{{ dbContextName }}, {{ EntityInfo.Name }}{{ primaryKeyText }}>(dbContextProvider), 
        I{{ EntityInfo.Name }}Repository
{
    public override async Task<IQueryable<{{ EntityInfo.Name }}>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

    public override async Task<{{ EntityInfo.Name }}> GetAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = new())
    {
        var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));
        return entity switch
        {
            null => throw new {{ EntityInfo.Name }}NotFoundException(id),
            _ => entity
        };
    }

    public new async Task<{{ EntityInfo.Name }}> GetAsync(bool includeDetails = true, CancellationToken cancellationToken = new())
    {
        var entity = await FindAsync(includeDetails, GetCancellationToken(cancellationToken));
        return entity switch
        {
            null => throw new {{ EntityInfo.Name }}NotFoundException(),
        _ => entity
        };
    }
}