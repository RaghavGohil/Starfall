using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAnimationManager : MonoBehaviour
{
    bool hasPlayedOnce = false;
    void Awake()
    {
        LoadData();
        if (!hasPlayedOnce)
        {
            SceneManager.LoadScene("Start");
        }
    }

    void LoadData() 
    {
        object o = SerializationManager.Load("start_animation");
        if (o != null) 
        {
            hasPlayedOnce = (bool) o;     
        }
        else 
        {
            hasPlayedOnce = false;
            SaveData();
        }
    }
    void SaveData() 
    {
        SerializationManager.Save("start_animation",true);
    }
}
