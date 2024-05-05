using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _id;

    public Action OnDie;

    public int Health { get => _health; }
    public int Damage { get => _damage; }
    public int Id { get => _id; }

    public void SetId(int id)
    {
        _id = id;
    }

    public void NewStats(int health, int damage)
    {
        _health = health;
        _damage = damage;
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDie?.Invoke();
    }

}
