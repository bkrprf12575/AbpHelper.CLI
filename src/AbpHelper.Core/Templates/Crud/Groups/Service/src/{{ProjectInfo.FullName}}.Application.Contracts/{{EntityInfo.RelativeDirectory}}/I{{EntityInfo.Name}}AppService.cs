using System;
{{~ if Option.SkipGetListInputDto ~}}
using Volo.Abp.Application.Dtos;
{{~ end ~}}
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Arim.Infrastructure.Inputs;
using Volo.Abp.Application.Services;

namespace {{ EntityInfo.Namespace }};

{{
    if !Option.SkipGetListInputDto
        TGetListInput = EntityInfo.Name + "GetListInput"
    else
        TGetListInput = "PagedAndSortedResultRequestDto"
end ~}}
{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】应用服务
/// </summary>
{{~ end ~}}
public interface I{{ EntityInfo.Name }}AppService 
    : ICrudAppService<{{ DtoInfo.ReadTypeName }}, {{ EntityInfo.PrimaryKey ?? EntityInfo.CompositeKeyName }}, {{TGetListInput}}, {{ DtoInfo.CreateTypeName }}, {{ DtoInfo.UpdateTypeName }}>
{
    /// <summary>
    /// 删除【{{ EntityInfo.Document }}】信息（批量）
    /// </summary>
    /// <param name="input">批量删除信息</param>
    public Task DeleteManyAsync([Required] BulkDeleteInput<Guid> input);
}