namespace WebApiTutorialHE.Service.Interface
{
    public interface IShowImageSevice
    {
        Task<byte[]> ShowImageAsync(string fileName);
    }
}
