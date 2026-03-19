using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    Camera cam;
    float width;
    float height;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        height = cam.orthographicSize * 2f;
        width = height * cam.aspect;

        Vector3 camPos = cam.transform.position;
        Vector3 pos = transform.position;

        if (pos.x > camPos.x + width / 2f)
            pos.x -= width;
        else if (pos.x < camPos.x - width / 2f)
            pos.x += width;

        if (pos.y > camPos.y + height / 2f)
            pos.y -= height;
        else if (pos.y < camPos.y - height / 2f)
            pos.y += height;

        transform.position = pos;
    }
}