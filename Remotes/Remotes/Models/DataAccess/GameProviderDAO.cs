using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remotes.Models
{
    public class GameProviderDAO
    {
        private string _connectString;
        public GameProviderDAO(IConfiguration configruration)
        {
            _connectString = configruration.GetConnectionString("DefaultConnectionString");
        }
    }
}
