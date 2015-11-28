namespace Paramount.ApplicationBlock.Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    public class Parameter
    {
        private readonly string _name;
        private readonly SqlDbType _sqlDbType;
        private readonly object _value;

        public Parameter(string name, int? value)
            : this(name, SqlDbType.Int, value.HasValue ? (object)value.Value : DBNull.Value) { }

        public Parameter(string name, decimal? value)
            : this(name, SqlDbType.Decimal, value.HasValue ? (object)value.Value : DBNull.Value) { }

        public Parameter(string name, DateTime? value)
            : this(name, SqlDbType.DateTime, value.HasValue ? (object)value.Value : DBNull.Value) { }

        public Parameter(string name, string value, StringType stringType) : this(name, GetSqlDbType(stringType), value) { }

        public Parameter(string name, bool? value) : this(name, SqlDbType.Bit, value) { }

        public Parameter(string name, Guid value) : this(name, SqlDbType.UniqueIdentifier, value) { }

        public Parameter(string name, DataTable value) : this(name, SqlDbType.Structured, value) { }

        public Parameter(string name, byte[] value)
            : this(name, SqlDbType.VarBinary, value) { }

        protected Parameter(string name, SqlDbType sqlDbType, object value)
        {
            this._name = name;
            this._sqlDbType = sqlDbType;
            this._value = value;
        }

    

        public string Name
        {
            get { return this._name; }
        }

        public SqlDbType SqlDbType
        {
            get { return this._sqlDbType; }
        }

        public virtual SqlParameter SqlParameter
        {
            get { return new SqlParameter { SqlDbType = this._sqlDbType, ParameterName = this._name, Value = this._value }; }
        }

        public object Value
        {
            get { return this._value; }
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
