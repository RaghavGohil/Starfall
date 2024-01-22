using UnityEngine;

[System.Serializable]
public class DropAble 
{
    public GameObject obj;
    public int times;
}
internal sealed class Drop: MonoBehaviour
{
    [SerializeField] DropAble[] dropAbles;
    [SerializeField] float tweenTime;
    [SerializeField] float maxDistance;
    Vector2 randomVector;
    public void DropStuff() 
    {
        for (int i = 0; i < dropAbles.Length; i++)
        {
            for (int j = 0; j < dropAbles[i].times; j++)
            {
                randomVector = (new Vector2(Random.Range(0f, maxDistance), Random.Range(0f, maxDistance)));
                GameObject go = Instantiate(dropAbles[i].obj,transform.position,Quaternion.identity);
                LeanTween.move(go,(Vector2)go.transform.position+randomVector,tweenTime);
            }
        }
    } 
}
