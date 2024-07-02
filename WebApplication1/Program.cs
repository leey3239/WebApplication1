namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
			private readonly string _connectionString;

			public StudentOptionsDB(string connectionString)
			{
				_connectionString = connectionString;
			}

			// All database calls are being made asynchronously
			// This is important as a web service will service many requests but will have a limited number of threads to use
			// This means that while the calls are being made threads can be used on other calls
			public async Task<List<StudentDto>> GetHouseInfoAsync()
			{
				List<StudentDto> result = new ();
				using (SqlConnection connection = new (_connectionString))
				{
					connection.Open();
					SqlCommand cmd = connection.CreateCommand();
					cmd.CommandText = "SELECT Student from dbo.StudentOptions";
					SqlDataReader reader = await cmd.ExecuteReaderAsync();
					while (reader.Read())
					{
						result.Add(GetStudent(reader));
					}
				}
				return result;
			}
        }
    }
}