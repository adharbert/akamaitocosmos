using Domain.Models;
using Domain.Models.Akamai;
using Newtonsoft.Json;

namespace Persistence.Repositories
{

    public class ApiAkamaiRepository : IRepository
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public ApiAkamaiRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> CountAsync()
        {
            int result = 0;
            string methodCall = "/entity.count?type_name=user";
            HttpCountParam hcp = new HttpCountParam() { type_name = "user" };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(hcp));
            var client = _httpClientFactory.CreateClient("SourceAPI");
            HttpResponseMessage response = await client.PostAsync(methodCall, content);

            if (response != null && response.IsSuccessStatusCode)
            {
                //var parseResult = JsonConvert.DeserializeObject<string>(response?.Content.ToString()) ?? "0";
                var parseResult = JsonConvert.DeserializeObject<AkamaiUserTotalCount>(response.Content.ReadAsStringAsync().Result);
                result = parseResult.total_count;
               
            }

            return result;
        }


        public async Task<IEnumerable<AkamaiUser>> GetAllUsersAsync(string startingId, int totalRecordCount)
        {
            int batchCount = 400;
            string startId = startingId;
            int errorCount = 0;
            int resultCount = batchCount;
            if (batchCount > totalRecordCount) {
                resultCount = totalRecordCount;
            }
            List<AkamaiUser> results = new List<AkamaiUser>();

            string methodCall = "/entity.find?type_name=user&filter=id>'{0}'&max_results={1}&timeout=120&sort_on=[\"id\"]";
            do
            {
                try {                
                    var client = _httpClientFactory.CreateClient("SourceAPI");
                    var response = await client.GetAsync(string.Format(methodCall, startId, resultCount));
                    if (response != null && response.IsSuccessStatusCode) {
                        var userRecord = JsonConvert.DeserializeObject<AkamaiResponse>(response.Content.ReadAsStringAsync().Result);
                        if (userRecord.result_count == 0) {
                            startId = (int.Parse(startId) + resultCount).ToString();
                        } else {
                            results.AddRange(userRecord.results);
                            startId = userRecord.results.Max(x => x.Id).ToString();
                        }
                    }
                }
                catch (Exception ex) {
                    // if this keeps throwing errors, this will stop the while loop.
                    if (errorCount++ > 30)
                        break;
                    Console.WriteLine("Error thrown - {0}", ex.Message.ToString());
                }
            } while (results.Count < totalRecordCount);


            return results;

        }

        public Task<AkamaiUser> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }


        public Task InsertAsync(AkamaiUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AkamaiUser user)
        {
            throw new NotImplementedException();
        }



        public Task Delete(AkamaiUser user)
        {
            throw new NotImplementedException();
        }



        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

    }
}
