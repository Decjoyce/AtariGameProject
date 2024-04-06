using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadCharacterScript : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }

}
