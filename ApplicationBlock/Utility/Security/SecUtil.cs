namespace Paramount.Utility.Security
{
    //------------------------------------------------------------------------------ 
    // <copyright file="SecurityUtil.cs" company="Microsoft">
    //     Copyright (c) Microsoft Corporation.  All rights reserved.
    // </copyright>
    //----------------------------------------------------------------------------- 

    /* 
     * SecurityUtil class 
     *
     * Copyright (c) 1999 Microsoft Corporation 
     */
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Configuration.Provider;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Configuration;
    using System.Web.Hosting;
    using System.Web.Security;

    public static class SecUtility
    {
        

        public struct ErrorMessages
        {
            public static string ProviderApplicationNameTooLong = "ProviderApplicationNameTooLong";
            public static string MinRequiredNonalphanumericCharactersCanNotBeMoreThanMinRequiredPasswordLength = "MinRequiredNonalphanumericCharactersCanNotBeMoreThanMinRequiredPasswordLength";
            public const string ProviderSchemaVersionNotMatch = "Provider schema version does not match";
            public const string ValueMustBePositiveInteger = "positive integer expected";
            public const string ValueMustBeNonNegativeInteger = "Positive integer is expected";
            public const string ValueMustBeBoolean = "Boolean value expected";
            public const string ParameterArrayDuplateElement = "Parameter array contains duplates element";
            public const string ParameterArrayEmpty = "Parameter array is empty";
            public const string ParameterContainsComma = "Parameter contains comma";
            public const string ParameterCannotBeEmpty = "Parameter cannot be empty";
            public const string ParameterTooLong = "Parameter is too long";
        }

        public static string GetDefaultAppName()
        {
            try
            {
                string appName = HostingEnvironment.ApplicationVirtualPath;
                if (String.IsNullOrEmpty(appName))
                {
#if !FEATURE_PAL
                    // ROTORTODO: enable Process.MainModule or support an alternative 
                    // naming scheme for (HttpRuntime.AppDomainAppVirtualPath == null)

                    appName = Process.GetCurrentProcess().
                        MainModule.ModuleName;

                    int indexOfDot = appName.IndexOf('.');
                    if (indexOfDot != -1)
                    {
                        appName = appName.Remove(indexOfDot);
                    }
#endif
                    // !FEATURE_PAL
                }

                if (String.IsNullOrEmpty(appName))
                {
                    return "/";
                }
                else
                {
                    return appName;
                }
            }
            catch
            {
                return "/";
            }
        }

        // We don't trim the param before checking with password parameters
        internal static bool ValidatePasswordParameter(ref string param, int maxSize)
        {
            if (param == null)
            {
                return false;
            }

            if (param.Length < 1)
            {
                return false;
            }

            if (maxSize > 0 && (param.Length > maxSize))
            {
                return false;
            }

            return true;
        }

        public static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty,
                                             bool checkForCommas, int maxSize)
        {
            if (param == null)
            {
                return !checkForNull;
            }

            param = param.Trim();
            if ((checkIfEmpty && param.Length < 1) ||
                (maxSize > 0 && param.Length > maxSize) ||
                (checkForCommas && param.Contains(",")))
            {
                return false;
            }

            return true;
        }

        // We don't trim the param before checking with password parameters 
        internal static void CheckPasswordParameter(ref string param, int maxSize, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (param.Length < 1)
            {
                throw new ArgumentException(ErrorMessages.ParameterCannotBeEmpty, paramName);
            }

            if (maxSize > 0 && param.Length > maxSize)
            {
                throw new ArgumentException(
                    ErrorMessages.ParameterTooLong,
                    paramName);
            }
        }

        internal static void CheckParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas,
                                            int maxSize, string paramName)
        {
            if (param == null)
            {
                if (checkForNull)
                {
                    throw new ArgumentNullException(paramName);
                }

                return;
            }

            param = param.Trim();
            if (checkIfEmpty && param.Length < 1)
            {
                throw new ArgumentException(ErrorMessages.ParameterCannotBeEmpty, paramName);
            }

            if (maxSize > 0 && param.Length > maxSize)
            {
                throw new ArgumentException(
                    ErrorMessages.ParameterTooLong,
                    paramName);
            }

            if (checkForCommas && param.Contains(","))
            {
                throw new ArgumentException(ErrorMessages.ParameterContainsComma, paramName);
            }
        }

        internal static void CheckArrayParameter(ref string[] param, bool checkForNull, bool checkIfEmpty,
                                                 bool checkForCommas, int maxSize, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (param.Length < 1)
            {
                throw new ArgumentException(ErrorMessages.ParameterArrayEmpty, paramName);
            }

            var values = new Hashtable(param.Length);
            for (int i = param.Length - 1; i >= 0; i--)
            {
                CheckParameter(ref param[i], checkForNull, checkIfEmpty, checkForCommas, maxSize,
                               paramName + "[ " + i.ToString(CultureInfo.InvariantCulture) + " ]");
                if (values.Contains(param[i]))
                {
                    throw new ArgumentException(ErrorMessages.ParameterArrayDuplateElement, paramName);
                }
                else
                {
                    values.Add(param[i], param[i]);
                }
            }
        }

        public static bool GetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
        {
            string sValue = config[valueName];
            if (sValue == null)
            {
                return defaultValue;
            }

            bool result;
            if (bool.TryParse(sValue, out result))
            {
                return result;
            }
            else
            {
                throw new ProviderException(ErrorMessages.ValueMustBeBoolean);
            }
        }

        internal static void CheckSchemaVersion(ProviderBase provider, SqlConnection connection, string[] features,
                                                string version, ref int schemaVersionCheck)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            if (features == null)
            {
                throw new ArgumentNullException("features");
            }

            if (version == null)
            {
                throw new ArgumentNullException("version");
            }

            if (schemaVersionCheck == -1)
            {
                throw new ProviderException(ErrorMessages.ProviderSchemaVersionNotMatch);
            }
            else if (schemaVersionCheck == 0)
            {
                lock (provider)
                {
                    if (schemaVersionCheck == -1)
                    {
                        throw new ProviderException(ErrorMessages.ProviderSchemaVersionNotMatch);
                    }
                    else if (schemaVersionCheck == 0)
                    {
                        int iStatus = 0;
                        SqlCommand cmd = null;
                        SqlParameter p = null;

                        foreach (string feature in features)
                        {
                            cmd = new SqlCommand("dbo.aspnet_CheckSchemaVersion", connection);

                            cmd.CommandType = CommandType.StoredProcedure;

                            p = new SqlParameter("@Feature", feature);
                            cmd.Parameters.Add(p);

                            p = new SqlParameter("@CompatibleSchemaVersion", version);
                            cmd.Parameters.Add(p);

                            p = new SqlParameter("@ReturnValue", SqlDbType.Int);
                            p.Direction = ParameterDirection.ReturnValue;
                            cmd.Parameters.Add(p);

                            cmd.ExecuteNonQuery();

                            iStatus = ((p.Value != null) ? ((int) p.Value) : -1);
                            if (iStatus != 0)
                            {
                                schemaVersionCheck = -1;

                                throw new ProviderException(ErrorMessages.ProviderSchemaVersionNotMatch);
                            }
                        }

                        schemaVersionCheck = 1;
                    }
                }
            }
        }

        public static int GetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed,
                                        int maxValueAllowed)
        {
            string sValue = config[valueName];

            if (sValue == null)
            {
                return defaultValue;
            }

            int iValue;
            if (!Int32.TryParse(sValue, out iValue))
            {
                if (zeroAllowed)
                {
                    throw new ProviderException(ErrorMessages.ValueMustBeNonNegativeInteger);
                }

                throw new ProviderException(ErrorMessages.ValueMustBePositiveInteger);
            }

            if (zeroAllowed && iValue < 0)
            {
                throw new ProviderException(ErrorMessages.ValueMustBeNonNegativeInteger);
            }

            if (!zeroAllowed && iValue <= 0)
            {
                throw new ProviderException(ErrorMessages.ValueMustBePositiveInteger);
            }

            if (maxValueAllowed > 0 && iValue > maxValueAllowed)
            {
                throw new ProviderException(ErrorMessages.ParameterTooLong);
            }

            return iValue;
        }

#if !FEATURE_PAL
        //
#endif
        // !FEATURE_PAL

        
    }
}

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.