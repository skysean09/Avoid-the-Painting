using System.Numerics;

public static class Setting
{
    public static float cellSize = 135;
    public static int gridSize = 5;
    public static Vector3 gridPos = new Vector3(0,100,0);

    public static int lineAttackNum = 2;
    public static int cellAttackNum = 3;
    public static int paintNum = 1;
    public static int hpNum = 1;

    public static float lineTime = 4f;
    public static float cellTime = 2f;
    public static float hpTime = 10f;

    public static int maxHp = 3;
    public static int hp = 3;
}

public enum Item
{
    None,
    Paint,
    CellAttack,
    LineAttack,
    Hp
}
