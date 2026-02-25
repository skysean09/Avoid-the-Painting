using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public static SlotManager instance;
    public List<string> hex = new List<string>()
    {
        "#E7E6F2",
        "#8DE074",
        "#7BD35B",
        "#51A52A",
        "#197B14"
    };
    public Dictionary<Vector3, SlotInfo> slotLevel;

    public void LevelUp(Vector3 pos)
    {
        SlotInfo slot = slotLevel[pos];
        slot.level++;
        if(slot.level > hex.Count - 1)
        {
            //slotLevel.Remove(pos);
        }
        else if (ColorUtility.TryParseHtmlString(hex[slot.level], out Color color))
        {
            slot.image.color = color;
        }
    }
}

public struct SlotInfo
{
    public int level;
    public GameObject slot;
    public Image image;
}