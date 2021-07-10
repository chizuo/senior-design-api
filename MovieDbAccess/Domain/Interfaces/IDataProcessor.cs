using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace MovieDbAccess.Domain.Interfaces
{
    public interface IDataProcessor
    {
        Task<User> GetUserAsync(string apphash);
        //Task<string> CreateUserAsync(string apphash);   
        Task<Movie> GetMovieAsync(int uid);
        Task<int> StoreMovieAsync(int uid, string movieID);     
        Task<int> DeleteMovieAsync(int uid, string movieID);
    }
}
