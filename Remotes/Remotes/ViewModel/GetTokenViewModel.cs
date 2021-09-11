using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.ViewModel
{
    public class GetTokenReuestViewModel
    {
        /// <summary>
        /// Sha256加密後的 Secret
        /// </summary>
        public string Secret { get; set; }
    }

    public class GetTokenResponseViewModel
    {
        public string Token { get; set; }
    }
}
