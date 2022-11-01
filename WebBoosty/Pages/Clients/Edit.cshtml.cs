using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebBoosty.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            Console.WriteLine();
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=MUSABII;Initial Catalog=some;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"SELECT * FROM clients where id = {id}";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientInfo.id = ""+ reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }

        public void OnPost()
        {
            string id = Request.Query["id"];

            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.phone.Length == 0  || clientInfo.address.Length == 0)
            {
                errorMessage = "All field are required";
                return;
            }

            //save the new client

            try
            {
                string connectionString = "Data Source=MUSABII;Initial Catalog=some;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE clients " +
                        "SET name = @name,email= @email,phone=@phone,address=@address "+
                        "WHERE id = @id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            //clientInfo.name = "";clientInfo.email = "";clientInfo.phone = "";clientInfo.address = "";
            clientInfo = new ClientInfo();
            successMessage = "New client added correctly";
            Response.Redirect("/Clients/Index");

        }
    }
}
