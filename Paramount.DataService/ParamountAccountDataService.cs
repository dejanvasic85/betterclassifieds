namespace Paramount.DataService
{
    using System;
    using ApplicationBlock.Data;

    public class ParamountAccountDataService
    {
        private const string ConfigSection = @"paramount/services";
        public static bool CreateAccount(
                string accountId,
                string userId,
                string accountName,
                string abn,
                string address,
                int? postcode,
                string state,
                string country,
                int? businessType,
                string domain
            )
        {
            var dataProxyFactory = new DatabaseProxy(Proc.AccountInformationInsert.Name, ConfigSection);

            dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.AccountId, accountId, StringType.VarChar);
            dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.MasterUserId, userId, StringType.VarChar);

            if (accountName.HasValue())
            {
                dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.AccountName, accountName,
                                              StringType.VarChar);
            }

            if (abn.HasValue())
            {
                dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.Abn, abn, StringType.VarChar);
            }

            if (address.HasValue())
            {
                dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.Address, address, StringType.VarChar);
            }

            if (postcode.HasValue)
            {
                dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.Postcode, postcode.Value);
            }

            if (state.HasValue())
            {
                dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.State, state, StringType.VarChar);
            }

            if (country.HasValue())
            {
                dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.Country, country, StringType.VarChar);
            }

            if (businessType.HasValue)
            {
                dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.BusinessType, businessType.Value);
            }

            if (domain.HasValue())
            {
                dataProxyFactory.AddParameter(Proc.AccountInformationInsert.Params.Domain, domain, StringType.VarChar);
            }

            dataProxyFactory.ExecuteNonQuery();

            return true;
        }
    }
}
