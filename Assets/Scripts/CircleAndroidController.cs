using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircleAndroidController : MonoBehaviour
{
    public GameObject Circle;
    public bool IsReturning = true;

    void Update()
    {
        Controller();
        ReturnCircle();
    }

    void ReturnCircle()
    {
        if (!IsReturning)
            return;
        if (new Vector3(0, 0).magnitude > 0.01f)
            return;

        Circle.transform.DOLocalMove(new Vector3(0, 0), 0.01f);
    }
    void Controller()
    {
        Touchscreen touch = Touchscreen.current;
        if (touch.primaryTouch.press.isPressed)
        {
            IsReturning = false;
            var MousePos = Camera.main.ScreenToWorldPoint(touch.primaryTouch.position.ReadValue());

            Vector2 direction = (Vector2)MousePos - (Vector2)Circle.transform.position;
            MousePos.z = transform.position.z;
            float distance = direction.magnitude;

            float speed = Mathf.Min(35f, distance * 20f); // Коэффициент 5 можно настроить

            if (distance > 0.01f)
            {
                direction.Normalize();
                Circle.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
                Player.Instance.PlayerGameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * 3;
            }
            else
            {
                Circle.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                Player.Instance.PlayerGameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
        }
        else
        {
            Circle.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            Player.Instance.PlayerGameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            IsReturning = true;
        }
    }
}
