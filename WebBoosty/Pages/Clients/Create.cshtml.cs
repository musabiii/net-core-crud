using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebBoosty.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
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
                    string sql = "INSERT INTO clients " +
                        "(name,email,phone,address) VALUES "+
                        "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql,connection))
					{
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
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
