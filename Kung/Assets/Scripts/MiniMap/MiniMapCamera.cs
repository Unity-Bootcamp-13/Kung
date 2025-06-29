using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public Transform player;
    private Vector3 positionControl; 

    public bool followX = false;
    public bool followY = true;

    void Start()
    {
        if (player != null)
        {
            positionControl = transform.position - player.position;   // ī�޶� ��ġ�� �÷��̾� ��ġ ����
        }
    }

    // LateUpdate�� ƨ�� ����
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 currentPos = transform.position;

            if (followX) 
            { 
                currentPos.x = player.position.x + positionControl.x;
            }

            if (followY)
            {
                currentPos.y = player.position.y + positionControl.y;
            }

            currentPos.z = player.position.z + positionControl.z;  // z��ǥ�� ����
            transform.position = currentPos;
        }
    }
}
