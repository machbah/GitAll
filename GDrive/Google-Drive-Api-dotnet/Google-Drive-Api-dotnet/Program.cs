using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;

namespace Daimto.Drive.api
{
    class DriveCommandLineSample
    {
        static void Main(string[] args)
        {
            UserCredential credential;
            using (var filestream = new FileStream("client_secrets.json",
                FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(filestream).Secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore("DriveCommandLineSample")).Result;
            }

            // Create the service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API Sample",
            });

            Google.Apis.Drive.v2.Data.File body = new Google.Apis.Drive.v2.Data.File();
            body.Title = "My document";
            body.Description = "A test document";
            body.MimeType = "text/plain";

            byte[] byteArray = System.IO.File.ReadAllBytes("document.txt");
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

            FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, "text/plain");
            request.Upload();

            Google.Apis.Drive.v2.Data.File file = request.ResponseBody;
            Console.WriteLine("File id: " + file.Id);
            Console.WriteLine("Press Enter to end this process.");
            Console.ReadLine();


            try
            {

                // Listing files with search.  
                // This searches for a directory with the name DiamtoSample
                string Q = "title = 'My document' and mimeType = 'text/plain'";
                IList<Google.Apis.Drive.v2.Data.File> _Files = DaimtoGoogleDriveHelper.GetFiles(service, Q);

                Q = "mimeType = 'application/vnd.google-apps.folder'";
                DaimtoGoogleDriveHelper.GetFolders(service, Q);

                foreach (Google.Apis.Drive.v2.Data.File item in _Files)
                {
                    Console.WriteLine(item.Title + " " + item.MimeType);
                }

                // If there isn't a directory with this name lets create one.
                if (_Files.Count == 0)
                {
                    _Files.Add(DaimtoGoogleDriveHelper.createDirectory(service, "DiamtoSample", "DiamtoSample", "root"));
                }

                // We should have a directory now because we either had it to begin with or we just created one.
                if (_Files.Count != 0)
                {

                    // This is the ID of the directory 
                    string directoryId = _Files[0].Id;

                    //Upload a file
                    Google.Apis.Drive.v2.Data.File newFile = DaimtoGoogleDriveHelper.uploadFile(service, @"c:\GoogleDriveDevelopment\dummyUploadFile.txt", directoryId);
                    // Update The file
                    Google.Apis.Drive.v2.Data.File UpdatedFile = DaimtoGoogleDriveHelper.updateFile(service, @"c:\GoogleDriveDevelopment\dummyUploadFile.txt", directoryId, newFile.Id);
                    // Download the file
                    DaimtoGoogleDriveHelper.downloadFile(service, newFile, @"C:\GoogleDriveDevelopment\downloaded.txt");
                    // delete The file
                    FilesResource.DeleteRequest request1 = service.Files.Delete(newFile.Id);
                    request1.Execute();
                }

                // Getting a list of ALL a users Files (This could take a while.)
                _Files = DaimtoGoogleDriveHelper.GetFiles(service, null);

                foreach (Google.Apis.Drive.v2.Data.File item in _Files)
                {
                    Console.WriteLine(item.Title + " " + item.MimeType);
                }
            }
            catch (Exception ex)
            {

                int i = 1;
            }

            Console.ReadLine();
        }
    }


    /*
    class Program
    {
        
        static void Main(string[] args)
        {
            // Connect with Oauth2 Ask user for permission
            String CLIENT_ID = "936674427482-fkplisfv6q7467ca62bmr4tg9ghf042l.apps.googleusercontent.com";
            String CLIENT_SECRET = "xwbRfPLwbyTm6R0sMQBztKUj";      
            DriveService service = Authentication.AuthenticateOauth(CLIENT_ID, CLIENT_SECRET, Environment.UserName);


            // connect with a Service Account
            //string ServiceAccountEmail = "1046123799103-6v9cj8jbub068jgmss54m9gkuk4q2qu8@developer.gserviceaccount.com";
            //string serviceAccountkeyFile = @"C:\GoogleDevelop\Diamto Test Everything Project-78049f608668.p12";
            //DriveService service = Authentication.AuthenticateServiceAccount(ServiceAccountEmail, serviceAccountkeyFile);

            if (service == null)
            {
                Console.WriteLine("Authentication error");
                Console.ReadLine();
            }


            try
            {

                // Listing files with search.  
                // This searches for a directory with the name DiamtoSample
                string Q = "title = 'DiamtoSample' and mimeType = 'application/vnd.google-apps.folder'";
                IList<File> _Files = DaimtoGoogleDriveHelper.GetFiles(service, Q);

                foreach (File item in _Files)
                {
                    Console.WriteLine(item.Title + " " + item.MimeType);
                }

                // If there isn't a directory with this name lets create one.
                if (_Files.Count == 0)
                {
                    _Files.Add(DaimtoGoogleDriveHelper.createDirectory(service, "DiamtoSample", "DiamtoSample", "root"));
                }

                // We should have a directory now because we either had it to begin with or we just created one.
                if (_Files.Count != 0)
                {

                    // This is the ID of the directory 
                    string directoryId = _Files[0].Id;

                    //Upload a file
                    File newFile = DaimtoGoogleDriveHelper.uploadFile(service, @"c:\GoogleDevelop\dummyUploadFile.txt", directoryId);
                    // Update The file
                    File UpdatedFile = DaimtoGoogleDriveHelper.updateFile(service, @"c:\GoogleDevelop\dummyUploadFile.txt", directoryId, newFile.Id);
                    // Download the file
                    DaimtoGoogleDriveHelper.downloadFile(service, newFile, @"C:\GoogleDevelop\downloaded.txt");
                    // delete The file
                    FilesResource.DeleteRequest request = service.Files.Delete(newFile.Id);
                    request.Execute();
                }

                // Getting a list of ALL a users Files (This could take a while.)
                _Files = DaimtoGoogleDriveHelper.GetFiles(service, null);

                foreach (File item in _Files)
                {
                    Console.WriteLine(item.Title + " " + item.MimeType);
                }
            }
            catch (Exception ex)
            {

                int i = 1;
            }

            Console.ReadLine();
        }
        
    }

    */
}
