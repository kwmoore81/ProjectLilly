using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySpellSpawn : MonoBehaviour
{
    public List<GameObject> SkySpellSpawnPoints;
    public GameObject projectile;
    private bool spellFired = false;
   

    public bool testFire1 = false;
    public bool testFire2 = false;
    public GameObject target;
    public GameObject spellSpawn;

    // This whole section is for testing purposes
    void Update()
    {
        if (testFire1)
        {
            spellFired = false;

            Vector3 relativePosition = target.transform.position - spellSpawn.transform.position;
            Quaternion spellRotation = Quaternion.LookRotation(relativePosition);
            GameObject tempSpell = Instantiate(projectile, spellSpawn.transform.position, spellRotation) as GameObject;

            testFire1 = false;
        }

        if (testFire2)
        {
            for (int i = 0; i < SkySpellSpawnPoints.Count; i++)
            {
                Quaternion spellRotation = SkySpellSpawnPoints[i].transform.rotation;
                GameObject tempSpell = Instantiate(projectile, SkySpellSpawnPoints[i].transform.position, spellRotation) as GameObject;
            }

            testFire2 = false;
        }
    }

    // Cycle through the spell spawn points and fire projectiles
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);

        if (!spellFired)
        {
            for (int i = 0; i < SkySpellSpawnPoints.Count; i++)
            {
                Quaternion spellRotation = SkySpellSpawnPoints[i].transform.rotation;
                GameObject tempSpell = Instantiate(projectile, SkySpellSpawnPoints[i].transform.position, spellRotation) as GameObject;
            }
        }

        spellFired = true;
    }
}
