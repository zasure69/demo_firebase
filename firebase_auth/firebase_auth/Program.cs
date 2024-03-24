using System.Runtime.CompilerServices;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Storage;

class Program
{
    static void Main()
    {
        Run().Wait();
    }

    static async Task Run()
    {
        //config Firebase Authentication
        var config = new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyBHZYh9tSMeSEYpZIRgdK7etYcQZUJj4vU",
            AuthDomain = "fir-c569c.firebaseapp.com",
            Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
        };

        var client = new FirebaseAuthClient(config);
        
        try
        {
            UserCredential userCredential = await SignIn(client);

            Console.WriteLine("You're signed in as {0}", userCredential.User.Info.DisplayName);
        }
        catch (FirebaseAuthException ex)
        {
            Console.WriteLine(ex.Reason);
            return;
        }
    }


    static async Task<UserCredential> SignIn (FirebaseAuthClient client)
    {
        Console.Write("Enter email: ");
        var email = Console.ReadLine();

        //check user exists
        var result = await client.FetchSignInMethodsForEmailAsync(email);

        if (result.UserExists)
        {
            Console.Write("User exists, enter password: ");
            var password = Console.ReadLine();
            var credential = EmailProvider.GetCredential(email, password);
            var emailUser = await client.SignInWithCredentialAsync(credential);
            while (emailUser.User.Uid == null)
            {
                Console.Write("Wrong password, enter password: ");
                credential = EmailProvider.GetCredential(email, password);
                emailUser = await client.SignInWithCredentialAsync(credential);

            }
            return emailUser;
        }
        else
        {
            Console.Write("User not found, let's create user's account. Enter password (at least 6 characters): ");
            var password = Console.ReadLine();
            Console.Write("Enter display name: ");
            var displayName = Console.ReadLine();
            return await client.CreateUserWithEmailAndPasswordAsync (email, password, displayName);
        }
        
    }
}




