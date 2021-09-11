using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Remotes.Filters;
using Remotes.Helper;
using Remotes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.API
{
    [Route("api/[controller]")]
    [ApiController, TypeFilter(typeof(APIActionCustomFilter))]
    public class GetTokenController : ControllerBase
    {
        private readonly JwtHelpers _jwtHelpers;
        private readonly IConfiguration _configuration;
        public GetTokenController(JwtHelpers jwtHelpers, IConfiguration configuration)
        {
            _jwtHelpers = jwtHelpers;
            _configuration = configuration;
        }

        [HttpPost]
        public APIResponseBaseViewModel<GetTokenResponseViewModel> POST([FromBody] GetTokenReuestViewModel data)
        {
            var resp = new APIResponseBaseViewModel<GetTokenResponseViewModel>() { Content = new GetTokenResponseViewModel() };
            var secret = _configuration.GetValue<string>("JwtSettings:SignKey");
                        
            if (Tools.CheckAPIParamterWithMessage(APICheckParamterType.Secret, resp, data.Secret, secret))
            {
                resp.Content.Token = _jwtHelpers.GenerateToken();
            }
                                    
            return resp;
        }
    }
}
