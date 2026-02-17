using Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Data;

public class RatingRepository : IRatingRepository
{
    private readonly string _connectionString;

    public RatingRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task AddRatingAsync(Rating rating)
    {
        string query = @"INSERT INTO RATING 
                        (HOST, METHOD, PATH, REFERER, USER_AGENT, Record_Date) 
                        VALUES 
                        (@Host, @Method, @Path, @Referer, @UserAgent, @RecordDate)";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.Add("@Host", SqlDbType.NVarChar).Value =
                (object?)rating.Host ?? DBNull.Value;

            command.Parameters.Add("@Method", SqlDbType.NVarChar).Value =
                (object?)rating.Method ?? DBNull.Value;

            command.Parameters.Add("@Path", SqlDbType.NVarChar).Value =
                (object?)rating.Path ?? DBNull.Value;

            command.Parameters.Add("@Referer", SqlDbType.NVarChar).Value =
                (object?)rating.Referer ?? DBNull.Value;

            command.Parameters.Add("@UserAgent", SqlDbType.NVarChar).Value =
                (object?)rating.UserAgent ?? DBNull.Value;

            command.Parameters.Add("@RecordDate", SqlDbType.DateTime).Value =
                rating.RecordDate;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
