using System.Security.Claims;
using System.Collections.Generic;
namespace WebApiTutorialHE.Manager.Token.Interface
{
    public interface IAuthService
    {
        string SecretKey { get; set; }
        bool IsTokenValid(string token);//là một phương thức để kiểm tra tính hợp lệ của JWT,
                                        //nhận một token dưới dạng chuỗi và trả về giá trị boolean.
                                        //Phương thức này sẽ kiểm tra xem JWT có hợp lệ hay không
                                        //bằng cách sử dụng `SecretKey` để giải mã và kiểm tra chữ ký số.
        string GenerateToken(IAuthContainerModel model);//là một phương thức để tạo JWT,
                                                        //nhận một đối tượng IAuthContainerModel chứa
                                                        //các thông tin về token như `Claims`, `ExpireMinutes` và `SecurityAlgorithm`.
                                                        //Phương thức này sử dụng `SecretKey` và `SecurityAlgorithm`
                                                        //để mã hóa các tuyên bố (claims) và tạo ra JWT.
        IEnumerable<Claim> GetTokenClaims(string token);// là một phương thức để lấy thông tin về tuyên bố trong một JWT,
                                                        // nhận một token dưới dạng chuỗi và trả về một danh sách các đối tượng 'Claim'.
                                                        // Phương thức này sử dụng `SecretKey` để giải mã JWT và lấy các tuyên bố từ chuỗi
                                                        // đã giải mã.
    }
}
