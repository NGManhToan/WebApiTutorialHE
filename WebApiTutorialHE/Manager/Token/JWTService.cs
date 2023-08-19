using Microsoft.IdentityModel.Tokens;
using WebApiTutorialHE.Manager.Token.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiTutorialHE.Manager.Token
{
    public class JWTService : IAuthService
    {
        public string SecretKey { get; set; }//Nơi lưu trữ mã bí mật để tạo và xác thực JWT

        public JWTService(string secretKey)//Nhận vào 1 mã bí mật và gán giá trị này cho thuộc tính
        {
            SecretKey = secretKey;
        }


        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Không thể xác nhận được token.");
            }

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();//lấy các thông số cần thiết xác thực JWT

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();//Tạo các đối tượng JWT, thực hiện kiểm tra tính hợp lệ của JWT
                                                                                            // và trích xuất dữ liệu
            try
            {
                //if (token.StartsWith("Bearer "))
                //{
                //    token = token.Substring(7); // Loại bỏ tiền tố "Bearer "
                //}

                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token,
                    tokenValidationParameters,
                    out SecurityToken validatedToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public string GenerateToken(IAuthContainerModel model)
        {
            if (model == null || model.Claims == null || model.Claims.Length == 0)
            {
                throw new ArgumentException("Chứng thực không thể xác thực.");
            }

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(model.Claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes)),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), model.SecurityAlgorithm)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Chứng thực không thể xác thực.");
            }

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                return tokenValid.Claims;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(Signature.Base64Encode(SecretKey));
            return new SymmetricSecurityKey(symmetricKey);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
    }
}
