using NUnit.Framework;
using System.Threading.Tasks;
using VintriTechnologies.Controllers;
using VintriTechnologies.Dto;
using VintriTechnologies.Models;
using VintriTechnologies.Services;
namespace Tests
{
    public class BeersTest
    {

        public IPunkApiService _punkApiService;
        public Config _conf;

        [SetUp]
        public void Setup()
        {
            _punkApiService = new PunkApiService();
            _conf = new Config();
            _conf.PunkAPIUrl = "https://api.punkapi.com/v2/beers";
            _conf.DBFolderName = "Data";
            _conf.JsonFileName = "database.json";            
        }

        [Test]
        public async Task GetBeersByName()
        {           
            BeersController beersController = new BeersController(_punkApiService, _conf);
            var result = await beersController.GetBeers("buzz");
            Assert.IsNotNull(result.Result);
        }

        [Test]
        public async Task AddBeersRating()
        {
            int Id = 2;
            UserRating userRating = new UserRating();
            userRating.Username = "john.doe@fictitious.com";
            userRating.Rating =3;
            userRating.Comments = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            BeersController beersController = new BeersController(_punkApiService, _conf);
            var result = await beersController.AddBeer(userRating,Id);
            Assert.IsNotNull(result);
        }
    }
}