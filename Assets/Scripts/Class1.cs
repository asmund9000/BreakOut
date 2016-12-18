
public class Rootobject
{
    public LevelConfig[] levels { get; set; }
}

public class LevelConfig
{
    public int id { get; set; }
    public int rows { get; set; }
    public int columns { get; set; }
    public int[][] levelStruct { get; set; }
}
