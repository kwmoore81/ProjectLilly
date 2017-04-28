using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;


public class PlayerInventoryDB : MonoBehaviour {

    private string itemConnectionDB;
    private string inventoryConnectionDB;

    public int itemIDtemp;
    public string nameTemp;
    public string typeTemp;
    public int quantityTemp;

    // Use this for initialization
    void Start ()
    {
        itemConnectionDB = "URI=File:" + Application.dataPath + "/DataBase/ItemsDB.sqlite";
        inventoryConnectionDB = "URI=File:" + Application.dataPath + "/DataBase/InventoryDB.sqlite";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Get an item from the item DB and store the values in temp variables
    public void GetItemData(int itemID)
    {
        using (IDbConnection dbConnection = new SqliteConnection(itemConnectionDB))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM ItemsDB WHERE ID = " + itemID;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader readerItem = dbCmd.ExecuteReader())
                {
                    while (readerItem.Read())
                    {
                        itemIDtemp = readerItem.GetInt32(0);
                        nameTemp = readerItem.GetString(1);
                        typeTemp = readerItem.GetString(2);
                    }
                    readerItem.Close();
                    dbConnection.Close();
                }

            }
        }
    }

    //Get an item from the player inventory and store it in a temp variable
    public void GetInventoryData(int itemID)
    {
        using (IDbConnection dbConnection = new SqliteConnection(inventoryConnectionDB))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM InventoryDB WHERE ID = " + itemID;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader readerItem = dbCmd.ExecuteReader())
                {
                    while (readerItem.Read())
                    {
                        itemIDtemp = readerItem.GetInt32(0);
                        nameTemp = readerItem.GetString(1);
                        typeTemp = readerItem.GetString(2);
                        quantityTemp = readerItem.GetInt32(3);
                    }
                    readerItem.Close();
                    dbConnection.Close();
                }

            }
        }
    }
    
    //Add an item stored in temp variables to the player's inventory
    public void AddToInventory(int itemID, int quantity)
    {
        using (SqliteConnection dbconnection = new SqliteConnection(inventoryConnectionDB))
        {
            dbconnection.Open();
            using (IDbCommand dbCmd = dbconnection.CreateCommand())
            {
                dbCmd.CommandText = "SELECT count(*) FROM InventoryDB WHERE ID = " + itemID;
                int count = Convert.ToInt32(dbCmd.ExecuteScalar());

                if (count <= 0)
                {
                    string sqlQuery = "INSERT INTO InventoryDB (ID, Name, Type, Quantity)";

                    dbCmd.CommandText = sqlQuery;
                    dbCmd.Connection = dbconnection;
                    dbCmd.Parameters.Add(new SqliteParameter("@ID", itemIDtemp));
                    dbCmd.Parameters.Add(new SqliteParameter("@Name", nameTemp));
                    dbCmd.Parameters.Add(new SqliteParameter("@Type", typeTemp));
                    dbCmd.Parameters.Add(new SqliteParameter("@Quantity", quantityTemp));
                }
                else
                {
                    int newQuantity = quantity + quantityTemp;
                    string sqlQuery = "Update CharacterStatsDB Set Quantity = @Quantity WHERE ID = " + itemID;
                    dbCmd.CommandText = sqlQuery;
                    dbCmd.Connection = dbconnection;
                    dbCmd.Parameters.Add(new SqliteParameter("@Quantity", newQuantity));
                }
                dbconnection.Close();
            }
        }
    }

    //Remove an item from the player's inventory
    public void RemoveFromInventory(int itemID)
    {
        using (SqliteConnection dbconnection = new SqliteConnection(inventoryConnectionDB))
        {
            dbconnection.Open();
            using (IDbCommand dbCmd = dbconnection.CreateCommand())
            {
                string sqlQuery = "DELETE FROM IventoryDB WHERE ID = " + itemID;

                dbconnection.Close();
            }
        }
    }
}
