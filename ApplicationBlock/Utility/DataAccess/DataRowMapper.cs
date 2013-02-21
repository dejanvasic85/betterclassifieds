using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Paramount.Utility.DataAccess
{
    public static class DataRowMapper
    {
        /// <summary>
        /// Converts the required datarow to a desired object using System.Reflection library
        /// </summary>
        /// <param name="dataRow">DataRow object containing a record with columns of expected properties in the target object</param>
        /// <param name="objTarget">Object required to be populated with data from the DataRow</param>
        /// <param name="blnSuppressExceptions">When setting a property and exception is thrown, set to True to ignore the Error</param>
        /// <param name="strIgnoreList">A list of properties within the business object to ignore</param>
        public static void ConvertToObject(this DataRow dataRow, object objTarget, bool blnSuppressExceptions, params string[] strIgnoreList)
        {
            List<string> ignore = null;
            if (strIgnoreList != null)
            {
                ignore = new List<string>(strIgnoreList);
            }

            //Loop through the Column in the DataRow
            for (int intcolcount = 0; intcolcount <= dataRow.ItemArray.Length - 1; intcolcount++)
            {
                //Get the column name 
                string strPropertyName = dataRow.Table.Columns[intcolcount].ColumnName;
                if (ignore == null || (ignore != null && !ignore.Contains(strPropertyName)))
                {
                    try
                    {
                        //set the value to the object
                        SetPropertyValue(objTarget, strPropertyName, dataRow[strPropertyName]);
                    }
                    catch (Exception ex)
                    {
                        if (!blnSuppressExceptions)
                            throw new Exception(string.Format("Error while " +
                                  "assigning value to the property : {0}",
                                  strPropertyName), ex);

                    }
                }
            }
        }

        public static void ConvertToObject(this DataRow dataRow, object objTarget)
        {
            bool blnSuppressExceptions = false;
            string[] strIgnoreList = null;

            dataRow.ConvertToObject(objTarget, blnSuppressExceptions, strIgnoreList);
        }

        private static void SetPropertyValue(object objTarget, string strPropertyName, object objValue)
        {
            //Get the property of the Target object(Class)
            // using the Column Name from the Datarow/DataReader
            PropertyInfo propertyInfo = objTarget.GetType().GetProperty(strPropertyName);

            //Get the Type (datatype)of the Property of the target class
            Type pType = GetPropertyType(propertyInfo.PropertyType);
            //Get the Type (datatype) of the Value from the Datarow/DataReader
            Type vType = GetPropertyType(objValue.GetType());

            //For null values in the Datarow/DataReader set the property value as DBnull 
            if (objValue == null)
                propertyInfo.SetValue(objTarget, DBNull.Value, null);
            else if (objValue.Equals(System.DBNull.Value))
            {
                if (pType.Equals(typeof(System.DateTime)))
                    propertyInfo.SetValue(objTarget, DateTime.MinValue, null);
                else if (pType.Equals(typeof(System.Double)))
                    propertyInfo.SetValue(objTarget, 0, null);
                else if (pType.Equals(typeof(System.Int32)))
                    propertyInfo.SetValue(objTarget, 0, null);
                else if (pType.Equals(typeof(System.Byte[])))
                    propertyInfo.SetValue(objTarget, new byte[0], null);
            }
            else
            {
                //For not null values in the Datarow/DataReader 
                if (pType.Equals(vType))
                {
                    // types match, just copy value
                    propertyInfo.SetValue(objTarget, objValue, null);
                }
                else
                {
                    // types don't match, try to coerce
                    if (pType.Equals(typeof(Guid)))
                    {
                        propertyInfo.SetValue(objTarget, new Guid(objValue.ToString()), null);
                    }
                    else if (pType.IsEnum && vType.Equals(typeof(string)))
                    {
                        propertyInfo.SetValue(objTarget, Enum.Parse(pType, objValue.ToString()), null);
                    }
                    else
                    {
                        propertyInfo.SetValue(objTarget, Convert.ChangeType(objValue, pType), null);
                    }
                }
            }
        }

        private static Type GetPropertyType(Type objPropertyType)
        {
            Type objType = objPropertyType;
            if (objType.IsGenericType && (objType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                return Nullable.GetUnderlyingType(objType);
            }
            return objType;
        }
    }
}
