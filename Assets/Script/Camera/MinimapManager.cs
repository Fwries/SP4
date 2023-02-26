using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    public void AdjustMinimapCamera(Vector2 minBound, Vector2 maxBound, float HALF_ROOM_SIZE)
    {
        float mapMinBoundX = minBound.x - HALF_ROOM_SIZE;
        float mapMinBoundY = minBound.y - HALF_ROOM_SIZE;

        float mapMaxBoundX = maxBound.x + HALF_ROOM_SIZE;
        float mapMaxBoundY = maxBound.y + HALF_ROOM_SIZE;

        transform.GetComponent<Camera>().orthographicSize = Mathf.Max(mapMaxBoundX - mapMinBoundX, mapMaxBoundY - mapMinBoundY) / 2.0f;

        transform.SetPositionAndRotation(new Vector3(mapMinBoundY + ((mapMaxBoundY - mapMinBoundY) / 2.0f),
                                                     10.0f,
                                                     mapMinBoundX + ((mapMaxBoundX - mapMinBoundX) / 2.0f)),
                                         Quaternion.Euler(90.0f, 0.0f, 0.0f));
    }
}
