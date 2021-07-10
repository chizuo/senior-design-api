using MovieDbAccess.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data;
using Dapper;
using MovieDbAccess.Persistence;

namespace MovieDbAccess
{
    public class DataProcessor : IDataProcessor
    {
        public async Task<User> GetUserAsync(string appHash)
        {
            User user = new User();
            using (IDbConnection conn = new MySqlConnection(Connection.ConnectionString))
            {
                int userExists = conn.QueryFirstOrDefault<int>("CheckUser",
                    new {app_hash = appHash},
                    commandType: CommandType.StoredProcedure);
                if(userExists == 1)
                {
                    await Task.Run(()=> {
                        Console.WriteLine("Retrieved User");
                        var results = conn.Query<User>("GetUser",
                            new {app_hash = appHash},
                            commandType: CommandType.StoredProcedure);
                        user = results.FirstOrDefault();
                    });
                } else {
                    await CreateUserAsync(appHash, conn);
                    user = GetUserAsync(appHash).Result;
                }
            }
            return user;
        }        
        public Task CreateUserAsync(string appHash, IDbConnection conn)
        {
            return Task.Run(()=> {
                conn.Query("NewUser",
                    new {app_hash = appHash},
                    commandType: CommandType.StoredProcedure);
                    Console.WriteLine("Created New User");
            });
        }
        public async Task<Movie> GetMovieAsync(int uID)
        {            
            Movie movie =  new Movie();
            using(IDbConnection conn = new MySqlConnection(Connection.ConnectionString))
            {
                await Task.Run(() => {
                    movie = conn.QueryFirstOrDefault<Movie>("GetMovie",
                        new {user_id = uID},
                        commandType: CommandType.StoredProcedure);
                    Console.WriteLine("Movie Retrieved");
                });
            }
            return movie;
        }
        public async Task<int> StoreMovieAsync(int uID, string movieID)
        {
            int result = 0;
            using(IDbConnection conn = new MySqlConnection(Connection.ConnectionString))
            {
                await Task.Run(() => {
                   result =  conn.Execute("NewMovie",
                        new {user_id = uID, imdb = movieID},
                        commandType: CommandType.StoredProcedure);
                });
                Console.WriteLine("Added New Movie");
            }
            return result;
        }
        public async Task<int> DeleteMovieAsync(int uID, string movieID)
        {
            int result = 0;
            using(IDbConnection conn = new MySqlConnection(Connection.ConnectionString)){
                await Task<int>.Run(() => {
                    result = conn.Execute("DeleteMovie",
                        new {user_id = uID, imdb = movieID},
                        commandType: CommandType.StoredProcedure);
                    Console.WriteLine("Movie Removed");
                });
            }
           return result;
        }
    }
}