using UnityEngine;

public class SharokuScript : MonoBehaviour
{
    private static SharokuScript _instance;
    public static SharokuScript Instance => _instance;

    public GameObject Hat;
    public GameObject Head;
    public GameObject Body;
    public GameObject Arm_Left;
    public GameObject Arm_Right;
    public GameObject Leg_Left;
    public GameObject Leg_Right;

    
    void Awake()
    {
        _instance = this;
    }


}
