using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 offset = new Vector2(0, 3);
    public float cameraSpeed = 4;
    
	private Transform cachedTransform;

    void Awake()
    {
        cachedTransform = transform;
    }

    void FixedUpdate()
    {
        if (!target)
            return;

        Vector3 pos = cachedTransform.position;
        Vector3 targetPos = target.position;
        cachedTransform.position = Vector3.Lerp(pos, new Vector3(targetPos.x + offset.x, targetPos.y + offset.y, pos.z), Time.deltaTime * cameraSpeed);
    }
}
