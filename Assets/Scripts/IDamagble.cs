namespace DefaultNamespace
{
    public interface IDamagble
    {
        int Damage { get; set; }
        void DamageTaken(int _damage);
    }
}