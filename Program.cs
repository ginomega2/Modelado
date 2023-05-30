using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Modelado.Data;
using Modelado.Models;

namespace Modelado;


class Program
{
    static void Main(string[] args)
    {

        // string conectionString = "Server=localhost;Database=DotNetCourseDatabase;Trusted_Connection=true;TrustServerCertificate=true";
        // IDbConnection dbConnection = new SqlConnection(conectionString);

        IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        DataContextDapper dapper = new DataContextDapper(config);
        DataContextEF entityFramework = new DataContextEF(config);


        DateTime ahora = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

        Console.WriteLine(ahora.ToString());

        Computer computer = new Computer()
        {
            Motherboard = "configuration",
            CPUCores = 4,
            HasLTE = true,
            HasWifi = true,
            Price = 1000.12m,
            ReleaseDate = DateTime.Now,
            VideoCard = "config Video card"
        };

        entityFramework.Add(computer);
        entityFramework.SaveChanges();

        string sql = @"INSERT INTO TutorialAppSchema.Computer(
            Motherboard,
            HasWifi,
            HasLTE,
            ReleaseDate,
            Price,
            VideoCard
        )  VALUES('" + computer.Motherboard
            + "','" + computer.HasWifi
            + "','" + computer.HasLTE
            + "','" + computer.ReleaseDate
            + "','" + computer.Price
            + "','" + computer.VideoCard

        + "')";

        Console.WriteLine(sql);
        bool result = dapper.ExecuteSql(sql);

        Console.WriteLine(result);

        string sqlSelect = @"
        SELECT
            Computer.ComputerId,
            Computer.Motherboard,
            Computer.HasWifi,
            Computer.HasLTE,
            Computer.ReleaseDate,
            Computer.Price,
            Computer.VideoCard
            FROM TutorialAppSchema.Computer";


        IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
        // List<Computer> computers =  dbConnection.Query<Computer>(sqlSelect).ToList();
        Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");


        foreach (Computer computer1 in computers)
        {
            Console.WriteLine("'" + computer1.ComputerId
            + "','" + computer1.Motherboard
            + "','" + computer1.HasWifi
            + "','" + computer1.HasLTE
            + "','" + computer1.ReleaseDate
            + "','" + computer1.Price
            + "','" + computer1.VideoCard

        + "'");
        }

        IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();
        if (computersEF != null)
        {
            Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");
            foreach (Computer computer1 in computersEF)
            {
                Console.WriteLine("'" + computer1.ComputerId
             + "','" + computer1.Motherboard
             + "','" + computer1.HasWifi
             + "','" + computer1.HasLTE
             + "','" + computer1.ReleaseDate
             + "','" + computer1.Price
             + "','" + computer1.VideoCard

         + "'");
            }
        }


    }
}
