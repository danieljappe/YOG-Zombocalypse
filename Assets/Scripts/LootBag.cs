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
       
       if (droppedItem != null)
       {
           GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
           Renderer renderer = droppedItem.lootGameObject.GetComponent<Renderer>();
    
           if (renderer != null)
           {
               renderer.material = droppedItem.lootGameObject.GetComponent<Renderer>().material;
    
               float dropForce = 300f;
               Vector2 dropDirection = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
               lootGameObject.GetComponent<Rigidbody>().AddForce(dropDirection * dropForce, ForceMode.Impulse);
           }
           else
           {
               Debug.Log("Renderer component not found on lootGameObject.");
           }
       }
    }


}
