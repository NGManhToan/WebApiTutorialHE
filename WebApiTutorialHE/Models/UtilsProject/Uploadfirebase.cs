using Firebase.Auth;
using Firebase.Storage;
using Google.Apis.Storage.v1.Data;

namespace WebApiTutorialHE.Models.UtilsProject
{
    public class Uploadfirebase
    {
        public static string ApiKey = "AIzaSyC3yfLAkaRCBnfusaDX2XdeMUAYqfvg6Mo";
        public static string Bucket = "sharingtogether-c8be8.appspot.com";
        public static string AuthEmail = "manhtoan121102@gmail.com";
        public static string AuthPassword = "123456";
        public async Task<string> UploadAvatar(byte[] imageData, string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            // You can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, an exception is thrown. By default, no exception is thrown.
                })
                .Child("Upload")
                .Child("Avatar")
                .Child(fileName)
                .PutAsync(new MemoryStream(imageData), cancellation.Token);

            try
            {
                // error during upload will be thrown when awaiting the task
                string link = await task;
                return link;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
                throw;
            }
        }

        public async Task<string> UploadPost(byte[] imageData, string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            // You can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, an exception is thrown. By default, no exception is thrown.
                })
                .Child("Upload")
                .Child("Post")
                .Child(fileName)
                .PutAsync(new MemoryStream(imageData), cancellation.Token);

            try
            {
                // error during upload will be thrown when awaiting the task
                string link = await task;
                return link;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
                throw;
            }
        }
    }
}
