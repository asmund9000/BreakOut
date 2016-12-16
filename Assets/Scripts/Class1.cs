
public class Rootobject
{
    public Level[] levels { get; set; }
}

public class Level
{
    public int id { get; set; }
    public int rows { get; set; }
    public int columns { get; set; }
    public int[][] levelStruct { get; set; }
}
