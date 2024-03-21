using Arim.Infrastructure;

namespace {{ EntityInfo.Namespace }};

{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】常量
/// </summary>
{{~ end ~}}
public static class {{ EntityInfo.Name }}Constants
{
    /// <summary>
    /// 默认值
    /// </summary>
    public class DefaultValue
    {
        /// <summary>
        /// 版本的默认值
        /// </summary>
        public const int Version = 1;

        /// <summary>
        /// 状态的默认值
        /// </summary>
        public const string Status = ArimConstants.Status.Created;

        /// <summary>
        /// 是否激活的默认值
        /// </summary>
        public const bool IsActive = false;
    }

    /// <summary>
    /// 最大长度限制
    /// </summary>
    public class MaxLength
    {
        {{~ for prop in EntityInfo.Properties ~}}
        {{~ if prop | abp.is_ignore_property || string.starts_with prop.Type "IList<"; continue; end ~}}
        {{~ if prop.Document| !string.whitespace ~}}
        /// <summary>
        /// 【{{ prop.Document }}】的最大长度限制
        /// </summary>
        {{~ end ~}}
        public const int {{ prop.Name }} = ArimConstants.StringLength.Default;
        {{~ if !for.last ~}}

        {{~ end ~}}
        {{~ end ~}}
    }
}
