using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    public bool cursorMovement = false;
    public float minX = 0;
    public float maxX = 1000;
    public float minY = 0;
    public float maxY = 100;
    public float followSpeed;
    public float cursorMoveSpeed;

    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!cursorMovement && target != null)
        {
            targetPos = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), Mathf.Clamp(target.position.y, minY, maxY), 0);
            Vector3 desiredPosition = targetPos + offset;
            Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.position = smoothed;
        }
        else if (cursorMovement)
        {
            // make it so when you move your cursor towards the edges you move the camera.
            Vector3 mousePos = Input.mousePosition;
            // make it so the 10% of the outside box is what causes the cursor to move.
            float leftInput = mousePos.x < Screen.width * 0.1 ? 1f : 0f;
            float rightInput = mousePos.x > Screen.width * 0.9 ? 1f : 0f;
            float upInput = mousePos.y > Screen.height * 0.9 ? 1f : 0f;
            float downInput = mousePos.y < Screen.height * 0.1 ? 1f : 0f;

            if (leftInput > 0 && transform.position.x > minX)
            {
                transform.Translate(new Vector3(-cursorMoveSpeed * Time.deltaTime, 0));
            }
            if (rightInput > 0 && transform.position.x < maxX)
            {
                transform.Translate(new Vector3(cursorMoveSpeed * Time.deltaTime, 0));
            }
            if (upInput > 0 && transform.position.y < maxY)
            {
                transform.Translate(new Vector3(0, cursorMoveSpeed * Time.deltaTime));
            }
            if (downInput > 0 && transform.position.y > minY)
            {
                transform.Translate(new Vector3(0, -cursorMoveSpeed * Time.deltaTime));
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        cursorMovement = false;
    }

    public void FollowCursor()
    {
        target = null;
        cursorMovement = true;
    }
}
