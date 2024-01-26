
internal static class ScoreManager 
{

    static int numLevels;
    public static int[] highScores;

    static ScoreManager() 
    {
        numLevels = 5;
        highScores = new int[numLevels];
        LoadData();
    }
    public static void LoadData() 
    {
        object o = SerializationManager.Load("high_scores");
        if(o != null) highScores = (int[])o;
        else SaveData();
    }

    public static void SaveData() 
    {
        SerializationManager.Save("high_scores",highScores);
    }
}