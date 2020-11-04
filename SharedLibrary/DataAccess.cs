using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SharedLibrary
{
    public class DataAccess
    {
        private static readonly string connectionString = @"";

        public static async Task<Settings> ReadJson()
        {
            var jsonFile = @"C:\Users\johan\Pictures\test.json";
            StorageFile file = await StorageFile.GetFileFromPathAsync(jsonFile);

            string text = await Windows.Storage.FileIO.ReadTextAsync(file);
            var obj = JsonConvert.DeserializeObject<dynamic>(text);

            Settings settings = new Settings(Convert.ToInt32(obj.NumberOfItems), Convert.ToString(obj.Status1), Convert.ToString(obj.Status2), Convert.ToString(obj.Status3));

            return settings;
        }
        
        public static async Task AddAsync(Customer customer, SupportCases supportcase)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) from customer where SSNumber = @SSN", conn))
                {
                    conn.Open();
                    sqlCommand.Parameters.AddWithValue("@SSN", customer.SSNumber);
                    int userCount = (int)sqlCommand.ExecuteScalar();

                    if (userCount > 0)
                    {
                        var query = @"INSERT INTO SupportCases (CustomerNumber, Status, Description, Title, Category, Time) 
                                    VALUES(@CustomerNumber, @Status, @Description, @Title, @Category, @Time)";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@CustomerNumber", customer.SSNumber);
                        cmd.Parameters.AddWithValue("@Status", supportcase.Status);
                        cmd.Parameters.AddWithValue("@Description", supportcase.Description);
                        cmd.Parameters.AddWithValue("@Title", supportcase.Title);
                        cmd.Parameters.AddWithValue("@Category", supportcase.Category);
                        cmd.Parameters.AddWithValue("@Time", supportcase.Time);

                        await cmd.ExecuteReaderAsync();
                        conn.Close();
                    }

                    else
                    {
                        var query = @"INSERT INTO Customer (SSNumber, Name, PhoneNumber, Email) 
                                    VALUES(@SSNumber, @Name, @PhoneNumber, @Email)
                                    INSERT INTO SupportCases (CustomerNumber, Status, Description, Title, Category, Time) 
                                    VALUES(@CustomerNumber, @Status, @Description, @Title, @Category, @Time)";


                        SqlCommand cmd = new SqlCommand(query, conn);


                        cmd.Parameters.AddWithValue("@CustomerNumber", customer.SSNumber);
                        cmd.Parameters.AddWithValue("@Status", supportcase.Status);
                        cmd.Parameters.AddWithValue("@Description", supportcase.Description);
                        cmd.Parameters.AddWithValue("@Title", supportcase.Title);
                        cmd.Parameters.AddWithValue("@Category", supportcase.Category);
                        cmd.Parameters.AddWithValue("@Time", supportcase.Time);

                        cmd.Parameters.AddWithValue("@SSNumber", customer.SSNumber);
                        cmd.Parameters.AddWithValue("@Name", customer.Name);
                        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);


                        await cmd.ExecuteReaderAsync();
                        conn.Close();
                    }
                }
            }

        }

        public static List<CaseList> GetAll(string status1, string status2)
        {
            Customer customer = new Customer();
            SupportCases supportcases = new SupportCases();
            var SampleList = new List<CaseList>();

            var task = Task.Run(async () => await ReadJson());
            var number = task.Result.NumberOfItems;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "SELECT TOP (@Limit) * FROM SupportCases INNER JOIN Customer ON Customer.SSNumber = SupportCases.CustomerNumber WHERE SupportCases.Status IN (@Status1, @Status2) ORDER BY SupportCases.CaseNumber DESC;";


                SqlCommand cmd = new SqlCommand(query, conn);               
                cmd.Parameters.AddWithValue("@Limit", number);
                cmd.Parameters.AddWithValue("@Status1", status1);
                cmd.Parameters.AddWithValue("@Status2", status2);

                var result = cmd.ExecuteReader();

                while (result.Read())
                {
                    int CaseNumber = result.GetInt32(0);
                    int CustomerNumber = result.GetInt32(1);
                    string Status = result.GetString(2);
                    string Description = result.GetString(3);
                    string Title = result.GetString(4);
                    string Category = result.GetString(5);
                    DateTime Time = result.GetDateTime(6);

                    int SSN = result.GetInt32(7);
                    string Name = result.GetString(8);
                    int PhoneNumber = result.GetInt32(9);
                    string Email = result.GetString(10);

                    customer = new Customer(SSN, Name, PhoneNumber, Email);
                    supportcases = new SupportCases(CaseNumber, CustomerNumber, Status, Description, Title, Category, Time);
                    SampleList.Add(new CaseList(customer, supportcases));
                }

                conn.Close();
                return SampleList;

            }
        }

        public static async Task UpdateAsync(int casenumber, string status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var query = "UPDATE SupportCases SET Status = @Status WHERE CaseNumber = @CaseNumber";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CaseNumber", casenumber);
                cmd.Parameters.AddWithValue("@Status", status);               
                


                await cmd.ExecuteReaderAsync();
                conn.Close();
            }

        }
    }
}
