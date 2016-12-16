using UnityEngine;

public class SimpleBreak : IBrick
{
    private BrickController _brickController;
    private BrickTypes _brickType;
    public int Hp { get; set; }

    public event BrickDestroyEventHandler onBrickDestroy;

    public SimpleBreak(BrickController brickController, BrickTypes brickType, int hp)
    {
        this._brickController = brickController;
        this._brickType = brickType;
        Hp = hp;
        onBrickDestroy += GameMaster.instance.BricksDecrement;
    }

    public void TakeDamage()
    {
        Hp--;
        if (Hp == 0)
        {
            BrickDestroy();
        }
    }

    public Vector3 GetBrickPosition()
    {
        return _brickController.GetTransform().position;
    }

    public void BrickDestroy()
    {
        onBrickDestroy(this);
        _brickController.BrickDestroy();
    }

    public BrickTypes GetBrickType()
    {
        return _brickType;
    }


}