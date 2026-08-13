using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D Rb;
    public float Speed = 0.35f;
    private bool isMobile = false;



    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

#if UNITY_ANDROID
        isMobile = true;
#else
        isMobile = false;
#endif

    }
    void Update()
    {
        if (Player.Instance.IsDead)
            return;
        if (SharokuScript.Instance.ZABAVKA.IsReady && !SharokuScript.Instance.ZABAVKA.IsClose)
        {
            if (Keyboard.current.upArrowKey.isPressed)
                Rb.AddForceY(-Speed);
            if (Keyboard.current.downArrowKey.isPressed)
                Rb.AddForceY(Speed);
            if (Keyboard.current.rightArrowKey.isPressed)
                Rb.AddForceX(-Speed);
            if (Keyboard.current.leftArrowKey.isPressed)
                Rb.AddForceX(Speed);
            return;
        }
        if (Keyboard.current.upArrowKey.isPressed)
            Rb.AddForceY(Speed);
        if (Keyboard.current.downArrowKey.isPressed)
            Rb.AddForceY(-Speed);
        if (Keyboard.current.rightArrowKey.isPressed)
            Rb.AddForceX(Speed);
        if (Keyboard.current.leftArrowKey.isPressed)
            Rb.AddForceX(-Speed);

        if (isMobile)
            AndroidController();
    }

    public void AndroidController()
    {
        Touchscreen touch = Touchscreen.current;

        if (touch.primaryTouch.press.isPressed)
        {
            Vector2 mouseScreenPos = touch.primaryTouch.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            // Вычисляем направление от игрока к позиции мыши
            Vector2 direction = (Vector2)mouseWorldPos - (Vector2)transform.position;
            mouseWorldPos.z = transform.position.z;

            // Нормализуем направление, чтобы скорость была одинаковой в разных направлениях
            direction.Normalize();

            // Применяем силу к Rigidbody2D для движения
            Rb.linearVelocity = direction * (Speed / 2);
        }
        else
        {
            // Если кнопка мыши не нажата, останавливаем игрока
            Rb.linearVelocity = Vector2.zero;
        }
    }
}
