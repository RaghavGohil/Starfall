using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SerializationManager
{

    static string save_prefix = "starfall_game_";

    public static bool Save(string savename,object data)
    {
        BinaryFormatter bf = GetBinaryFormatter();

        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/" + save_prefix + savename + ".save";

        FileStream fs = File.Create(path);

        bf.Serialize(fs,data);

        fs.Close();

        return true;

    }

    public static object Load(string p)
    {

        string path = Application.persistentDataPath + "/saves/" + save_prefix + p + ".save";

        if(!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter bf = GetBinaryFormatter();

        FileStream fs = File.Open(path,FileMode.Open);

        try
        {
            object save = bf.Deserialize(fs);
            fs.Close();
            return save;
        }
        catch
        {
            Debug.LogError("Can't load file on path" + path);
            fs.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter bf = new BinaryFormatter();
        return bf;
    }

}
