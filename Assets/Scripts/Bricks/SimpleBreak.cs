using UnityEngine;

public class SimpleBreak : IBrick
{
    private BrickController _brickController;
    private BrickTypes _brickType;
    public int Hp { get; set; }

    public event BrickDestroyEventHandler onBrickDestroy;

    public SimpleBreak(BrickController brickController)
    {
        this._brickController = brickController;
        _brickType = BrickTypes.Easy;
        Hp = 1;
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

    public void BrickDestroy()
    {
        onBrickDestroy(_brickType);
        _brickController.BrickDestroy();
    }

    public BrickTypes GetBrickType()
    {
        return _brickType;
    }


}