using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.UIController.Contants
{
    public class ValidationExpressions
    {
        public const string Email = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        public const string Url = @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
    }
}
