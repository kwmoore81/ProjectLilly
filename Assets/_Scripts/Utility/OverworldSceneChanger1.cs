using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldSceneChanger1 : MonoBehaviour {

    public GameObject overworld1;
    public GameObject overworld2;
   
    public GameObject DataBankObj;
    private DataBank databank;

    public int gabiCurrentHealth;
    public int gabiCurrentResolve;

    public int arvandusCurrentHealth;
    public int arvanusCurrentStamina;

    public int quinnCurrentHealth;
    public int quinnCurrentFire;
    public int quinnCurrentEarth;
    public int quinnCurrentWater;

    public int currentAreaCorruption;

    void Start()
    {
        databank = DataBankObj.GetComponent<DataBank>();
    }

    public void SceneChange()
    {             
            overworld2.gameObject.SetActive(true);
            overworld1.gameObject.SetActive(false);   
    }

    public void UpdateFromBank()
    {
        gabiCurrentHealth = databank.gabiCurrentHealth;
        gabiCurrentResolve = databank.gabiCurrentResolve;

        arvandusCurrentHealth = databank.arvandusCurrentHealth;
        arvanusCurrentStamina = databank.arvanusCurrentStamina;

        quinnCurrentHealth = databank.quinnCurrentHealth;
        quinnCurrentFire = databank.quinnCurrentFire;
        quinnCurrentEarth = databank.quinnCurrentEarth;
        quinnCurrentWater = databank.quinnCurrentWater;

        currentAreaCorruption = databank.currentAreaCorruption;             
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (overworld2.gameObject.activeInHierarchy == false && overworld1.gameObject.activeInHierarchy == true)
            {
                databank.UpdateBank(gabiCurrentHealth, gabiCurrentResolve, arvandusCurrentHealth, arvanusCurrentStamina, quinnCurrentHealth, quinnCurrentFire, quinnCurrentEarth, quinnCurrentWater, currentAreaCorruption);             
                SceneChange();
            }
        }
    }

}
