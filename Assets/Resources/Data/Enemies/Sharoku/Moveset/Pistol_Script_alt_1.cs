using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Pistol_Script_alt_1 : MonoBehaviour
{
    public GameObject MainObject;
    public GameObject Bullet;
    public GameObject PistolModel;
    public GameObject PistolModelTemp;
    public Sprite OriginalArmSprite;
    public bool IsAiming = true;
    public List<Sprite> Anim = new List<Sprite>();

    void Start()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>("SpriteAtlas/SHAROK ATL");

        Anim.Add(System.Array.Find(allSprites, sprite => sprite.name == "Anim_Pistol_0"));
        Anim.Add(System.Array.Find(allSprites, sprite => sprite.name == "Anim_Pistol_1"));
        Anim.Add(System.Array.Find(allSprites, sprite => sprite.name == "Anim_Pistol_2"));
        Anim.Add(System.Array.Find(allSprites, sprite => sprite.name == "Anim_Pistol_3"));
        Anim.Add(System.Array.Find(allSprites, sprite => sprite.name == "Anim_Pistol_4"));
        Anim.Add(System.Array.Find(allSprites, sprite => sprite.name == "Anim_Pistol_5"));
        Anim.Add(System.Array.Find(allSprites, sprite => sprite.name == "Anim_Pistol_6"));
        Anim.Add(System.Array.Find(allSprites, sprite => sprite.name == "Anim_Pistol_7"));
        Anim.Add(System.Array.Find(allSprites, sprite => sprite.name == "Anim_Pistol_8"));

        Debug.Log("Pistol attack is started");
        StartCoroutine(PistolLogic());
    }

    IEnumerator PistolLogic()
    {
        var Arm = SharokuScript.Instance.Arm_Left.GetComponent<Image>();
        OriginalArmSprite = Arm.sprite;

        Arm.sprite = Anim[0];
        Arm.rectTransform.sizeDelta = new Vector2(20, 35);
        Arm.transform.localScale = new Vector3(8f, 8f);

        SharokuScript.Instance.Parts_Left_Hand.transform.SetAsLastSibling();

        yield return new WaitForSeconds(0.12f);
        Arm.sprite = Anim[1];
        yield return new WaitForSeconds(0.12f);
        Arm.sprite = Anim[2];
        yield return new WaitForSeconds(0.12f);
        Arm.sprite = Anim[3];
        yield return new WaitForSeconds(0.07f);
        Arm.sprite = Anim[4];
        yield return new WaitForSeconds(0.07f);
        Arm.sprite = Anim[5];
        yield return new WaitForSeconds(0.07f);
        Arm.sprite = Anim[6];
        yield return new WaitForSeconds(0.07f);
        Arm.sprite = Anim[7];
        yield return new WaitForSeconds(0.07f);
        Arm.sprite = Anim[8];
        PistolModelTemp = Instantiate(PistolModel, SharokuScript.Instance.Arm_Left.transform);
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(Aiming(Arm.transform));
    }
    IEnumerator Aiming(Transform Arm)
    {

        StartCoroutine(Shoot(Arm.gameObject.GetComponent<Image>()));

        while (IsAiming)
        {
            var direction = Player.Instance.PlayerGameObject.transform.position - Arm.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Arm.DORotateQuaternion(Quaternion.Euler(0, 0, angle + 125f), 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }
    IEnumerator Shoot(Image Arm)
    {

        yield return new WaitForSeconds(2f);
        Speech.Instance.Say("Паф паф!");
        ShootAnim();
        yield return new WaitForSeconds(0.5f);
        ShootAnim();
        yield return new WaitForSeconds(5f);
        Speech.Instance.Say("Па-па-паф!");
        ShootAnim();
        yield return new WaitForSeconds(0.3f);
        ShootAnim();
        yield return new WaitForSeconds(0.3f);
        ShootAnim();
        yield return new WaitForSeconds(3.25f);
        Speech.Instance.Say("Паф!");
        ShootAnim();
        yield return new WaitForSeconds(4f);
        Speech.Instance.Say("Паф...");
        ShootAnim();
        yield return new WaitForSeconds(0.75f);
        ShootAnim();
        yield return new WaitForSeconds(1.5f);
        Speech.Instance.Say("Хе");

        yield return new WaitForSeconds(2f);
        StartCoroutine(QuitAttack(Arm));
    }
    void ShootAnim()
    {
        Main.Instance.AllSpace.transform.DOShakePosition(0.2f, 4, 15, 50);
        SoundManagerUi.Instance.PlaySound("fireshot");
        PistolModelTemp.transform.DOLocalJump(PistolModelTemp.transform.localPosition, 1f, 1, 0.2f);
        StartCoroutine(FireAnim());
        var temp = Instantiate(Bullet, SharokuScript.Instance.Arm_Left.transform);
        temp.transform.localScale = new Vector3(1.7f, 1.7f);
        temp.transform.localPosition = new Vector3(0, -1, -5);
        temp.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-10f, -15), ForceMode2D.Impulse);
        temp.transform.SetParent(transform);
        temp.transform.localPosition = new Vector3(temp.transform.localPosition.x, temp.transform.localPosition.y, -1);
        StartCoroutine(BulletCorrector(temp));

    }
    IEnumerator FireAnim()
    {
        List<Transform> directChildren = new List<Transform>();
        foreach (Transform child in PistolModelTemp.transform)
        {
            directChildren.Add(child);
        }
        directChildren[0].gameObject.SetActive(true);
        directChildren[1].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.06f);
        directChildren[0].gameObject.SetActive(false);
        directChildren[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.06f);
        directChildren[0].gameObject.SetActive(false);
        directChildren[1].gameObject.SetActive(false);
        yield break;
    }
    IEnumerator BulletCorrector(GameObject Bullet)
    {
        if (UnityEngine.Random.Range(0, 100) > 50)
        {
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceY(120);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceY(120);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-320f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(320f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-320f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceY(320);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-220f);
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceY(120);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceY(120);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-420f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(320f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-320f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(320f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceY(320);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(-220f);
            yield return new WaitForSeconds(0.02f);
            Bullet.GetComponent<Rigidbody2D>().AddForceX(220f);
        }


        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 5; i++)
        {
            Bullet.transform.SetParent(FunnyBox.Instance.gameObject.transform);
            Bullet.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            Vector2 direction = (Player.Instance.PlayerGameObject.transform.localPosition - Bullet.transform.localPosition).normalized;
            Bullet.GetComponent<Rigidbody2D>().AddForce(direction * 10f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }
    IEnumerator QuitAttack(Image Arm)
    {
        IsAiming = false;
        yield return new WaitForSeconds(1f);
        Arm.sprite = OriginalArmSprite;
        Arm.rectTransform.sizeDelta = new Vector2(56.66f, 245.54f);
        Arm.transform.localScale = new Vector3(1f, 1f);
        Arm.transform.eulerAngles = new Vector3(0, 0, 0);
        Destroy(PistolModelTemp);
        SharokuScript.Instance.Parts_Left_Hand.transform.SetAsFirstSibling();

        Debug.Log("Attack was ended.");
        Fight.Instance.QuitFightExternal();

        yield break;
    }
}
