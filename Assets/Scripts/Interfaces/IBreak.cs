public interface IBrick
{
    event BrickDestroyEventHandler onBrickDestroy;

    void TakeDamage();

    void BrickDestroy();

    BrickTypes GetBrickType();

    int Hp { get; set; }

}
