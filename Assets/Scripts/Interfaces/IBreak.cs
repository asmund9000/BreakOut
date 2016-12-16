public interface IBrick
{
    event BrickDestroyEventHandler onBrickDestroy;

    void TakeDamage();

    void BrickDestroy();


    UnityEngine.Vector3 GetBrickPosition();
    BrickTypes GetBrickType();

    int Hp { get; set; }

}
