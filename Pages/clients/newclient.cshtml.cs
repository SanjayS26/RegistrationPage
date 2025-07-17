using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace RegistrationPage.Pages.clients
{
    public class newclientModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.Name = Request.Form["name"];
            clientInfo.Email = Request.Form["email"];
            clientInfo.Phone = Request.Form["phone"];
            

            if (clientInfo.Name.Length == 0 || clientInfo.Email.Length == 0 ||
                clientInfo.Phone.Length == 0 )
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-QA1DMAN\\SQLEXPRESS01;Initial Catalog=sanjay;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients1" + "(name, email, phone) VALUES"
+ "(@name, @email,@phone)";                 
                
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.Name);
                        command.Parameters.AddWithValue("@email", clientInfo.Email);
                        command.Parameters.AddWithValue("@phone", clientInfo.Phone);

                        command.ExecuteNonQuery();



                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.Name = ""; clientInfo.Email = ""; clientInfo.Phone = "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Clients/Index");
        }

    }
}
