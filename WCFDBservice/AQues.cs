//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFDBservice
{
    using System;
    using System.Collections.Generic;
    
    public partial class AQues
    {
        public int id { get; set; }
        public string teacherid { get; set; }
        public Nullable<int> courseid { get; set; }
        public Nullable<int> con { get; set; }
        public Nullable<int> objective { get; set; }
        public Nullable<int> diff { get; set; }
        public Nullable<int> usenum { get; set; }
        public Nullable<int> errornum { get; set; }
        public byte[] question { get; set; }
        public byte[] answ { get; set; }
    }
}
