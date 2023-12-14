using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockGenerator : MonoBehaviour
{
    int blockAmount = 100;
    public GameObject blockPrefab;
    Vector2 position;
    List<Vector2> positions;
    int range;

    private void Start()
    {
        range = 100;

        for (int i = 0; i < blockAmount; i++) 
        {

            position = new Vector2(Random.Range(-range, range), Random.Range(-range, range));

            GameObject g = Instantiate(blockPrefab,position,Quaternion.identity); // or should we get powers by dashing through the enemies?

        }
    }

}
