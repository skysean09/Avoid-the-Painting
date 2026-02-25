using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SlotInfo slotinfo = new SlotInfo()
        {
            level = 0,
            slot = this.gameObject,
            image = this.GetComponent<Image>()
        };

        
        SlotManager.instance.slotLevel[this.transform.position] = slotinfo;
    }
}
