using System;
using Volo.Abp;

namespace {{ EntityInfo.Namespace }}.Exceptions;

{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】实体不存在的异常信息
/// </summary>
{{~ end ~}}
[Serializable]
public class {{ EntityInfo.Name }}NotFoundException: UserFriendlyException
{
    {{~ if EntityInfo.Document | !string.whitespace ~}}
    /// <summary>
    /// 【{{ EntityInfo.Document }}】实体不存在的异常信息
    /// </summary>
    {{~ end ~}}
    public {{ EntityInfo.Name }}NotFoundException()
        : base(
            "{{ EntityInfo.Document }}信息不存在",
            {{ ProjectInfo.Name }}ErrorCodes.{{ EntityInfo.Name }}.NotFoundCode,
            "{{ EntityInfo.Document }}信息不存在")
    {
    }

    {{~ if EntityInfo.Document | !string.whitespace ~}}
    /// <summary>
    /// 【{{ EntityInfo.Document }}】实体不存在的异常信息
    /// <param name="id">{{ EntityInfo.Document }}的 ID</param>
    /// </summary>
    {{~ end ~}}
    public {{ EntityInfo.Name }}NotFoundException(Guid id)
        : base(
            "未根据{{ EntityInfo.Document }} ID 查询到信息",
            {{ ProjectInfo.Name }}ErrorCodes.{{ EntityInfo.Name }}.NotFoundCode,
            "未根据{{ EntityInfo.Document }} ID 查询到信息")
    {
    }
}

