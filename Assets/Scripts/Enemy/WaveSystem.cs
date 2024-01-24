using System.Collections.Generic;
using TMPro;
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
    List<Health> currentEnemies;
    [SerializeField] CanvasGroup waveIndicatorCG;
    [SerializeField] TMP_Text waveIndicatorText;
    [SerializeField] CanvasGroup gameControlCG;
    [SerializeField] float waveIndicatorTweenTime;
    [SerializeField] GameObject winPanel;
    LTDescr waveIndicatorTween;

    [SerializeField]
    int threshold;

    [SerializeField]
    int range;

    int currentIndex;


    Vector2 position;

    private void Start()
    {
        currentEnemies = new List<Health>();
        currentIndex = 0;
    }
    public void CheckGenerateWave() 
    {
        if (currentEnemies == null) return;
        int deadCount = 1;
        for(int i = 0;i<currentEnemies.Count;i++) 
        {
            if (currentEnemies[i].aliveStatus == Health.State.Dead)
            {
                deadCount++;
            }
        }
        print(deadCount);
        if(deadCount >= currentEnemies.Count)
        {
            currentIndex++;
            GenerateWave();
        }
    }

    internal void GenerateWave() 
    {
        if (currentIndex > (enemyConfigs.Length - 1)) { winPanel.SetActive(true); return; };

        currentEnemies.Clear();

        waveIndicatorText.text = "WAVE " + (currentIndex+1).ToString();

        waveIndicatorCG.gameObject.SetActive(true);
        gameControlCG.interactable = false;

        if (waveIndicatorTween != null)
            LeanTween.cancel(waveIndicatorTween.id);
        waveIndicatorTween = LeanTween.value(gameObject, (value) => {  waveIndicatorCG.alpha = value; }, 0f,1f,waveIndicatorTweenTime).setEase(LeanTweenType.easeInOutCubic).setLoopPingPong(1)
            .setOnComplete(() => {waveIndicatorCG.gameObject.SetActive(false); gameControlCG.interactable = true; });

        foreach (Enemy enemy in enemyConfigs[currentIndex].enemies)
        {
            for (int i = 0; i < enemy.amount; i++)
            {

                float x = Random.Range(-range, range);
                float y = (x < threshold && x > (-threshold)) ? (Random.Range(0, 2) == 0) ? Random.Range(-range, -threshold) : Random.Range(threshold, range)
                    : Random.Range(-range, range);
                position = new Vector2(x, y);
                GameObject go = Instantiate(enemy.prefab, position, new Quaternion(transform.rotation.x, transform.rotation.y, Random.Range(0f, 1f), transform.rotation.w), transform);
                go.GetComponent<DieEnemy>().waveSystemInstance = this;
                currentEnemies.Add(go.GetComponent<Health>()); // or should we get powers by dashing through the enemies?
            }
        }
    }
}
