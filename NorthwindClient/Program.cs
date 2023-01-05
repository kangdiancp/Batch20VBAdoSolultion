using Microsoft.Extensions.Configuration;
using NorhtwindVbNetApi;
using System;
using System.Runtime.InteropServices;

namespace NorthwindClient // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static IConfigurationRoot Configuration;

        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            BuildConfig();

            INorthwindVbApi _northwindVbApi = new NorthwindVbApi(Configuration.GetConnectionString("NorthWindDS"));

            //_northwindVbApi.SayHello();
            var listRegion = _northwindVbApi.RepositoryManager.Region.FindAllRegion();

            foreach (var item in listRegion)
            {
                Console.WriteLine($"{item}");
            }


            var regionById = _northwindVbApi.RepositoryManager.Region.FindRegionById(18);
            Console.WriteLine($"Found region : {regionById}");

            //create region
            /*var newRegion = _northwindVbApi.RepositoryManager.Region.CreateRegion(new NorhtwindVbNetApi.Model.Region
            {
                RegionId = 32,
                RegionDescription = "Sentul Limboto"
            });

            Console.WriteLine($"New Region :  {newRegion}");*/

            //update region
            var updateRegion = _northwindVbApi.RepositoryManager.Region.UpdateRegionById(32, "Sentul Medi", true);
            var regionUpdateResult = _northwindVbApi.RepositoryManager.Region.FindRegionById(32);
            Console.WriteLine($"{regionUpdateResult}");

            //delete region
            var rowDelete = _northwindVbApi.RepositoryManager.Region.DeleteRegion(31);
            Console.WriteLine($"Delete region row : {rowDelete}");


            var updateRegionBySp = _northwindVbApi.RepositoryManager.Region.UpdateRegionById(32, "Bootcamp CodeId", true);
            var updateRegionSpResult = _northwindVbApi.RepositoryManager.Region.FindRegionById(32);
            Console.WriteLine($"{updateRegionSpResult}");

            Console.WriteLine("----------- Async ---------------");

            var listRegionAsyn = await _northwindVbApi.RepositoryManager.Region.FindAllRegionAsync();

            foreach (var item in listRegionAsyn)
            {
                Console.WriteLine($"{item}");
            }
        }

        static void BuildConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            
            Configuration = builder.Build();

            Console.WriteLine(Configuration.GetConnectionString("NorthwindDS"));

        }

    }
}