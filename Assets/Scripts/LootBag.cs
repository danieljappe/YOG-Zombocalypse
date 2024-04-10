using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    Loot GetDroppedItems(){

        int randomNumber = Random.Range(1, 101);

        List<Loot> possibleItems = new List<Loot>();

        foreach(Loot item in lootList){
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if(possibleItems.Count > 0 ){
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
    Loot droppedItem = GetDroppedItems();
    Debug.Log("DroppedItem = " + droppedItem);
    
    if (droppedItem != null && droppedItem.lootGameObject != null)
    {
        GameObject lootGameObject = Instantiate(droppedItem.lootGameObject, spawnPosition, Quaternion.identity);

        Rigidbody rb = lootGameObject.GetComponent<Rigidbody>();

        float dropForce = 1f;
        Vector3 dropDirection = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), Random.Range(-1f,1f));
        rb.AddForce(dropDirection * dropForce, ForceMode.Impulse);
    }
    else
    {
        Debug.Log("No loot dropped or lootGameObject is not assigned.");
    }
    }



}
