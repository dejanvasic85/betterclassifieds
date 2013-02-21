namespace Paramount.ApplicationBlock.Logging.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Enum;

    public class Parameter
    {
        private readonly string name;
        private readonly SqlDbType sqlDbType;
        private readonly object value;

        public Parameter(string name, int? value)
            : this(name, SqlDbType.Int, value.HasValue ? (object)value.Value : DBNull.Value) { }

        public Parameter(string name, decimal? value)
            : this(name, SqlDbType.Decimal, value.HasValue ? (object)value.Value : DBNull.Value) { }

        public Parameter(string name, DateTime? value)
            : this(name, SqlDbType.DateTime, value.HasValue ? (object)value.Value : DBNull.Value) { }

        public Parameter(string name, string value, StringType stringType) : this(name, GetSqlDbType(stringType), value) { }

        public Parameter(string name, bool? value) : this(name, SqlDbType.Bit, value) { }

        public Parameter(string name, Guid value) : this(name, SqlDbType.UniqueIdentifier, value) { }

        protected Parameter(string name, SqlDbType sqlDBType, object value)
        {
            this.name = name;
            this.sqlDbType = sqlDBType;
            this.value = value;
        }

        public string Name
        {
            get { return this.name; }
        }

        public SqlDbType SqlDBType
        {
            get { return this.sqlDbType; }
        }

        public virtual SqlParameter SqlParameter
        {
            get { return new SqlParameter { SqlDbType = this.sqlDbType, ParameterName = this.name, Value = this.value }; }
        }

        public object Value
        {
            get { return this.value; }
        }

        private static SqlDbType GetSqlDbType(StringType stringType)
        {
            switch (stringType)
            {
                case StringType.Char:
                    return SqlDbType.Char;
                case StringType.Text:
                    return SqlDbType.Text;
                case StringType.VarChar:
                    return SqlDbType.VarChar;
                default:
                    throw new ArgumentException(
                        @"StringType """ + stringType + @""" is not recognised by Parameter.GetSqlDbType");
            }
        }
    }
}
