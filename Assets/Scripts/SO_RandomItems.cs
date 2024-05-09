using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item List", menuName = "Items/Item List", order = 0)]
public class SO_RandomItems : ScriptableObject
{
    public Item[] items;
    public float[] chances2Spawn;

    public Item GetRandomItem()
    {
        float total = 0;

        foreach (float elem in chances2Spawn)
        {
            total += elem;
        }


        float randomPoint = Random.value * total;

        for (int i = 0; i < items.Length; i++)
        {
            if (randomPoint < chances2Spawn[i])
            {
                return items[i];
            }
            else
            {
                randomPoint -= chances2Spawn[i];
            }
        }
        return items[items.Length - 1];
    }
}
