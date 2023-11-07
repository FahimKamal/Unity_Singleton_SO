using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class StringData
{
    public StringData(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public string Key { get; set; }
    public string Value { get; set; }

}

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private string dbName = "Database";
    [SerializeField] private string tableName = "StringValues";

    public string DbName { get => dbName; set => dbName = value; }

    public void InsertInto<T>(string tableName, string key, T value){
        var dataBaseLocation = SetDataBaseClass.SetDataBase(DbName + ".db");

        using (var dbConnection = new SqliteConnection(dataBaseLocation)){
            dbConnection.Open();
            using (var dbCommand = dbConnection.CreateCommand()){
                var sqlQuery = "INSERT OR REPLACE INTO " + tableName + " (Key, Value) VALUES ('"+key+"', '"+value+"');";
                dbCommand.CommandText = sqlQuery;
                using (var reader = dbCommand.ExecuteReader()){
                    while (reader.Read()){
                        
                    }
                    reader.Close();
                }
                dbCommand.Dispose();
            }
            dbConnection.Close();
        }
    }
    
    public bool FindItem(string key, out string value){ 
        value = null;
        var dataBaseLocation = SetDataBaseClass.SetDataBase(DbName + ".db");

        using (var dbConnection = new SqliteConnection(dataBaseLocation)){
            dbConnection.Open();
            using (var dbCommand = dbConnection.CreateCommand()){
                var sqlQuery = "SELECT Value FROM " + tableName + " WHERE Key = '"+key+"';";
                dbCommand.CommandText = sqlQuery;
                using (var reader = dbCommand.ExecuteReader()){
                    while (reader.Read()){
                        value = reader["Value"].ToString();
                    }
                    reader.Close();
                }
                dbCommand.Dispose();
            }
            dbConnection.Close();
        }
        
        return value != null;
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
        // CreateDB();
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

