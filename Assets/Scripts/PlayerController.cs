using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D Rb;
    public float Speed = 0.35f;
    private bool isMobile = false;
    public bool isEnd = false;
    public GameObject AndroidCircle;

    private static PlayerController _instance;
    public static PlayerController Instance => _instance;

    void Awake()
    {
#if UNITY_ANDROID
        isMobile = true;
#else
        isMobile = false;
#endif
    }

    void Start()
    {
        _instance = this;
        Rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        if (SharokuScript.Instance.READY_TO_MERCY.IsReady)
            isEnd = true;
        if (isMobile && Main.Instance.PlayerUsesCircle)
            AndroidCircle.SetActive(true);
    }
    void OnDisable()
    {
        if (isMobile && Main.Instance.PlayerUsesCircle)
            AndroidCircle.SetActive(false);
    }

    void Update()
    {
        if (Player.Instance.IsDead)
            return;
        if (SharokuScript.Instance.ZABAVKA.IsReady && !SharokuScript.Instance.ZABAVKA.IsClose && !isMobile)
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

        if (isMobile && !Main.Instance.PlayerUsesCircle)
        {
            AndroidController();
        }
        if (isEnd && isMobile && Main.Instance.PlayerUsesCircle)
        {
            AndroidController();
        }
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

            if (SharokuScript.Instance.ZABAVKA.IsReady && !SharokuScript.Instance.ZABAVKA.IsClose)
            {
                Rb.linearVelocity = direction * (-Speed / 2);
            } else
            {
                Rb.linearVelocity = direction * (Speed / 2);
            }

        }
        else
        {
            // Если кнопка мыши не нажата, останавливаем игрока
            Rb.linearVelocity = Vector2.zero;
        }
    }
}
