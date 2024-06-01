using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DatabaseManager
{
    private string dbName = "/PlaneARow.db";
    public void GetTable(string tableName)
    {
        IDbConnection dbConnection = Getconnection();
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        IDataReader reader = dbCommand.ExecuteReader();

        while (reader.Read())
        {
            //Debug.Log(reader.GetInt16(0));
        }
        dbConnection.Close();
    }

    public float GetMainOption(OptionType optionType,int level)
    {
        IDbConnection dbConnection = Getconnection();
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT {optionType} FROM MainOption WHERE Level = {level}";
        IDataReader reader = dbCommand.ExecuteReader();

        //Debug.Log(dbCommand.CommandText);
        //Debug.Log(reader.GetValue(0).ToString());
        float result = float.Parse(reader.GetValue(0).ToString());

        dbConnection.Close();

        return result;
    }
    public float GetAdditionalOption(OptionType optionType, EquipmentClass equipmentClass, string valueSize)
    {
        IDbConnection dbConnection = Getconnection();
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT {valueSize} FROM AdditionalOption WHERE Type = '{optionType}' AND Class = '{equipmentClass}'";
        IDataReader reader = dbCommand.ExecuteReader();

        //Debug.Log(dbCommand.CommandText);
        //Debug.Log(reader.GetValue(0));
        float result = float.Parse(reader.GetValue(0).ToString());

        dbConnection.Close();

        return result;
    }
    public int GetEquipmentNeedExperience(EquipmentClass equipmentClass, int EnhancementCount)
    {
        IDbConnection dbConnection = Getconnection();
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT {equipmentClass} FROM EquipmentNeedExperience WHERE Level = '{EnhancementCount+1}'";
        IDataReader reader = dbCommand.ExecuteReader();

        //Debug.Log(dbCommand.CommandText);
        //Debug.Log(reader.GetValue(0));
        int result = int.Parse(reader.GetValue(0).ToString());

        dbConnection.Close();

        return result;
    }
    public List<int> GetEquipmentNeedExperience(EquipmentClass equipmentClass)
    {
        IDbConnection dbConnection = Getconnection();
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = $"SELECT {equipmentClass} FROM EquipmentNeedExperience ";
        IDataReader reader = dbCommand.ExecuteReader();


        List<int> exp = new List<int>();
        
        for (int i = 0; i < 20; i++)
        {
            reader.Read();

            if (int.Parse(reader.GetValue(0).ToString()) == 0)
                break;
            exp.Add(int.Parse(reader.GetValue(0).ToString()));

        }

        dbConnection.Close();
        

        return exp;
    }
    private IDbConnection Getconnection()
    {
        string connectionString = "URI=file:" + Application.streamingAssetsPath + dbName;
        return new SqliteConnection(connectionString);
    }
}
