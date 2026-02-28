using UnityEngine;

public static class Setting
{
    public static float cellSize = 135;
    public static int gridSize = 5;
    public static Vector3 gridPos = new Vector3(0, -100, 0);

    public static int maxLevel = 5;
    public static int maxLevelCount;
    public static int clearCount = 25;
    public static int paintNum = 1;
    public static float durationPaint = 5f;

    public static int maxHp = 3;
    public static int hp = 3;
}

public enum Item
{
    None,
    Paint,
}
