
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;
    [SerializeField] bool isWeapon;
    private float degree;

    // Update is called once per frame
    void LateUpdate()
    {
        if (isWeapon)
        {
            RotateAround(transform, new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.right, 720 * Time.deltaTime);
        }
        else if (freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0.0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }

    static void RotateAround(Transform transform, Vector3 pivotPoint, Vector3 axis, float angle)
    {
        Quaternion rot = Quaternion.AngleAxis(angle, axis);
        transform.position = rot * (transform.position - pivotPoint) + pivotPoint;
        transform.rotation = rot * transform.rotation;
    }
}
