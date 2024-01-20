using UnityEngine;

[System.Serializable]
internal struct Enemy 
{
    public GameObject prefab;
    public byte amount;
}

[System.Serializable]
internal struct EnemyConfiguration 
{
    [SerializeField]
    public Enemy[] enemies;
}

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    EnemyConfiguration[] enemyConfigs;

    [SerializeField]
    int threshold;

    [SerializeField]
    int range;

    [SerializeField]
    int numWaves;


    Vector2 position;

    private void Start()
    {
        GenerateWave(0);
    }

    void GenerateWave(int index) 
    {
        if (enemyConfigs[index].enemies.Length == 0) return;
        foreach (Enemy enemy in enemyConfigs[index].enemies)
        {
            for (int i = 0; i < enemy.amount; i++)
            {

                float x = Random.Range(-range, range);
                float y = (x < threshold && x > (-threshold)) ? (Random.Range(0, 2) == 0) ? Random.Range(-range, -threshold) : Random.Range(threshold, range)
                    : Random.Range(-range, range);
                position = new Vector2(x, y);
                GameObject g = Instantiate(enemy.prefab, position, new Quaternion(transform.rotation.x, transform.rotation.y, Random.Range(0f, 1f), transform.rotation.w), transform); // or should we get powers by dashing through the enemies?
            }
        }
    }
}
