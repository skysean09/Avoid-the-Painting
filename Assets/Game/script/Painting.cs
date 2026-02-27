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
            slot.state = Item.None;
            SlotManager.instance.slotInfo[pos] = slot;

            ItemSpanwer.instance.paintNum--;
            Destroy(gameObject);
        }
    }
}
