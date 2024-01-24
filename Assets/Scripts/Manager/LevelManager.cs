using Unity.VisualScripting.FullSerializer;

internal static class LevelManager 
{

    static int numLevels;
    public static bool[] levelsUnlocked;

    static LevelManager() 
    {
        numLevels = 5;
        levelsUnlocked = new bool[numLevels];
        for (int i = 0; i < numLevels; i++) 
        {
            levelsUnlocked[i] = (i==0)?true:false; 
        }
        LoadData();
    }

    internal static void UnlockLevel(int levelIndex) 
    {
        if (levelIndex > (numLevels-1))
            return;
        levelsUnlocked[levelIndex] = true;
        SaveData();
    }

    public static void LoadData() 
    {
        object o = SerializationManager.Load("levels_unlocked");
        if(o != null) levelsUnlocked = (bool[])o;
        else SaveData();
    }

    public static void SaveData() 
    {
        SerializationManager.Save("levels_unlocked",levelsUnlocked);
    }
}
