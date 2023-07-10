using System;
using System.ComponentModel;
using DGCore.Common;
using DGCore.PD;

namespace DGCore.Helpers
{
    public class DGColumnModel
    {
        public string Id { get; }
        public string DisplayName { get; }
        public int DisplayIndex { get; }
        public string Format { get; }
        public object GetValue(object component) => _getter(component);
        public Type NotNullableValueType { get; }

        private Func<object, object> _getter;

        public bool IsHidden { get; set; }
        public int? Width { get; set; }
        public string Description { get; set; }
        public Enums.TotalFunction? TotalFunction { get; set; }
        public bool IsFrozen { get; set; }

        public DGColumnModel(PropertyDescriptor pd, int columnDisplayIndex)
        {
            Id = pd.Name;
            DisplayName = pd.DisplayName;
            DisplayIndex = columnDisplayIndex;
            NotNullableValueType = Utils.Types.GetNotNullableType(pd.PropertyType);
            Format = ((IMemberDescriptor)pd).Format;
            _getter = o => pd.GetValue(o);
        }
    }
}
