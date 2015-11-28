namespace Paramount.Common.UIController
{
    using System.Collections.ObjectModel;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Paramount.Common.DataTransferObjects.UserAccountService;
    using ViewObjects;

    public partial class Converter
    {
        public static UserAccountProfileView FromContract(UserAccountProfile contract)
        {
            if (contract == null)
                return null;

            return new UserAccountProfileView
            {
                Email = contract.Email,
                UserId = contract.UserId,
                Username = contract.Username,
                CreateDate = contract.CreateDate,
                IsApproved = contract.IsApproved,
                IsLockedOut = contract.IsLockedOut
            };
        }

        public static List<UserAccountProfileView> FromContractCollection(List<UserAccountProfile> contractCollection)
        {
            if (contractCollection == null)
                return null;

            return contractCollection.Select(i => Converter.FromContract(i)).ToList();
        }
    }
}
