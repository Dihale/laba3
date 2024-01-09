using System.Data.SQLite;
using System.Runtime.Serialization.Json;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace lab3;

public class Storage
{
    private readonly string _jsonFilePath =
        $"C:\\Users\\{Environment.UserName}\\RiderProjects\\Lab3\\Lab3\\bin\\Debug\\net7.0\\queries.json";

    private readonly string _xmlFilePath =
        $"C:\\Users\\{Environment.UserName}\\RiderProjects\\Lab3\\Lab3\\bin\\Debug\\net7.0\\queries.xml";

    private readonly string _sqliteDbPath =
        $"C:\\Users\\{Environment.UserName}\\RiderProjects\\Lab3\\Lab3\\bin\\Debug\\net7.0\\queries.db";

    public Storage()
    {
        if (!File.Exists(_jsonFilePath))
        {
            File.Create(_jsonFilePath).Close();
        }

        if (!File.Exists(_xmlFilePath))
        {
            File.Create(_xmlFilePath).Close();
        }

        if (!File.Exists(_sqliteDbPath))
        {
            using (var fileStream = File.Create(_sqliteDbPath))
            {
                fileStream.Close();
            }
            
            using (var connection = new SQLiteConnection($"Data Source={_sqliteDbPath};Version=3;"))
            {
                connection.Open();
                connection.Execute(
                    "CREATE TABLE IF NOT EXISTS Queries (Operation int, Input TEXT, Result TEXT)");
            }
        }
    }

    public void SaveToJson(List<QueryInformation> queries)
    {
        var json = JsonConvert.SerializeObject(queries, (Newtonsoft.Json.Formatting)Formatting.Indented);
        File.WriteAllText(_jsonFilePath, json);
    }

    public List<QueryInformation> LoadFromJson()
    {
        if (File.Exists(_jsonFilePath))
        {
            var json = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<List<QueryInformation>>(json);
        }

        return new List<QueryInformation>();
    }

    public List<QueryInformation> LoadFromXML()
    {
        if (!File.Exists(_xmlFilePath)) return new List<QueryInformation>();
        using var reader = XmlReader.Create(_xmlFilePath);
        var ser = new DataContractJsonSerializer(typeof(List<QueryInformation>));
        return (List<QueryInformation>)ser.ReadObject(reader);

    }

    public void SaveToXml(List<QueryInformation> queries)
    {
        using var writer = XmlWriter.Create(_xmlFilePath);
        var ser = new DataContractJsonSerializer(typeof(List<QueryInformation>));
        ser.WriteObject(writer, queries);
    }

    public void SaveToSqLite(List<QueryInformation> queries)
    {
        using var connection = new SQLiteConnection($"Data Source={_sqliteDbPath};Version=3;");
        connection.Open();
        connection.Execute("DELETE FROM queries");

        foreach (var queryInformation in queries)
        {
            connection.Execute(
                "INSERT INTO Queries (Operation, Input, Result) VALUES (@Operation, @Input, @Result)",
                new
                {
                    Operation = queryInformation.IndexOperation,
                    Input = queryInformation.Input,
                    Result = queryInformation.Result,
                });
        }
    }

    public List<QueryInformation> LoadFromSqLite()
    {
        using var connection = new SQLiteConnection($"Data Source={_sqliteDbPath};Version=3;");
        connection.Open();
        var queries = connection.Query("SELECT * FROM queries").AsList();

        return queries.Select(queryInformation => new QueryInformation(queryInformation.Operation, queryInformation.Description,
            queryInformation.Result)).ToList();
    }
}