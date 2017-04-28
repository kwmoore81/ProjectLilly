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

    public GameObject battleMaster;
    private OverWorldSceneChanger2 overWorldSceneChanger2;

    public PauseGame pauseGame;

    public int gabiCurrentHealth;
    public int gabiCurrentResolve;

    public int arvandusCurrentHealth;
    public int arvandusCurrentStamina;

    public int quinnCurrentHealth;
    public int quinnCurrentFire;
    public int quinnCurrentEarth;
    public int quinnCurrentWater;

    public float currentAreaCorruption;

    private string connectionString;

    // Use this for initialization
    void Start()
    {
        overWorldSceneChanger1 = overWorldMaster.GetComponent<OverworldSceneChanger1>();
        overWorldSceneChanger2 = battleMaster.GetComponent<OverWorldSceneChanger2>();
        pauseGame = GetComponent<PauseGame>();
        connectionString = "URI=file:" + Application.dataPath + "/DataBase/CharacterStatsDB.sqlite";
        GetData();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame.pause();
        }
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

                    }

                    readerQuinn.Close();

                    //Update Area Corruption from the DataBase
                    string sqlQueryCorruption = "SELECT * FROM CharacterStatsDB WHERE Name = 'Corruption'";

                    dbCmd.CommandText = sqlQueryCorruption;

                    using (IDataReader readerCorruption = dbCmd.ExecuteReader())

                    {
                        while (readerCorruption.Read())
                        {
                            currentAreaCorruption = readerCorruption.GetFloat(1);
                        }

                        readerCorruption.Close();
                        dbConnection.Close();

                    }

                }
            }
        }
    }
    public void SendData1()
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
        currentAreaCorruption = overWorldSceneChanger1.currentAreaCorruption;

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

                //Update Area Corruption
                string sqlQueryCorruption = "Update CharacterStatsDB Set Amount = @amount WHERE Name = 'Corruption'";

                dbCmd.CommandText = sqlQueryCorruption;
                dbCmd.Connection = dbConnection;
                dbCmd.Parameters.Add(new SqliteParameter("@amount", currentAreaCorruption));

                dbConnection.Close();

            }
        }
    }

    public void SendData2()
    {

        //Grabbing Current Character Vitals
        gabiCurrentHealth = overWorldSceneChanger2.gabiCurrentHealth;
        gabiCurrentResolve = overWorldSceneChanger2.gabiCurrentResolve;

        arvandusCurrentHealth = overWorldSceneChanger2.arvandusCurrentHealth;
        arvandusCurrentStamina = overWorldSceneChanger2.arvanusCurrentStamina;

        quinnCurrentHealth = overWorldSceneChanger2.quinnCurrentHealth;
        quinnCurrentFire = overWorldSceneChanger2.quinnCurrentFire;
        quinnCurrentEarth = overWorldSceneChanger2.quinnCurrentEarth;
        quinnCurrentWater = overWorldSceneChanger2.quinnCurrentWater;
        currentAreaCorruption = overWorldSceneChanger2.currentAreaCorruption;


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

                //Update Area Corruption
                string sqlQueryCorruption = "Update CharacterStatsDB Set Amount = @amount WHERE Name = 'Corruption'";

                dbCmd.CommandText = sqlQueryCorruption;
                dbCmd.Connection = dbConnection;
                dbCmd.Parameters.Add(new SqliteParameter("@amount", currentAreaCorruption));

                dbConnection.Close();

            }
        }

    }
}
