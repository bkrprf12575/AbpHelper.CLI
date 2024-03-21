using System;
using Volo.Abp;

namespace {{ EntityInfo.Namespace }}.Exceptions;

{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】已存在的异常信息
/// </summary>
{{~ end ~}}
[Serializable]
public class {{ EntityInfo.Name }}AlreadyExistException: UserFriendlyException
{
    {{~ if EntityInfo.Document | !string.whitespace ~}}
    /// <summary>
    /// 【{{ EntityInfo.Document }}】已存在的异常信息
    /// </summary>
    {{~ end ~}}
    public {{ EntityInfo.Name }}AlreadyExistException()
        : base(
            "{{ EntityInfo.Document }}信息已存在",
            {{ ProjectInfo.Name }}ErrorCodes.{{ EntityInfo.Name }}.AlreadyExistCode,
            "{{ EntityInfo.Document }}信息已存在")
    {
    }
}

