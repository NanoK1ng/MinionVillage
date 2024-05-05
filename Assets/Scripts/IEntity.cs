public interface IEntity
{
    int Health { get;}
    int Damage { get;}

    public void TakeDamage(int amount);
    public void Die();

}
