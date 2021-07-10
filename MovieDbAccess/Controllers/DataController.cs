using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDbAccess.Domain.Interfaces;

namespace MovieDbAccess.Controllers
{
    [Route("[controller]")]
    public class DataController : Controller
    {
        private readonly IDataProcessor _dataProcessor;
        public DataController(IDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;
        }

        [HttpGet]
        [Route("{apphash}")]
        public async Task<IActionResult> GetUserAsync(string apphash)
        {
            User user = await _dataProcessor.GetUserAsync(apphash);
            return Ok(user);
        }

        [HttpGet]
        [Route("{uid}/movie")]
        public async Task<IActionResult> GetMovieAsync(int uid)
        {
            Movie movie =  await _dataProcessor.GetMovieAsync(uid);
            return Ok(movie);
        }

        [HttpPost]
        [Route("{uid}/{movieID}")]
        public async Task<IActionResult> AddMovieAsync(int uid, string movieID)
        {           
            int result = await _dataProcessor.StoreMovieAsync(uid, movieID);
            if(result == 1)
                return Accepted();
            else
                return BadRequest();
        }
        [HttpDelete]
        [Route("{uid}/{movieID}")]
        public async Task<IActionResult> DeleteMovieAsync(int uid, string movieID)
        {           
            int result = await _dataProcessor.DeleteMovieAsync(uid, movieID);        
            if(result == 1)
                return Ok();
            else
                return NotFound();
        }
    }
}