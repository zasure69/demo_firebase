

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
    /*private static string AuthEmail = "22520410@gm.uit.edu.vn";
    private static string AuthPassword = "12345678";*/

    private static void Main(string[] args)
    {
        Run().Wait();
    }
    //demo firebase storage
    private static async Task Run()
    {
        // FirebaseStorage.Put method accepts any type of stream.
        //var stream = new MemoryStream(Encoding.ASCII.GetBytes("Hello world!"));
        var stream = File.Open(@"C:\TLHT\HK2_2023-2024\LapTrinhMangCanBan\Demo_Firebase\firebase_storage\test.txt", FileMode.Open);

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
        /*var auth = new FirebaseAuthClient(config);
        var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);*/

        // you can use CancellationTokenSource to cancel the upload midway
        var cancellation = new CancellationTokenSource();

        var task = new FirebaseStorage(
            Bucket
            ++++++++++++++++++++++++++++++++++++---------
            +-/*
            new FirebaseStorageOptions
            {
                
                //ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
            }*/)
            .Child("test")
            .Child("test1.txt")
            .PutAsync(stream, cancellation.Token);

            

        try
        {
            // error during upload will be thrown when you await the task
            await task;
            // cancel the upload
            cancellation.Cancel();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception was thrown: {0}", ex);
        }
    }
}