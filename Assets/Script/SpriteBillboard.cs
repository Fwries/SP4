
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField]
    bool freezeXZAxis = true;

    // Update is called once per frame
    void LateUpdate()
    {
        if (freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
