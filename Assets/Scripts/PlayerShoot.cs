using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.TakeDamage(_damage);
                }
            }
        }
    }
}
