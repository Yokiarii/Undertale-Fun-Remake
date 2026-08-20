using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;

public class SpriteDisassembler : MonoBehaviour
{
    [Header("Настройки разбиения")]
    public int columns = 10;
    public int rows = 10;

    [Header("Настройки разлёта")]
    public float flyDuration = 1.5f;
    public float disappearDelay = 0.3f;
    public float spreadRadius = 500f;

    [Header("Эффекты")]
    public bool randomDirection = true;
    public bool fadeOut = true;

    private Image imageComponent;
    private RectTransform rectTransform;
    private GameObject fragmentsParent;
    private List<RectTransform> fragments = new List<RectTransform>();


    public void Dissolve()
    {
        imageComponent = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        
        if (imageComponent == null)
        {
            Debug.LogError("Нет компонента Image на этом объекте!");
            return;
        }

        if (fragments.Count > 0)
        {
            ClearFragments();
        }

        CreateFragments();
    }

    void CreateFragments()
    {
        Sprite originalSprite = imageComponent.sprite;
        if (originalSprite == null)
        {
            Debug.LogError("Нет спрайта в Image!");
            return;
        }

        Texture2D texture = originalSprite.texture;
        Rect spriteRect = originalSprite.rect;

        // Размер одного фрагмента в пикселях текстуры
        float fragmentPixelWidth = spriteRect.width / columns;
        float fragmentPixelHeight = spriteRect.height / rows;

        // Получаем размер исходного спрайта в UI-координатах
        Vector2 spriteUISize = rectTransform.rect.size;
        float fragmentUIWidth = spriteUISize.x / columns;
        float fragmentUIHeight = spriteUISize.y / rows;

        // Получаем pivot исходного спрайта
        Vector2 sourcePivot = rectTransform.pivot;

        // СОЗДАЁМ РОДИТЕЛЬСКИЙ ОБЪЕКТ
        if (fragmentsParent == null)
        {
            fragmentsParent = new GameObject("FragmentsParent");
            
            // Добавляем RectTransform
            RectTransform parentRect = fragmentsParent.AddComponent<RectTransform>();
            
            // Устанавливаем родителем Canvas
            fragmentsParent.transform.SetParent(transform.parent, false);
            
            // КОПИРУЕМ ВСЕ НАСТРОЙКИ С ИСХОДНОГО IMAGE
            parentRect.anchorMin = rectTransform.anchorMin;
            parentRect.anchorMax = rectTransform.anchorMax;
            parentRect.pivot = sourcePivot; // Важно: копируем pivot!
            parentRect.anchoredPosition = rectTransform.anchoredPosition;
            parentRect.sizeDelta = rectTransform.sizeDelta;
            parentRect.localScale = rectTransform.localScale;
            parentRect.localRotation = rectTransform.localRotation;
            parentRect.localPosition = new Vector3(-3f,236.55f,0);
        }

        // Получаем RectTransform родителя
        RectTransform parentRectTransform = fragmentsParent.GetComponent<RectTransform>();
        
        // Вычисляем смещение от центра родителя к его левому нижнему углу
        // с учётом pivot родителя
        Vector2 parentSize = parentRectTransform.rect.size;
        Vector2 pivotOffset = new Vector2(
            -parentSize.x * parentRectTransform.pivot.x,
            -parentSize.y * parentRectTransform.pivot.y
        );
        
        // Позиция левого нижнего угла родителя в локальных координатах
        Vector2 parentLeftBottom = pivotOffset;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                // Координаты пикселя в текстуре
                float pixelX = spriteRect.x + x * fragmentPixelWidth;
                float pixelY = spriteRect.y + y * fragmentPixelHeight;

                // Создаём спрайт-фрагмент
                Sprite fragmentSprite = Sprite.Create(
                    texture,
                    new Rect(pixelX, pixelY, fragmentPixelWidth, fragmentPixelHeight),
                    new Vector2(0.5f, 0.5f),
                    originalSprite.pixelsPerUnit
                );

                // Создаём GameObject для фрагмента
                GameObject fragmentObj = new GameObject($"Fragment_{x}_{y}");
                fragmentObj.transform.SetParent(fragmentsParent.transform, false);

                // Добавляем Image
                Image fragmentImage = fragmentObj.AddComponent<Image>();
                fragmentImage.sprite = fragmentSprite;
                fragmentImage.raycastTarget = false;

                // Настраиваем RectTransform фрагмента
                RectTransform fragRect = fragmentObj.GetComponent<RectTransform>();
                
                // ВАЖНО: фрагменты используют ТОТ ЖЕ PIVOT, что и родитель
                fragRect.anchorMin = new Vector2(0, 0);
                fragRect.anchorMax = new Vector2(0, 0);
                fragRect.pivot = sourcePivot; // Копируем pivot исходного спрайта
                fragRect.sizeDelta = new Vector2(fragmentUIWidth, fragmentUIHeight);

                // Вычисляем позицию фрагмента в локальных координатах родителя
                // с учётом его pivot
                float localX = parentLeftBottom.x + x * fragmentUIWidth + fragmentUIWidth / 2;
                float localY = parentLeftBottom.y + y * fragmentUIHeight + fragmentUIHeight / 2;
                
                // Корректируем позицию с учётом pivot фрагмента
                // Так как pivot фрагмента = sourcePivot, нужно сместить позицию
                Vector2 fragmentPivotOffset = new Vector2(
                    -fragmentUIWidth * (sourcePivot.x - 0.5f),
                    -fragmentUIHeight * (sourcePivot.y - 0.5f)
                );
                
                fragRect.anchoredPosition = new Vector2(localX, localY) + fragmentPivotOffset;
                fragRect.anchorMin = new Vector2(0.5f,0.5f);
                fragRect.anchorMax = new Vector2(0.5f,0.5f);

                fragments.Add(fragRect);
            }
        }

        // Скрываем исходный Image
        imageComponent.enabled = false;
        StartCoroutine(AnimateFragments());
    }

    IEnumerator AnimateFragments()
    {
        foreach (var item in fragments)
        {
            var temp = new Vector3(Random.Range(-40.2f,40.2f),Random.Range(-40.2f,-10));
            item.gameObject.transform.DOLocalMove(new Vector3(item.gameObject.transform.localPosition.x + temp.x
            ,item.gameObject.transform.localPosition.y + temp.y),6f).SetEase(Ease.InQuad);
            item.GetComponent<Image>().DOFade(0,6f).SetEase(Ease.InOutSine);
            yield return new WaitForSecondsRealtime(0.0001f);
        }
        yield break;
    }

    void ClearFragments()
    {
        foreach (RectTransform frag in fragments)
        {
            if (frag != null) Destroy(frag.gameObject);
        }
        fragments.Clear();
        
        if (fragmentsParent != null)
        {
            Destroy(fragmentsParent);
            fragmentsParent = null;
        }
    }
}