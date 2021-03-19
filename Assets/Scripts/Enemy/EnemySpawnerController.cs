// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// --------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerController : MonoBehaviour
{
    // Container object to specify the details of each wave.
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

    // Custom Linked List to keep  track of enemies that are currently spawned and update their global health.
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
            if (head == null || head.enemy == null)
            {
                head = null;
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

    private bool doneSpawning = false;
    private bool started = false;
    private bool wiped = false;

    private void Update()
    {
        // If all enemies were defeated, set wiped flag to true.
        if (!wiped && doneSpawning && enemies.head == null) wiped = true;

        // Update global health of all enemies in a handy health bar.
        if (healthBar != null)
        {
            enemies.UpdateHealth();
            healthBar.SetMainValue(enemies.health, enemies.maxHealth);
        }

        // Update remaining time before next wave in a hand bar.
        if (waveTimer != null)
        {
            waveTimer.SetSecondaryValue(currentWaveTime, currentWaveMaxTime, Color.yellow, new Color(1.0f, 0.5f, 0.0f, 1.0f));
        }
    }

    // Getter to see if Spawner has started.
    public bool Started()
    {
        return started;
    }

    // Getter to see if all enemies were killed.
    public bool Wiped()
    {
        return wiped;
    }

    // Helper method to start spawning all waves.
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
        started = true;
    }

    // Coroutine to spawn each wave.
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

        yield return new WaitForSeconds(5.0f);
        doneSpawning = true;
        currentCoroutine = null;
    }

    // Helper method to start spawning the enemies of the current wave.
    private void NextWave()
    {
        if (currentWave < enemyWaves.Length) StartCoroutine(SpawnEnemies());
        else Debug.Log("No more waves left to spawn.");
    }

    // Coroutine to actually spawn those enemies.
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

    // Helper method to spawn a single enemy and add it to our linked list.
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

    // Draw Gizmos on the Unity editor to see where the spawn points are.
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
