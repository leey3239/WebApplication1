using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlClient;

public class StudentDB
{
	public StudentDB()
	{
		private readonly string _connectionString;

		public StudentDB(string connectionString)
		{
			_connectionString = connectionString;
		}

		// All database calls are being made asynchronously
		// This is important as a web service will service many requests but will have a limited number of threads to use
		// This means that while the calls are being made threads can be used on other calls
		public async Task<List<HouseDto>> GetHouseInfoAsync()
		{
			List<HouseDto> result = new ();
			using (SqlConnection connection = new (_connectionString))
			{
				connection.Open();
				SqlCommand cmd = connection.CreateCommand();
				cmd.CommandText = "SELECT HouseName, UndermasterFirstName, EventsCoordinator from dbo.House";
				SqlDataReader reader = await cmd.ExecuteReaderAsync();
				while (reader.Read())
				{
					result.Add(GetHouse(reader));
				}
			}
			return result;
		}
	}
}
