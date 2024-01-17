internal static class CoinManager
{

    static int totalAmount = 0;

    static CoinManager() 
    {
        LoadData();
    }

    internal static void AddAmount(int amount) 
    {
        totalAmount += amount;
        SaveData();
    }

    internal static void DeductAmount(int amount)
    {
        totalAmount -= amount;
        SaveData();
    }

    internal static int GetAmount() => totalAmount;

    public static void LoadData() 
    {
        object o = SerializationManager.Load("coins");
        if(o != null) totalAmount = (int)o;
    }

    public static void SaveData() 
    {
        SerializationManager.Save("coins",totalAmount);
    }
}
