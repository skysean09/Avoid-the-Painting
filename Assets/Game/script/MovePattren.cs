using UnityEngine;

public class MovePattren : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * 30;
        if (transform.position.x <= -540)
        {
            transform.position = new Vector2(3450, this.transform.position.y);
        }
    }
}
