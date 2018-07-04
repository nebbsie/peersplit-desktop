using System;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;
using peersplit_desktop.Model;
using peersplit_desktop.Model.APIResponse;

namespace peersplit_desktop.Controller
{
    public static class HolderAPIController
    {
        /// <summary>
        ///  Registers the storage holder into the database.
        /// </summary>
        public async static Task<HolderCreateResponse> RegisterHolder(int id, string _holderName, int bytesAvailible)
        {
            try
            {
                // Convert gigabytes into bytes.
                long bytes = (bytesAvailible * 1024) * 1024;

                // Call the login api.
                var res = await("http://localhost:3000/holder/create")
                    .PostUrlEncodedAsync(new { ownerID = id, holderName = _holderName, bytesAvailable = bytes })
                    .ReceiveString();

                HolderCreateResponse filmRes = JsonConvert.DeserializeObject<HolderCreateResponse>(res);
                return filmRes;
            }
            catch
            {
                Console.WriteLine("Failed to create holder");
                return null;
            }
        }

        /// <summary>
        ///  Updates the holders last online time in the database.
        /// </summary>
        public async static void UpdateHolder(int id)
        {
            try
            {
                // Call the  api.
                var res = await ("http://localhost:3000/holder/update").PostUrlEncodedAsync(new { holderID = id }).ReceiveString();
            }
            catch
            {
                Console.WriteLine("Failed to create holder");
            }
        }
    }
}
