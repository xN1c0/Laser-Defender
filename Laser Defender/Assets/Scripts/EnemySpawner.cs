using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;

    int currentWaveIndex;
    [SerializeField] bool looping;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        looping = true;
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int i=currentWaveIndex; i < waveConfigs.Count; i++)
        {
            WaveConfig currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnWave(currentWave));
        }
    }

    private IEnumerator SpawnWave(WaveConfig waveConfig)
    {
        for (int i=0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            GameObject newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
            waveConfig.GetWayPoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
