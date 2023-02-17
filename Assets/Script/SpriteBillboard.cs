
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;
    private float degree;

    // Update is called once per frame
    void LateUpdate()
    {
        if (freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, 0f, 0.0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }

    }
}
