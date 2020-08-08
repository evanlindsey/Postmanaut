using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform thePlayer;

    public float x = -5f;
    public float y = 2f;

    private void LateUpdate()
    {
        float playerX = thePlayer.position.x - x;
        float playerY = thePlayer.position.y + y;
        float playerZ = thePlayer.position.z;
        transform.position = new Vector3(playerX, playerY, playerZ);
    }

}
