using System.Collections.Generic;
using System.Threading.Tasks;
using VintriTechnologies.Dto;

namespace VintriTechnologies.Services
{
    public interface IPunkApiService
    {
        Task<List<Beer>> GetBeers(string Url);
        List<BeerRatings> GetBeersRatings(string path);
        string AddBeerRating(List<BeerRatings> beerRatings, string path);


    }
}
