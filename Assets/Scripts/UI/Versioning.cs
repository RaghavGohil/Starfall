using UnityEngine;
using TMPro;

public class Versioning : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = "v"+Application.version; 
    }
}
