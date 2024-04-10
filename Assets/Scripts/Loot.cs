using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public GameObject lootGameObject;
    public string lootName;
    public int dropChance;

    public Loot(GameObject lootGameObject, string lootName, int dropChance)
    {
        this.lootGameObject = lootGameObject;
        this.lootName = lootName;
        this.dropChance = dropChance;
    }

    // Add a constructor without parameters
    public Loot()
    {
    }
}
