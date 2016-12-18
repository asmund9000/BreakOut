using UnityEngine;

public class SimpleBrick : IBrick
{
    private BrickController _brickController;
    private BrickTypes _brickType;
    public int Hp { get; set; }

    public event BrickDestroyEventHandler onBrickDestroy;

    public SimpleBrick(BrickController brickController)
    {
        this._brickController = brickController;
        this._brickType = BrickTypes.Easy;
        switch (brickController.brickType)
        {
            case BrickTypes.Easy:
                Hp = 1;
                break;
            case BrickTypes.Medium:
                Hp = 2;
                break;
            case BrickTypes.Hard:
                Hp = 3;
                break;
            default:
                break;
        }
        
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