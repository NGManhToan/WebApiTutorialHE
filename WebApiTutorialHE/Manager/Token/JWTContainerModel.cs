using Microsoft.IdentityModel.Tokens;
using WebApiTutorialHE.Manager.Token.Interface;
using WebApiTutorialHE.Models.UtilsProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiTutorialHE.Manager.Token
{
    public class JWTContainerModel:IAuthContainerModel
    {
        public string SecretKey { get; set; } = Utils.KeyToken;
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 525600; // 1 năm
        public Claim[] Claims { get; set; }
    }
}
