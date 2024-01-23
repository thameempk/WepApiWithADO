using Microsoft.Data.SqlClient;

namespace ADOExample.Models
{
    public class CollegeRep : ICollegeRep
    {  
        private readonly IConfiguration _configuration;
        private  string ConnectionSring { get; set; } 
        public CollegeRep(IConfiguration configuration) 
        {
            _configuration = configuration;
            ConnectionSring = _configuration["ConnectionString:DefaultConnection"];
        }
       




        public List<College> GetColleges()
        {
            using (SqlConnection con = new SqlConnection(ConnectionSring))
            {
              
                SqlCommand sqlCommand = new SqlCommand("select * from college", con);
                con.Open();
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                List<College> colleges = new List<College>();
                while (sdr.Read())
                {
                    College college = new College();
                    college.Id = Convert.ToInt32(sdr["id"]);
                    college.Name = sdr["Name"].ToString();
                    college.Place = sdr["place"].ToString();
                    colleges.Add(college);
                }
                return colleges;
            }
           
        }

        public List<College> GetCollegeById(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionSring))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from college where id = {id}", con);
                con.Open();
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                List<College> colleges = new List<College>();
                while (sdr.Read())
                {
                    College college = new College();
                    college.Id = Convert.ToInt32(sdr["id"]);
                    college.Name = sdr["Name"].ToString();
                    college.Place = sdr["place"].ToString();
                    colleges.Add(college);

                }
                return colleges;
            }
        }

        public void AddCollege(College college)
        {
            using (SqlConnection con = new SqlConnection(ConnectionSring))
            {
                
                SqlCommand sqlCommand = new SqlCommand("insert into college (Id, Name, Place) values (@id, @name, @place)", con);
                sqlCommand.Parameters.AddWithValue("@id", college.Id);
                sqlCommand.Parameters.AddWithValue("@name", college.Name);
                sqlCommand.Parameters.AddWithValue("@place", college.Place);
                con.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateCollege(int id, College college)
        {
            using (SqlConnection con = new SqlConnection(ConnectionSring))
            {
                SqlCommand sqlCommand = new SqlCommand("update college set Name = @name , place = @place where id = @id", con);
                sqlCommand.Parameters.AddWithValue("@name", college.Name);
                sqlCommand.Parameters.AddWithValue("@place", college.Place);
                sqlCommand.Parameters.AddWithValue("@id", id);
                con.Open();
                sqlCommand.ExecuteNonQuery();

            }
        }

        public void DeleteCollege(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionSring))
            {
                SqlCommand command = new SqlCommand("delete from college where id = @id", con);
                command.Parameters.AddWithValue("@id", id);
                con.Open();
                command.ExecuteNonQuery();
            }
        }



    }
}
