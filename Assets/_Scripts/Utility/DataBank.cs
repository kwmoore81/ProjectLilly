using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBank : MonoBehaviour {
    public int gabiCurrentHealth;
    public int gabiCurrentResolve;

    public int arvandusCurrentHealth;
    public int arvanusCurrentStamina;

    public int quinnCurrentHealth;
    public int quinnCurrentFire;
    public int quinnCurrentEarth;
    public int quinnCurrentWater;

    public int currentAreaCorruption;

	// Use this for initialization
	void Start () {

        gabiCurrentHealth = 100;
        gabiCurrentResolve = 100;

        arvandusCurrentHealth = 100;
        arvanusCurrentStamina = 100;

        quinnCurrentHealth = 100;
        quinnCurrentFire = 5;
        quinnCurrentEarth = 5;
        quinnCurrentWater = 5;

        currentAreaCorruption = 100;
}
	
	// Update is called once per frame
	public void UpdateBank(int gabiHealth, int gabiResolve, int arvandusHealth, int arvandusStamina, int quinnHealth, int quinnFire, int quinnEarth, int quinnWater, int areaCorruption)
    {
        gabiCurrentHealth = gabiHealth;
        gabiCurrentResolve = gabiResolve;

        arvandusCurrentHealth = arvandusHealth;
        arvanusCurrentStamina = arvandusStamina;

        quinnCurrentHealth = quinnHealth;
        quinnCurrentFire = quinnFire;
        quinnCurrentEarth = quinnEarth;
        quinnCurrentWater = quinnWater;

        currentAreaCorruption = areaCorruption;

    }
}
