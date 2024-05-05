using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    private int _startHealth;
    private Enemy _enemy;
    private Image _healthImage;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
        _healthImage = GetComponent<Image>();
    }

    private void Start()
    {
        _startHealth = _enemy.Health;
    }

    private void Update() => UpdateUI();

    private void UpdateUI()
    {
        float healthPercentage = _enemy.Health / (float)_startHealth;
        _healthImage.fillAmount = Mathf.Clamp01(healthPercentage);
    }
}
