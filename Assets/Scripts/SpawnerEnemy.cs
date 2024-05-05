using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private float _healthGrowthRate = 1.1f;
    [SerializeField] private float _damageGrowthRate = 1.2f;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _enemyPrefab;

    private int _health = 100;
    private int _damage = 10;
    private int _currentEnemyId = 0;
    private bool _firstSpawn = true;
    private Enemy _enemy;
    private GameObject _createdEnemy;

    public void Spawn()
    {
        if (_spawnPoint != null && _enemyPrefab != null)
        {
            _createdEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
            _enemy = _createdEnemy.GetComponent<Enemy>();
            _enemy.OnDie += DestroyEnemy;

            if (_firstSpawn)
                return;

            _currentEnemyId++;
            _enemy.SetId(_currentEnemyId);
            Debug.Log("New Mob Spawned with ID: " + _currentEnemyId);

            _health = (int)(_health * _healthGrowthRate);
            _damage = (int)(_damage * _damageGrowthRate);
            _enemy.NewStats(_health, _damage);

            SaveEnemyStats(_enemy);
        }
        else
        {
            Debug.LogError("Spawn point is not assigned for enemy!");
        }
    }

    private void DestroyEnemy()
    {
        _enemy.OnDie -= DestroyEnemy;
        Destroy(_createdEnemy);
        Spawn();
    }

    private void Awake()
    {
        _enemy = _enemyPrefab.GetComponent<Enemy>();
    }

    private void Start()
    {
        _currentEnemyId = PlayerPrefs.GetInt("LastEnemyId", 0);
        _enemy.SetId(_currentEnemyId);
        Spawn();
        _firstSpawn = false;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.O)) //zeroing
        {
            _health = 100;
            _damage = 10;
            _enemy.NewStats(_health, _damage);
            SaveEnemyStats(_enemy);
            _currentEnemyId = 0;
            _enemy.SetId(_currentEnemyId);
            PlayerPrefs.SetInt("LastEnemyId", _currentEnemyId);
        }
#endif
    }
    private void SaveEnemyStats(Enemy enemy)
    {
        PlayerPrefs.SetInt("EnemyHealth_" + enemy.Id, enemy.Health);
        PlayerPrefs.SetInt("EnemyDamage_" + enemy.Id, enemy.Damage);

        PlayerPrefs.SetInt("LastEnemyId", _currentEnemyId);
        PlayerPrefs.Save();
    }
}
