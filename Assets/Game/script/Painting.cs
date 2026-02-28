using Unity.VisualScripting;
using UnityEngine;

public class Painting : MonoBehaviour
{
    public Vector2Int pos;

    private void Update()
    {
        if(pos == Player.instance.pos)
        {
            SlotInfo slot = SlotManager.instance.slotInfo[pos];
            slot.level += 1;
            if(slot.level == Setting.maxLevel)
            {
                Setting.maxLevelCount++;
            }
            slot.state = Item.None;
            SlotManager.instance.slotInfo[pos] = slot;
            SlotManager.instance.SlotLevelUp(pos);

            ItemSpanwer.instance.paintNum--;
            Destroy(gameObject);
        }
    }
}
