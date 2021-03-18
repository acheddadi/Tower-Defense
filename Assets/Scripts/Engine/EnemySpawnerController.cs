using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerController : MonoBehaviour
{
    [System.Serializable]
    private class EnemyWave
    {
        public float nextWaveDelay;
        public int enemyCount;
        public float spawnDuration;

        public EnemyWave(float nextWaveDelay, int enemyCount, float spawnDuration)
        {
            this.nextWaveDelay = nextWaveDelay;
            this.enemyCount = enemyCount;
            this.spawnDuration = spawnDuration;
        }
    }
    private class EnemyList
    {
        public class Node
        {
            public HealthController enemy;
            public Node next;
            public Node previous;

            public Node(HealthController enemy, Node previous)
            {
                this.enemy = enemy;
                this.next = null;
                this.previous = previous;
            }
        }

        public Node head;
        public Node currentNode;
        public float health = 0.0f;
        public float maxHealth = 0.0f;

        public EnemyList()
        {
            head = null;
            currentNode = head;
        }

        public Node Push(HealthController enemy)
        {
            maxHealth += enemy.GetMaxHealth();

            if (head == null)
            {
                head = new Node(enemy, null);
                return head;
            }

            currentNode = head;
            while (currentNode.next != null) currentNode = currentNode.next;

            return currentNode.next = new Node(enemy, currentNode);
        }

        public void UpdateHealth()
        {
            if (head == null)
            {
                health = maxHealth = 0.0f;
                return;
            }

            health = 0.0f;
            currentNode = head;
            while (currentNode != null)
            {
                if (currentNode.enemy != null) health += currentNode.enemy.GetHealth();

                else
                {
                    if (currentNode.previous != null) currentNode.previous.next = currentNode.next;
                    if (currentNode.next != null) currentNode.next.previous = currentNode.previous;
                }

                currentNode = currentNode.next;
            }
        }
    }

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Vector3[] spawnPoints = {
        new Vector3(4.0f, 0.0f, 4.0f),
        new Vector3(-4.0f, 0.0f, 4.0f),
        new Vector3(-4.0f, 0.0f, -4.0f)
    };
    [SerializeField] private EnemyWave[] enemyWaves = { new EnemyWave(30.0f, 3, 3.0f) };
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private HealthBar waveTimer;
    [SerializeField] private Text waveCountText;

    private Coroutine currentCoroutine;
    private EnemyList enemies = new EnemyList();
    private int currentWave = 0;
    private float currentWaveTime = 0.0f;
    private float currentWaveMaxTime = 0.0f;
    private int lastSpawnPoint = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) StartEnemySpawner();

        if (healthBar != null)
        {
            enemies.UpdateHealth();
            healthBar.SetMainValue(enemies.health, enemies.maxHealth);
        }

        if (waveTimer != null)
        {
            waveTimer.SetSecondaryValue(currentWaveTime, currentWaveMaxTime, Color.yellow, new Color(1.0f, 0.5f, 0.0f, 1.0f));
        }
    }

    public void StartEnemySpawner()
    {
        if (enemyPrefabs == null || spawnPoints == null || enemyWaves == null)
        {
            Debug.LogError("Missing data, cannot call NextWave().");
            return;
        }

        if (currentCoroutine != null)
        {
            Debug.Log("Busy spawning enemies, please try again later.");
            return;
        }

        currentCoroutine = StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for (; currentWave < enemyWaves.Length; currentWave++)
        {
            if (waveCountText != null) waveCountText.text = string.Format("{0:00}", currentWave + 1);
            currentWaveMaxTime = enemyWaves[currentWave].nextWaveDelay;

            NextWave();
            for (float t = 0.0f; t < enemyWaves[currentWave].nextWaveDelay; t += Time.deltaTime)
            {
                currentWaveTime = currentWaveMaxTime - t;
                yield return new WaitForEndOfFrame();
            }
        }

        currentCoroutine = null;
    }

    private void NextWave()
    {
        if (currentWave < enemyWaves.Length) StartCoroutine(SpawnEnemies());
        else Debug.Log("No more waves left to spawn.");
    }

    private IEnumerator SpawnEnemies()
    {
        enemies.maxHealth = enemies.health;
        float delay = enemyWaves[currentWave].spawnDuration / enemyWaves[currentWave].enemyCount;

        for (int i = 0; i < enemyWaves[Mathf.Min(currentWave, enemyWaves.Length - 1)].enemyCount; i++)
        {
            enemies.Push(SpawnEnemy());
            yield return new WaitForSeconds(delay);
        }
    }

    private HealthController SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);
        int randomPoint;

        do
        {
            randomPoint = Random.Range(0, spawnPoints.Length);
        } while (randomPoint == lastSpawnPoint);

        
        lastSpawnPoint = randomPoint;
        GameObject enemy = Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomPoint], Quaternion.identity);

        return enemy.GetComponent<HealthController>();
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;

        Gizmos.color = Color.red;
        foreach (Vector3 point in spawnPoints)
        {
            Gizmos.DrawSphere(point, 0.25f);
        }
    }
}
