using UnityEngine;
using UnityEngine.EventSystems;
#if  UNITY_ANDROID || UNITY_IOS
using UnityEngine.InputSystem;
#endif
public class RotateObjectWithMouse : MonoBehaviour
{
    [SerializeField] private float _rotationSpeedPC = 10f;
    [SerializeField] private float _rotationSpeedMobile = 3f;

    private void Update()
    {
#if UNITY_WEBGL || UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Click UI");
                return;
            }

            float mouseX = Input.GetAxis("Mouse X") * _rotationSpeedPC;
            float mouseY = Input.GetAxis("Mouse Y") * _rotationSpeedPC;

            if (!Mathf.Approximately(mouseX, 0.0f) || !Mathf.Approximately(mouseY, 0.0f))
            {
                transform.Rotate(Vector3.up, -mouseX, Space.World);
            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                Debug.Log($"Click UI touch.fingerId {touch.fingerId}");
                return;
            }
        }

        if (Input.touchCount > 0)
        {
            if (Touchscreen.current == null || Touchscreen.current.touches.Count == 0)
                return;

            int touchIndex = 0;

            if (Touchscreen.current.touches.Count > 1 && Touchscreen.current.touches[1].isInProgress)
            {
                touchIndex = 1;
            }

            if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[touchIndex].touchId.ReadValue()))
            {
                Debug.Log($"Click UI touchIndex {touchIndex}");
                return;
            }

            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Moved)
            {
                float touchDeltaX = touch.deltaPosition.x * _rotationSpeedMobile;
                float touchDeltaY = touch.deltaPosition.y * _rotationSpeedMobile;

                transform.Rotate(Vector3.up, -touchDeltaX, Space.World);
            }
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                Debug.Log($"Click UI touch.fingerId {touch.fingerId}");
            }
        }
#endif
    }
}