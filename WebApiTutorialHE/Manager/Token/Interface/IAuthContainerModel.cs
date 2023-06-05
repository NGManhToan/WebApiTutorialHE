using System.Security.Claims;

namespace WebApiTutorialHE.Manager.Token.Interface
{
    public interface IAuthContainerModel
    {
        string SecretKey { get; set; }//là chuỗi bí mật được sử dụng để mã hóa và giải mã token.
        string SecurityAlgorithm { get; set; }// là thuật toán mã hóa được sử dụng để tạo token.
        int ExpireMinutes { get; set; }
        Claim[] Claims { get; set; }//là một mảng các tuyên bố (claim) về người dùng
                                    //, chẳng hạn như ID của người dùng, email, vai trò, v.v.
                                    //Mỗi tuyên bố được định nghĩa dưới dạng một đối tượng Claim.
                                    //Các tuyên bố này sẽ được sử dụng để xác thực và phân quyền trong ứng dụng.
    }
}
