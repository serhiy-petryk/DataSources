using System;
using System.Collections.Generic;

namespace DSCore.Core
{
    public class DataInfo
    {
        public List<ColumnInfo> Columns;
    }

    public class ColumnInfo
    {
        public string Name;
        public Type Type;
    }

}
