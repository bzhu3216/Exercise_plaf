//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 原始文件名:
// 生成日期: 2020/1/29 15:03:05
namespace Exercise_form.ServiceReference1
{
    
    /// <summary>
    /// 架构中不存在 db_exerciseEntities 的注释。
    /// </summary>
    public partial class db_exerciseEntities : global::System.Data.Services.Client.DataServiceContext
    {
        /// <summary>
        /// 初始化新的 db_exerciseEntities 对象。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public db_exerciseEntities(global::System.Uri serviceRoot) : 
                base(serviceRoot, global::System.Data.Services.Common.DataServiceProtocolVersion.V3)
        {
            this.ResolveName = new global::System.Func<global::System.Type, string>(this.ResolveNameFromType);
            this.ResolveType = new global::System.Func<string, global::System.Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
            this.Format.LoadServiceModel = GeneratedEdmModel.GetInstance;
        }
        partial void OnContextCreated();
        /// <summary>
        /// 因为在 Visual Studio 中为此服务引用配置的
        /// 命名空间与在服务器架构中指示的命名空间不同，所以
        /// 使用类型映射器在这两者之间进行映射。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected global::System.Type ResolveTypeFromName(string typeName)
        {
            global::System.Type resolvedType = this.DefaultResolveType(typeName, "db_exerciseModel", "Exercise_form.ServiceReference1");
            if ((resolvedType != null))
            {
                return resolvedType;
            }
            return null;
        }
        /// <summary>
        /// 因为在 Visual Studio 中为此服务引用配置的
        /// 命名空间与在服务器架构中指示的命名空间不同，所以
        /// 使用类型映射器在这两者之间进行映射。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected string ResolveNameFromType(global::System.Type clientType)
        {
            if (clientType.Namespace.Equals("Exercise_form.ServiceReference1", global::System.StringComparison.Ordinal))
            {
                return string.Concat("db_exerciseModel.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// 架构中不存在 mchoiceQues 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<mchoiceQues> mchoiceQues
        {
            get
            {
                if ((this._mchoiceQues == null))
                {
                    this._mchoiceQues = base.CreateQuery<mchoiceQues>("mchoiceQues");
                }
                return this._mchoiceQues;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<mchoiceQues> _mchoiceQues;
        /// <summary>
        /// 架构中不存在 classinfo 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<classinfo> classinfo
        {
            get
            {
                if ((this._classinfo == null))
                {
                    this._classinfo = base.CreateQuery<classinfo>("classinfo");
                }
                return this._classinfo;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<classinfo> _classinfo;
        /// <summary>
        /// 架构中不存在 Course 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<Course> Course
        {
            get
            {
                if ((this._Course == null))
                {
                    this._Course = base.CreateQuery<Course>("Course");
                }
                return this._Course;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<Course> _Course;
        /// <summary>
        /// 架构中不存在 teacherinfo 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<teacherinfo> teacherinfo
        {
            get
            {
                if ((this._teacherinfo == null))
                {
                    this._teacherinfo = base.CreateQuery<teacherinfo>("teacherinfo");
                }
                return this._teacherinfo;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<teacherinfo> _teacherinfo;
        /// <summary>
        /// 架构中不存在 mchoiceQues 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddTomchoiceQues(mchoiceQues mchoiceQues)
        {
            base.AddObject("mchoiceQues", mchoiceQues);
        }
        /// <summary>
        /// 架构中不存在 classinfo 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToclassinfo(classinfo classinfo)
        {
            base.AddObject("classinfo", classinfo);
        }
        /// <summary>
        /// 架构中不存在 Course 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToCourse(Course course)
        {
            base.AddObject("Course", course);
        }
        /// <summary>
        /// 架构中不存在 teacherinfo 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToteacherinfo(teacherinfo teacherinfo)
        {
            base.AddObject("teacherinfo", teacherinfo);
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private abstract class GeneratedEdmModel
        {
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel ParsedModel = LoadModelFromString();
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private const string ModelPart0 = "<edmx:Edmx Version=\"1.0\" xmlns:edmx=\"http://schemas.microsoft.com/ado/2007/06/edm" +
                "x\"><edmx:DataServices m:DataServiceVersion=\"1.0\" m:MaxDataServiceVersion=\"3.0\" x" +
                "mlns:m=\"http://schemas.microsoft.com/ado/2007/08/dataservices/metadata\"><Schema " +
                "Namespace=\"db_exerciseModel\" xmlns=\"http://schemas.microsoft.com/ado/2009/11/edm" +
                "\"><EntityType Name=\"mchoiceQues\"><Key><PropertyRef Name=\"id\" /></Key><Property N" +
                "ame=\"id\" Type=\"Edm.Int32\" Nullable=\"false\" p6:StoreGeneratedPattern=\"Identity\" x" +
                "mlns:p6=\"http://schemas.microsoft.com/ado/2009/02/edm/annotation\" /><Property Na" +
                "me=\"ques\" Type=\"Edm.String\" MaxLength=\"Max\" FixedLength=\"false\" Unicode=\"false\" " +
                "/><Property Name=\"answ\" Type=\"Edm.Int32\" /><Property Name=\"con\" Type=\"Edm.Int32\"" +
                " /><Property Name=\"objective\" Type=\"Edm.Int32\" /><Property Name=\"diff\" Type=\"Edm" +
                ".Int32\" /><Property Name=\"usenum\" Type=\"Edm.Int32\" /><Property Name=\"errornum\" T" +
                "ype=\"Edm.Int32\" /><Property Name=\"question\" Type=\"Edm.Binary\" MaxLength=\"Max\" Fi" +
                "xedLength=\"false\" /></EntityType><EntityType Name=\"classinfo\"><Key><PropertyRef " +
                "Name=\"classid\" /></Key><Property Name=\"classid\" Type=\"Edm.Int32\" Nullable=\"false" +
                "\" /><Property Name=\"classinfo1\" Type=\"Edm.String\" MaxLength=\"100\" FixedLength=\"f" +
                "alse\" Unicode=\"false\" /><Property Name=\"courseid\" Type=\"Edm.Int32\" /><Property N" +
                "ame=\"teacher\" Type=\"Edm.String\" MaxLength=\"4\" FixedLength=\"true\" Unicode=\"true\" " +
                "/><Property Name=\"addtime\" Type=\"Edm.DateTime\" Precision=\"0\" /></EntityType><Ent" +
                "ityType Name=\"Course\"><Key><PropertyRef Name=\"id\" /></Key><Property Name=\"id\" Ty" +
                "pe=\"Edm.Int32\" Nullable=\"false\" p6:StoreGeneratedPattern=\"Identity\" xmlns:p6=\"ht" +
                "tp://schemas.microsoft.com/ado/2009/02/edm/annotation\" /><Property Name=\"CourseN" +
                "ame\" Type=\"Edm.String\" Nullable=\"false\" MaxLength=\"50\" FixedLength=\"false\" Unico" +
                "de=\"false\" /><Property Name=\"Courseid\" Type=\"Edm.String\" MaxLength=\"50\" FixedLen" +
                "gth=\"false\" Unicode=\"false\" /></EntityType><EntityType Name=\"teacherinfo\"><Key><" +
                "PropertyRef Name=\"teacherid\" /></Key><Property Name=\"teacherid\" Type=\"Edm.String" +
                "\" Nullable=\"false\" MaxLength=\"4\" FixedLength=\"true\" Unicode=\"true\" /><Property N" +
                "ame=\"name\" Type=\"Edm.String\" MaxLength=\"10\" FixedLength=\"true\" Unicode=\"true\" />" +
                "<Property Name=\"pd\" Type=\"Edm.String\" MaxLength=\"10\" FixedLength=\"true\" Unicode=" +
                "\"true\" /></EntityType></Schema><Schema Namespace=\"WCFDBservice\" xmlns=\"http://sc" +
                "hemas.microsoft.com/ado/2009/11/edm\"><EntityContainer Name=\"db_exerciseEntities\"" +
                " m:IsDefaultEntityContainer=\"true\" p6:LazyLoadingEnabled=\"true\" xmlns:p6=\"http:/" +
                "/schemas.microsoft.com/ado/2009/02/edm/annotation\"><EntitySet Name=\"mchoiceQues\"" +
                " EntityType=\"db_exerciseModel.mchoiceQues\" /><EntitySet Name=\"classinfo\" EntityT" +
                "ype=\"db_exerciseModel.classinfo\" /><EntitySet Name=\"Course\" EntityType=\"db_exerc" +
                "iseModel.Course\" /><EntitySet Name=\"teacherinfo\" EntityType=\"db_exerciseModel.te" +
                "acherinfo\" /></EntityContainer></Schema></edmx:DataServices></edmx:Edmx>";
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static string GetConcatenatedEdmxString()
            {
                return string.Concat(ModelPart0);
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            public static global::Microsoft.Data.Edm.IEdmModel GetInstance()
            {
                return ParsedModel;
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel LoadModelFromString()
            {
                string edmxToParse = GetConcatenatedEdmxString();
                global::System.Xml.XmlReader reader = CreateXmlReader(edmxToParse);
                try
                {
                    return global::Microsoft.Data.Edm.Csdl.EdmxReader.Parse(reader);
                }
                finally
                {
                    ((global::System.IDisposable)(reader)).Dispose();
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::System.Xml.XmlReader CreateXmlReader(string edmxToParse)
            {
                return global::System.Xml.XmlReader.Create(new global::System.IO.StringReader(edmxToParse));
            }
        }
    }
    /// <summary>
    /// 架构中不存在 db_exerciseModel.mchoiceQues 的注释。
    /// </summary>
    /// <KeyProperties>
    /// id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("mchoiceQues")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("id")]
    public partial class mchoiceQues : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 创建新的 mchoiceQues 对象。
        /// </summary>
        /// <param name="ID">id 的初始值。</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static mchoiceQues CreatemchoiceQues(int ID)
        {
            mchoiceQues mchoiceQues = new mchoiceQues();
            mchoiceQues.id = ID;
            return mchoiceQues;
        }
        /// <summary>
        /// 架构中不存在属性 id 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this.OnidChanging(value);
                this._id = value;
                this.OnidChanged();
                this.OnPropertyChanged("id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _id;
        partial void OnidChanging(int value);
        partial void OnidChanged();
        /// <summary>
        /// 架构中不存在属性 ques 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string ques
        {
            get
            {
                return this._ques;
            }
            set
            {
                this.OnquesChanging(value);
                this._ques = value;
                this.OnquesChanged();
                this.OnPropertyChanged("ques");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _ques;
        partial void OnquesChanging(string value);
        partial void OnquesChanged();
        /// <summary>
        /// 架构中不存在属性 answ 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<int> answ
        {
            get
            {
                return this._answ;
            }
            set
            {
                this.OnanswChanging(value);
                this._answ = value;
                this.OnanswChanged();
                this.OnPropertyChanged("answ");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<int> _answ;
        partial void OnanswChanging(global::System.Nullable<int> value);
        partial void OnanswChanged();
        /// <summary>
        /// 架构中不存在属性 con 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<int> con
        {
            get
            {
                return this._con;
            }
            set
            {
                this.OnconChanging(value);
                this._con = value;
                this.OnconChanged();
                this.OnPropertyChanged("con");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<int> _con;
        partial void OnconChanging(global::System.Nullable<int> value);
        partial void OnconChanged();
        /// <summary>
        /// 架构中不存在属性 objective 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<int> objective
        {
            get
            {
                return this._objective;
            }
            set
            {
                this.OnobjectiveChanging(value);
                this._objective = value;
                this.OnobjectiveChanged();
                this.OnPropertyChanged("objective");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<int> _objective;
        partial void OnobjectiveChanging(global::System.Nullable<int> value);
        partial void OnobjectiveChanged();
        /// <summary>
        /// 架构中不存在属性 diff 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<int> diff
        {
            get
            {
                return this._diff;
            }
            set
            {
                this.OndiffChanging(value);
                this._diff = value;
                this.OndiffChanged();
                this.OnPropertyChanged("diff");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<int> _diff;
        partial void OndiffChanging(global::System.Nullable<int> value);
        partial void OndiffChanged();
        /// <summary>
        /// 架构中不存在属性 usenum 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<int> usenum
        {
            get
            {
                return this._usenum;
            }
            set
            {
                this.OnusenumChanging(value);
                this._usenum = value;
                this.OnusenumChanged();
                this.OnPropertyChanged("usenum");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<int> _usenum;
        partial void OnusenumChanging(global::System.Nullable<int> value);
        partial void OnusenumChanged();
        /// <summary>
        /// 架构中不存在属性 errornum 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<int> errornum
        {
            get
            {
                return this._errornum;
            }
            set
            {
                this.OnerrornumChanging(value);
                this._errornum = value;
                this.OnerrornumChanged();
                this.OnPropertyChanged("errornum");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<int> _errornum;
        partial void OnerrornumChanging(global::System.Nullable<int> value);
        partial void OnerrornumChanged();
        /// <summary>
        /// 架构中不存在属性 question 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public byte[] question
        {
            get
            {
                if ((this._question != null))
                {
                    return ((byte[])(this._question.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnquestionChanging(value);
                this._question = value;
                this.OnquestionChanged();
                this.OnPropertyChanged("question");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private byte[] _question;
        partial void OnquestionChanging(byte[] value);
        partial void OnquestionChanged();
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// 架构中不存在 db_exerciseModel.classinfo 的注释。
    /// </summary>
    /// <KeyProperties>
    /// classid
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("classinfo")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("classid")]
    public partial class classinfo : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 创建新的 classinfo 对象。
        /// </summary>
        /// <param name="classid">classid 的初始值。</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static classinfo Createclassinfo(int classid)
        {
            classinfo classinfo = new classinfo();
            classinfo.classid = classid;
            return classinfo;
        }
        /// <summary>
        /// 架构中不存在属性 classid 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int classid
        {
            get
            {
                return this._classid;
            }
            set
            {
                this.OnclassidChanging(value);
                this._classid = value;
                this.OnclassidChanged();
                this.OnPropertyChanged("classid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _classid;
        partial void OnclassidChanging(int value);
        partial void OnclassidChanged();
        /// <summary>
        /// 架构中不存在属性 classinfo1 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string classinfo1
        {
            get
            {
                return this._classinfo1;
            }
            set
            {
                this.Onclassinfo1Changing(value);
                this._classinfo1 = value;
                this.Onclassinfo1Changed();
                this.OnPropertyChanged("classinfo1");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _classinfo1;
        partial void Onclassinfo1Changing(string value);
        partial void Onclassinfo1Changed();
        /// <summary>
        /// 架构中不存在属性 courseid 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<int> courseid
        {
            get
            {
                return this._courseid;
            }
            set
            {
                this.OncourseidChanging(value);
                this._courseid = value;
                this.OncourseidChanged();
                this.OnPropertyChanged("courseid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<int> _courseid;
        partial void OncourseidChanging(global::System.Nullable<int> value);
        partial void OncourseidChanged();
        /// <summary>
        /// 架构中不存在属性 teacher 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string teacher
        {
            get
            {
                return this._teacher;
            }
            set
            {
                this.OnteacherChanging(value);
                this._teacher = value;
                this.OnteacherChanged();
                this.OnPropertyChanged("teacher");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _teacher;
        partial void OnteacherChanging(string value);
        partial void OnteacherChanged();
        /// <summary>
        /// 架构中不存在属性 addtime 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> addtime
        {
            get
            {
                return this._addtime;
            }
            set
            {
                this.OnaddtimeChanging(value);
                this._addtime = value;
                this.OnaddtimeChanged();
                this.OnPropertyChanged("addtime");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.DateTime> _addtime;
        partial void OnaddtimeChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnaddtimeChanged();
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// 架构中不存在 db_exerciseModel.Course 的注释。
    /// </summary>
    /// <KeyProperties>
    /// id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Course")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("id")]
    public partial class Course : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 创建新的 Course 对象。
        /// </summary>
        /// <param name="ID">id 的初始值。</param>
        /// <param name="courseName">CourseName 的初始值。</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static Course CreateCourse(int ID, string courseName)
        {
            Course course = new Course();
            course.id = ID;
            course.CourseName = courseName;
            return course;
        }
        /// <summary>
        /// 架构中不存在属性 id 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this.OnidChanging(value);
                this._id = value;
                this.OnidChanged();
                this.OnPropertyChanged("id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _id;
        partial void OnidChanging(int value);
        partial void OnidChanged();
        /// <summary>
        /// 架构中不存在属性 CourseName 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string CourseName
        {
            get
            {
                return this._CourseName;
            }
            set
            {
                this.OnCourseNameChanging(value);
                this._CourseName = value;
                this.OnCourseNameChanged();
                this.OnPropertyChanged("CourseName");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _CourseName;
        partial void OnCourseNameChanging(string value);
        partial void OnCourseNameChanged();
        /// <summary>
        /// 架构中不存在属性 Courseid 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Courseid
        {
            get
            {
                return this._Courseid;
            }
            set
            {
                this.OnCourseidChanging(value);
                this._Courseid = value;
                this.OnCourseidChanged();
                this.OnPropertyChanged("Courseid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Courseid;
        partial void OnCourseidChanging(string value);
        partial void OnCourseidChanged();
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// 架构中不存在 db_exerciseModel.teacherinfo 的注释。
    /// </summary>
    /// <KeyProperties>
    /// teacherid
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("teacherinfo")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("teacherid")]
    public partial class teacherinfo : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 创建新的 teacherinfo 对象。
        /// </summary>
        /// <param name="teacherid">teacherid 的初始值。</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static teacherinfo Createteacherinfo(string teacherid)
        {
            teacherinfo teacherinfo = new teacherinfo();
            teacherinfo.teacherid = teacherid;
            return teacherinfo;
        }
        /// <summary>
        /// 架构中不存在属性 teacherid 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string teacherid
        {
            get
            {
                return this._teacherid;
            }
            set
            {
                this.OnteacheridChanging(value);
                this._teacherid = value;
                this.OnteacheridChanged();
                this.OnPropertyChanged("teacherid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _teacherid;
        partial void OnteacheridChanging(string value);
        partial void OnteacheridChanged();
        /// <summary>
        /// 架构中不存在属性 name 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this.OnnameChanging(value);
                this._name = value;
                this.OnnameChanged();
                this.OnPropertyChanged("name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _name;
        partial void OnnameChanging(string value);
        partial void OnnameChanged();
        /// <summary>
        /// 架构中不存在属性 pd 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string pd
        {
            get
            {
                return this._pd;
            }
            set
            {
                this.OnpdChanging(value);
                this._pd = value;
                this.OnpdChanged();
                this.OnPropertyChanged("pd");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _pd;
        partial void OnpdChanging(string value);
        partial void OnpdChanged();
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
