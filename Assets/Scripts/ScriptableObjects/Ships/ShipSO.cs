using UnityEngine;

[CreateAssetMenu(fileName = "ship",menuName = "New Ship")]
public class ShipSO : ScriptableObject
{
    public int id;
    public string shipName;
    public string description;
    public Sprite shipImage;
    public int price;
}
