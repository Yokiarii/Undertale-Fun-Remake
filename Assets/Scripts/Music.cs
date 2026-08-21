using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource audio;
    [SerializeField] private AudioClip[] Source = new AudioClip[2]; 
    private static Music _instance;
    public static Music Instance => _instance;
    void Start()
    {
        _instance = this;
    }
    
    public void StartMusic(int music)
    {
        audio.Stop();
        audio.clip = Source[music];
        audio.Play();
    }
}
