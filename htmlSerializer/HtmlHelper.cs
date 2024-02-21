using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace htmlSerializer
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public string[] LablesWithClosure { get; set; }
        public string[] LablesWithoutClosure { get; set; }

        private HtmlHelper()
        {
            string jsonContent = File.ReadAllText("HtmlTags.json");
            string jsonContent1 = File.ReadAllText("HtmlVoidTags.json");
            try
            {
                // Deserialize the JSON data into an array of strings
                LablesWithClosure = JsonSerializer.Deserialize<string[]>(jsonContent);
                LablesWithoutClosure = JsonSerializer.Deserialize<string[]>(jsonContent1);
            }
            catch (JsonException ex)
            {
                // Handle any JSON deserialization errors
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }

            
        }
    }
}
