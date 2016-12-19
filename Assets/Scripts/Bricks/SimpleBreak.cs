using UnityEngine;

public class SimpleBrick : IBrick
{
    private BrickController _brickController;
    private BrickTypes _brickType;
    private int _hp;

    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if (_hp == 0)
            {
                BrickDestroy();
            }
        }
    }


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
        _brickController.BrickHit();
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