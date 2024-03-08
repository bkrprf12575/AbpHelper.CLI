namespace EasyAbp.AbpHelper.Core.Models
{
    public class PropertyInfo
    {
        public string Type { get; }

        public string Name { get; }

        public string Document { get; set; }

        public bool IsNullable { get; set; }

        public PropertyInfo(string type, bool isNullable, string name, string document)
        {
            Type = type;
            IsNullable=isNullable;
            Name = name;
            Document = document;
        }
    }
}