﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_exerciseEntities : DbContext
    {
        public db_exerciseEntities()
            : base("name=db_exerciseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<mchoiceQues> mchoiceQues { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<teacherinfo> teacherinfo { get; set; }
        public DbSet<TFQues> TFQues { get; set; }
        public DbSet<SQues> SQues { get; set; }
        public DbSet<AQues> AQues { get; set; }
        public DbSet<classExer> classExer { get; set; }
        public DbSet<class_student> class_student { get; set; }
        public DbSet<exerDetail> exerDetail { get; set; }
        public DbSet<studAnsw> studAnsw { get; set; }
        public DbSet<tech_course> tech_course { get; set; }
        public DbSet<classinfo> classinfo { get; set; }
        public DbSet<StudInfo> StudInfo { get; set; }
        public DbSet<View_student> View_student { get; set; }
        public DbSet<V_tea_course> V_tea_course { get; set; }
        public DbSet<eQues> eQues { get; set; }
        public DbSet<exerL> exerL { get; set; }
        public DbSet<View_detai_exerL> View_detai_exerL { get; set; }
    }
}
