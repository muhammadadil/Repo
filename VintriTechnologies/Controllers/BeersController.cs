using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VintriTechnologies.Dto;
using VintriTechnologies.Models;
using VintriTechnologies.Services;

namespace VintriTechnologies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {        
        public List<BeerRatings> beerRatings;
        private readonly IPunkApiService _punkApiService;
        private readonly Config _conf;        
        public static string path = string.Empty;

        public BeersController(IPunkApiService punkApiService, Config conf)
        {
            _punkApiService = punkApiService;
            _conf = conf;           
            path = Path.Combine(Environment.CurrentDirectory, _conf.DBFolderName, _conf.JsonFileName);
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<string>> GetBeers(string name)
        {
            try
            {
                
                var bears = await _punkApiService.GetBeers(_conf.PunkAPIUrl);
                var beerRatings = _punkApiService.GetBeersRatings(path);
                if (bears.Count() > 0 && beerRatings.Count() >0)
                {
                    var result = from b in bears
                                 join r in beerRatings on b.Id equals r.Id
                                 where b.Name.ToLower() == name.ToLower()
                                 select new
                                 {
                                     Id = b.Id
                                            ,
                                     Name = b.Name
                                            ,
                                     Description = b.Description
                                            ,
                                     UserRatings = r.UserRatings
                                 };

                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
                
        [HttpPost("{id}")]
        public async Task<ActionResult<string>> AddBeer([FromBody] UserRating value, int id)
        {
            try {
                string json = string.Empty;
                List <Beer> bears= await _punkApiService.GetBeers(_conf.PunkAPIUrl);
                var result = bears.Where(x => x.Id == id);
                if (result == null)
                    return "Beer Id not exist";

               beerRatings = new List<BeerRatings>();

                BeerRatings beerRating = new BeerRatings();
                beerRating.UserRatings = new List<UserRating>();

                beerRatings = _punkApiService.GetBeersRatings(path);
                
                if (beerRatings != null)
                {                    
                    beerRating = beerRatings.Where(x => x.Id == id).FirstOrDefault();
                    if (beerRating != null)
                    {
                        beerRating.UserRatings.Add(value);                        
                    }
                    else
                    {
                        beerRating = new BeerRatings();
                        beerRating.UserRatings = new List<UserRating>();
                        beerRating.Id = id;
                        beerRating.UserRatings.Add(value);
                        beerRatings.Add(beerRating);
                    }
                }                
                else
                {
                    beerRating.Id = id;
                    beerRating.UserRatings.Add(value);
                    beerRatings.Add(beerRating);
                }

                json = _punkApiService.AddBeerRating(beerRatings, path);
                
                return Ok(json);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
