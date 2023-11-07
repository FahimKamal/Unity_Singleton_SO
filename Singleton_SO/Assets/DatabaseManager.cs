using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;

public class StringData
{
    public StringData(string key, string value)
    {
        this.Key = key;
        Value = value;
    }

    public string Key { get; set; }
    public string Value { get; set; }

}

public class DatabaseManager : MonoBehaviour
{
    private string dbName;
    //private string dbName = "URL=file:Database.db";
    //private static string dbName ="/Database.db";
    private string tableName = "StringValues";

    public string DbName { get => dbName; set => dbName = value; }

    private void Awake()
    {
        DbName = Path.Combine(Application.persistentDataPath, "Database.db");
    }

    /// <summary>
    /// Method to create a table if it doesn't exist already
    /// </summary>
    private void CreateDB()
    {
        using(var connection = new SqliteConnection(DbName))
        {
            connection.Open();       

            using(var command = connection.CreateCommand())
            {
                // Create a table called `StringValues` with 2 fields: Key and Value
                command.CommandText = "CREATE TABLE IF NOT EXISTS "+ tableName +" (Key VARCHAR(50), Value TEXT,PRIMARY KEY(Key));";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }


    private void ReadAll()
    {
        var stringDataList = new List<StringData>();
        using(var connection = new SqliteConnection(DbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                // Select everything from database.
                command.CommandText = "SELECT * FROM " + tableName + ";";

                // Iterate through the recordset that was returned from the statement.
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var stringData = new StringData(reader["Key"].ToString(), reader["Value"].ToString());
                        stringDataList.Add(stringData);
                    }

                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    private void DisplayKeyValuePair(List<StringData> dataList)
    {
        foreach(var data in dataList)
        {
            Debug.Log("Key: " + data.Key + "\t" + "Value: " + data.Value + "\n");
        }
    }

    private void Start()
    {
        CreateDB();
        Debug.Log("Database Location: " + DbName);
        var tempList = new List<StringData>() { 
            new StringData("Fahim", "Kamal"), 
            new StringData("FK", "Ahmed"), 
            new StringData("Arif", "some"), 
            new StringData("Naim", "other"), 
            new StringData("Key", "lost"), 
        };

        DisplayKeyValuePair(tempList);
            
    }


}

