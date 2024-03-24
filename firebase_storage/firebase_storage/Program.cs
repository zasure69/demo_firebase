

using System;
using System.IO;
using Firebase.Auth;
using Firebase.Storage;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

internal class Program
{
    private static string ApiKey = "AIzaSyBHZYh9tSMeSEYpZIRgdK7etYcQZUJj4vU";
    private static string Bucket = "fir-c569c.appspot.com";
    private static string AuthEmail = "zasure69@gmail.com";
    private static string AuthPassword = "123456";

    private static void Main(string[] args)
    {
        Run().Wait();
    }

    private static async Task Run()
    {
        // FirebaseStorage.Put method accepts any type of stream.
        var stream = new MemoryStream(Encoding.ASCII.GetBytes("Hello world!"));
        //var stream = File.Open(@"C:\someFile.png", FileMode.Open);

        // of course you can login using other method, not just email+password
        var config = new FirebaseAuthConfig
        {
            ApiKey = ApiKey,
            AuthDomain = "fir-c569c.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                // Add and configure individual providers
        
                new EmailProvider()
                // ...
            }
        };
        var auth = new FirebaseAuthClient(config);
        var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

        // you can use CancellationTokenSource to cancel the upload midway
        var cancellation = new CancellationTokenSource();

        var task = new FirebaseStorage(
            Bucket,
            new FirebaseStorageOptions
            {
                
                ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
            })
            .Child("receipts")
            .Child("test")
            .Child("someFile.png")
            .PutAsync(stream, cancellation.Token);

        task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

        // cancel the upload
        // cancellation.Cancel();

        try
        {
            // error during upload will be thrown when you await the task
            Console.WriteLine("Download link:\n" + await task);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception was thrown: {0}", ex);
        }
    }
}