using UnityEngine;

public class DissolveAnimator : MonoBehaviour
{
    public float duration = 1.5f;
    public float startDelay = 0f;
    public Color edgeColor = Color.white;
    public float edgeWidth = 0.05f;
    public bool destroyAfter = true;

    private SpriteRenderer sr;
    private Material mat;
    private float timer;
    private bool dissolving;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        Shader shader = Shader.Find("Custom/SpriteDissolve");
        mat = new Material(shader);
        sr.material = mat;
        
        mat.SetFloat("_EdgeWidth", edgeWidth);
        mat.SetColor("_EdgeColor", edgeColor);
        
        Invoke(nameof(Begin), startDelay);
    }

    void Begin()
    {
        dissolving = true;
    }

    void Update()
    {
        if (!dissolving) return;
        
        timer += Time.deltaTime;
        float progress = Mathf.Clamp01(timer / duration);
        
        // 0.5 до -0.5 (потому что pivot в центре, ноги на -0.5)
        mat.SetFloat("_DissolveAmount", 0.5f - progress);
        
        if (progress >= 1f)
        {
            if (destroyAfter) Destroy(gameObject);
            dissolving = false;
        }
    }
}