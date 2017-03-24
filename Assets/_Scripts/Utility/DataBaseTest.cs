using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class DataBaseTest : MonoBehaviour {

    private string connectionString;

	// Use this for initialization
	void Start ()
    {
        connectionString = "URI=file:" + Application.dataPath + "/DataBase/DataBaseTest.sqlite";
        GetData();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void InsertAmount(string name, int amount)
    //{
    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();

    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())
    //        {
    //            string sqlQuery = String.Format("InsertAmount INTO DataBaseTest(Amount) VALUES(\"{3}\")", name, amount);

    //            dbCmd.CommandText = sqlQuery;
    //            dbCmd.ExecuteScalar();
    //            dbConnection.Close();
       
    //         }

    //     }
    // }

    private void GetData()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM DataBaseTest";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader.GetInt32(0) + " - " + reader.GetString(1) + " " + reader.GetString(2) + " - " + reader.GetInt32(3));
                    }

                    dbConnection.Close();
                    reader.Close();
                }

            }
        }
    }
}
