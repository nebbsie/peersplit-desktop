using System;
using System.Threading.Tasks;
using peersplit_desktop.Model;
using peersplit_desktop.Model.APIResponse;
using Flurl.Http;
using Newtonsoft.Json;

namespace peersplit_desktop.Controller
{
    public static class FileAPIController
    {
        /// <summary>
        /// Get all of the files the user has uploaded and present them on screen.
        /// </summary>
        public static async Task<FilmResponse> GetAllFilesInNetwork(int id)
        {
            try
            {
                // Call the login api.
                var res = await ("http://localhost:3000/file/getAll")
                    .PostUrlEncodedAsync(new { ownerID = id })
                    .ReceiveString();

                FilmResponse filmRes = JsonConvert.DeserializeObject<FilmResponse>(res);
                // 
                return filmRes;
            }
            catch
            {
                Console.WriteLine("Failed to get files");
                return null;
            }

        }

        /// <summary>
        /// Upload the file to the network.
        /// </summary>
        public static async Task<bool> UploadFile(int id, string fileLocation )
        {
            try
            {
                var res = await ("http://localhost:3000/file/new")
                    .PostMultipartAsync(mp => mp
                    .AddStringParts(new { ownerID = id })
                    .AddFile("file", fileLocation)
                    ).ReceiveString();

                UploadResponse json = JsonConvert.DeserializeObject<UploadResponse>(res);

                if (json.success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
