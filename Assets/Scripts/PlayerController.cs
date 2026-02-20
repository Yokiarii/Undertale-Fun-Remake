using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D Rb;
    public float Speed = 0.35f;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Keyboard.current.upArrowKey.isPressed)
            Rb.AddForceY(Speed);
        if (Keyboard.current.downArrowKey.isPressed)
            Rb.AddForceY(-Speed);
        if (Keyboard.current.rightArrowKey.isPressed)
            Rb.AddForceX(Speed);
        if (Keyboard.current.leftArrowKey.isPressed)
            Rb.AddForceX(-Speed);
    }
}
