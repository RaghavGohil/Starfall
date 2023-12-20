using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
internal sealed class Block 
{
    public GameObject obj;
    public byte amount;
}

internal sealed class BlockGenerator : MonoBehaviour
{

    [SerializeField]
    Block[] blocks;

    [SerializeField]
    int threshold;

    [SerializeField]
    int range;

    Vector2 position;

    private void Start()
    {
        if (blocks.Length == 0) return;
        foreach (Block block in blocks) 
        {
            for (int i = 0; i < block.amount; i++) 
            {

                float x = Random.Range(-range, range);
                float y = (x<threshold && x>(-threshold)) ? (Random.Range(0, 2) == 0) ? Random.Range(-range, -threshold) : Random.Range(threshold, range)
                    : Random.Range(-range, range);
                position = new Vector2(x, y);
                GameObject g = Instantiate(block.obj,position,new Quaternion(transform.rotation.x, transform.rotation.y, Random.Range(0f,1f), transform.rotation.w),transform); // or should we get powers by dashing through the enemies?

            }
        }
    }

}
