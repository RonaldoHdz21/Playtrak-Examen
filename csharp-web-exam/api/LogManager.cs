using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace api
{
    public class LogManager
    {
        private readonly IConfiguration _configuration;
        public LogManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void AddLog(string message, string method, string type)
        {
            try
            {
                DateTime date = DateTime.Today;
                string pathLog = _configuration["PathLog"];
                if (!Directory.Exists(pathLog))
                    Directory.CreateDirectory(pathLog);
                string nameFile = $"Log_{date.ToString("yyyy-MM-dd")}.txt";
                pathLog = pathLog + nameFile;
                
                if (!File.Exists(pathLog))
                {
                    var stream = File.Create(pathLog);
                    stream.Close();
                }
                    

                File.AppendAllText(pathLog, $"{DateTime.Now} - {type} - {method} - {message}\n");
            }
            catch 
            { 

            }

        }
    }
}
