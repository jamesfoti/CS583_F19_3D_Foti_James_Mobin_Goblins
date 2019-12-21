using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemySpawner : MonoBehaviour
{
    Transform[] waypoints;
    public Transform waypointParent;
    public Transform enemyParent;
    public int totalEnemies;
    public int enemiesDeleted;
    public GameObject nextWaveUI;
    [System.Serializable]
    public struct waveComponent
    {
        [SerializeField]
        public GameObject enemy;
        [SerializeField]
        public float delay;
        [SerializeField]
        public int count;
        [SerializeField]
        public float rate;
    }
    [System.Serializable]
    public struct Wave
    {
        [SerializeField]
        public waveComponent[] components;
    }
    [SerializeField]
    public Wave[] waves;
    int waveIndex=0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        //BeginNextWave();
    }
	public void BeginNextWave()
    {
        
        
            AudioManager.instance.Play("AirHorn");
            enemiesDeleted = 0;
            totalEnemies = 0;
            foreach (waveComponent w in waves[waveIndex].components)
            {
                totalEnemies += w.count;
                StartCoroutine(spawner(w));

            }
            waveIndex++;
        
    }
    public void killEnemy()
    {
        enemiesDeleted++;
        print("kill");
        if (enemiesDeleted == totalEnemies)
        {
            if (waveIndex == waves.Length)
            {
                PlayerHealth.Win();
            }
            else
            {
                nextWaveUI.SetActive(true);
                print("enable UI)");
            }
        }
    }
    private IEnumerator spawner(waveComponent wave)
    {
        for(int i = 0; i < wave.count; i++) { 
            yield return new WaitForSeconds(wave.rate);
            Spawn(wave.enemy);
        }
    }
    void Spawn(GameObject enemy)
    {
        var enemyInstance=Instantiate(enemy,enemyParent);
        enemyInstance.transform.position = waypoints[0].position;
        Enemy enemyscript = enemyInstance.GetComponent<Enemy>();
        enemyscript.waypoints = waypoints;
        enemyscript.spawner = this;
    }
}
