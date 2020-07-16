using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VintriTechnologies.Dto;
using VintriTechnologies.Helper;

namespace VintriTechnologies.Services
{
    public class PunkApiService: IPunkApiService
    {        
        public async Task<List<Beer>> GetBeers(string Url)
        {
            
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(Url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsAsync<JArray>();
                List<Beer> result = Beer.FromJson(responseBody.ToString());

                return result;            

        }
        public List<BeerRatings> GetBeersRatings(string path)
        {
            List<BeerRatings>  beerRatings = new List<BeerRatings>();
            var json = JsonHelper.Get(path);
            if (!string.IsNullOrEmpty(json))
            {
                beerRatings = BeerRatings.FromJson(json);

            }
            return beerRatings;
        }

        public string AddBeerRating(List<BeerRatings> beerRatings,string path)
        {
            bool flag = false;
            string json = Serialize.ToJson(beerRatings);
            flag = JsonHelper.Add(json,path);
            if (flag)
                return "success";
            else
                return "Failure";            
        }
    }
}
