using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;


public class CharacterStatsDB : MonoBehaviour
{

    public int gabiCurrentHealth;
    public int gabiCurrentResolve;

    public int arvandusCurrentHealth;
    public int arvandusCurrentStamina;

    public int quinnCurrentHealth;
    public int quinnCurrentFire;
    public int quinnCurrentEarth;
    public int quinnCurrentWater;

    private string connectionString;

    // Use this for initialization
    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/DataBase/CharacterStatsDB.sqlite";
        GetData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetData()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQueryGabi = "SELECT * FROM CharacterStatsDB WHERE Name = 'Gabi'";

                dbCmd.CommandText = sqlQueryGabi;

                using (IDataReader readerGabi = dbCmd.ExecuteReader())
                {
                    while (readerGabi.Read())
                    {
                        gabiCurrentHealth = readerGabi.GetInt32(2);
                        gabiCurrentResolve = readerGabi.GetInt32(3);
                    }

                    readerGabi.Close();
                }

                string sqlQueryArvandus = "SELECT * FROM CharacterStatsDB WHERE Name = 'Arvandus'";

                dbCmd.CommandText = sqlQueryArvandus;

                using (IDataReader readerArvandus = dbCmd.ExecuteReader())

                {
                    while (readerArvandus.Read())
                    {

                        arvandusCurrentHealth = readerArvandus.GetInt32(2);
                        arvandusCurrentStamina = readerArvandus.GetInt32(3);

                    }

                    readerArvandus.Close();
                }

                string sqlQueryQuinn = "SELECT * FROM CharacterStatsDB WHERE Name = 'Quinn'";

                dbCmd.CommandText = sqlQueryQuinn;

                using (IDataReader readerQuinn = dbCmd.ExecuteReader())

                {
                    while (readerQuinn.Read())
                    {
                        quinnCurrentHealth = readerQuinn.GetInt32(2);
                        quinnCurrentFire = readerQuinn.GetInt32(15);
                        quinnCurrentEarth = readerQuinn.GetInt32(16);
                        quinnCurrentWater = readerQuinn.GetInt32(17);

                        Debug.Log(quinnCurrentFire + " - " + quinnCurrentEarth + " - " + quinnCurrentWater);

                    }

                    readerQuinn.Close();
                    dbConnection.Close();

                }



            }
        }
    }
}
