namespace Paramount.ApplicationBlock.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.SqlClient;
    using System.Data;

    public class OutputParameter : Parameter
    {
        private readonly OutputType _outputType;

        public OutputParameter(string name, OutputType outputType) : this(name, outputType, null) { }

        public OutputParameter(string name, OutputType outputType, object value)
            : base(name, GetSqlDbType(outputType), value)
        {
            _outputType = outputType;
        }

        public OutputType OutputType
        {
            get { return _outputType; }
        }

        public override SqlParameter SqlParameter
        {
            get
            {
                return new SqlParameter
                {
                    SqlDbType = SqlDbType,
                    ParameterName = Name,
                    Value = Value ?? GetEmptyValue(),
                    Direction = ParameterDirection.InputOutput
                };
            }
        }

        private static SqlDbType GetSqlDbType(OutputType type)
        {
            switch (type)
            {
                case OutputType.Char:
                    return SqlDbType.Char;

                case OutputType.UniqueIdentifier:
                    return SqlDbType.UniqueIdentifier;

                case OutputType.VarChar:
                    return SqlDbType.VarChar;

                case OutputType.Int:
                    return SqlDbType.Int;
            }

            throw new ArgumentOutOfRangeException("type");
        }

        private object GetEmptyValue()
        {
            switch (_outputType)
            {
                case OutputType.Char:
                case OutputType.VarChar:
                    return string.Empty;

                case OutputType.UniqueIdentifier:
                    return Guid.Empty;

                case OutputType.Int:
                    return 0;

                default:
                    throw new ArgumentException("Output Type " + _outputType + " is not recognised.");
            }
        }
    }
}
