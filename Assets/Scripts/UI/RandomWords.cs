using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class RandomWords : MonoBehaviour
{

    [SerializeField]
    string[] str;

    void Start()
    {
        if (str.Length != 0) 
        {
            int random = Random.Range(0,str.Length-1);
            GetComponent<TMP_Text>().text = str[random];
        }
    }
}
