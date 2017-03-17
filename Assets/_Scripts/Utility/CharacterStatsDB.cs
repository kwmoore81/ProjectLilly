using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;


public class CharacterStatsDB : MonoBehaviour
{
    public GameObject overWorldMaster;
    private OverworldSceneChanger1 overWorldSceneChanger1;

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
        overWorldSceneChanger1 = overWorldMaster.GetComponent<OverworldSceneChanger1>();
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
                //Update Gabi From Database
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

                //Update Arvandus From DataBase
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

                //Update Quinn From Database
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

    public void SendData()
    {

        //Grabbing Current Character Vitals
        gabiCurrentHealth = overWorldSceneChanger1.gabiCurrentHealth;
        gabiCurrentResolve = overWorldSceneChanger1.gabiCurrentResolve;

        arvandusCurrentHealth = overWorldSceneChanger1.arvandusCurrentHealth;
        arvandusCurrentStamina = overWorldSceneChanger1.arvanusCurrentStamina;

        quinnCurrentHealth = overWorldSceneChanger1.quinnCurrentHealth;
        quinnCurrentFire = overWorldSceneChanger1.quinnCurrentFire;
        quinnCurrentEarth = overWorldSceneChanger1.quinnCurrentEarth;
        quinnCurrentWater = overWorldSceneChanger1.quinnCurrentWater;


        using (SqliteConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                //Update Gabi's DataBase
                string sqlQueryGabi = "Update CharacterStatsDB Set Health = @health, Resource = @resource WHERE Name = 'Gabi'";

                dbCmd.CommandText = sqlQueryGabi;
                dbCmd.Connection = dbConnection;
                dbCmd.Parameters.Add(new SqliteParameter("@health", gabiCurrentHealth));
                dbCmd.Parameters.Add(new SqliteParameter("@resource", gabiCurrentResolve));

                //Update Arvandus' DataBase
                string sqlQueryArvandus = "Update CharacterStatsDB Set Health = @health, Resource = @resource WHERE Name = 'Arvandus'";

                dbCmd.CommandText = sqlQueryArvandus;
                dbCmd.Connection = dbConnection;
                dbCmd.Parameters.Add(new SqliteParameter("@health", arvandusCurrentHealth));
                dbCmd.Parameters.Add(new SqliteParameter("@resource", arvandusCurrentStamina));
                
                //Update Quinn's DataBase
                string sqlQueryQuinn = "Update CharacterStatsDB Set Health = @health, FireCharges = @fireCharges, EarthCharges = @earthCharges, WaterCharges = @waterCharges WHERE Name = 'Quinn'";

                dbCmd.CommandText = sqlQueryQuinn;
                dbCmd.Connection = dbConnection;
                dbCmd.Parameters.Add(new SqliteParameter("@health", quinnCurrentHealth));
                dbCmd.Parameters.Add(new SqliteParameter("@fireCharges", quinnCurrentFire));
                dbCmd.Parameters.Add(new SqliteParameter("@earthCharges", quinnCurrentEarth));
                dbCmd.Parameters.Add(new SqliteParameter("@waterCharges", quinnCurrentWater));

                dbConnection.Close();              

            }
        }
    }
}
