using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Pistol_Script_alt_2 : MonoBehaviour
{
    public GameObject MainObject;
    public GameObject Bullet;
    public GameObject PistolModel;
    public GameObject PistolModelTemp;
    public GameObject bomb;
    public GameObject Hat;
    public Sprite OriginalArmSprite;
    public bool IsAiming = true;
    public bool IsHatOut = true;
    public bool IsHatAttacking = false;
    public bool IsHatHeal = false;
    public bool IsStopMoving = true;
    public List<Sprite> Anim = new List<Sprite>();
    public Image Arm;

    void Start()
    {
        Hat = SharokuScript.Instance.Hat;
        Arm = SharokuScript.Instance.Arm_Left.GetComponent<Image>();
        OriginalArmSprite = Arm.sprite;
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

        StartCoroutine(Shoot(Arm.gameObject.GetComponent<Image>()));
    }

    IEnumerator PistolLogic()
    {
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
        IsAiming = true;

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

        yield return new WaitForSeconds(3f);
        Speech.Instance.Say("Не знаю зачем тебе, но если так хочешь..<Лови!");
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(PistolLogic());

        yield return new WaitForSeconds(3f);
        Hat.transform.SetParent(FunnyBox.Instance.gameObject.transform);
        StartCoroutine(HatIdleAnimation());
        ShootAnim();

        while (!HatColliderScript.Instance.PlayerFinallyCatch)
        {
            yield return new WaitForSeconds(0.17f);
            ShootAnim();
            yield return new WaitForSeconds(0.17f);
            ShootAnim();
            yield return new WaitForSeconds(0.10f);
            ShootAnim();
            yield return new WaitForSeconds(0.52f);
            ShootAnim();
            yield return new WaitForSeconds(0.3f);
            ShootAnim();
            yield return new WaitForSeconds(1f);

            StartCoroutine(QuitAnimAttack(Arm));

            yield return new WaitForSeconds(1.5f);

            yield return new WaitForSeconds(0.3f);
            BombAttack();
            yield return new WaitForSeconds(0.5f);
            BombAttack();
            yield return new WaitForSeconds(2f);
            StartCoroutine(PistolLogic());
            yield return new WaitForSeconds(2f);
            ShootAnim();
            yield return new WaitForSeconds(0.17f);
            ShootAnim();
            yield return new WaitForSeconds(0.17f);
            ShootAnim();
            yield return new WaitForSeconds(0.3f);
            ShootAnim();
            yield return new WaitForSeconds(0.17f);
            ShootAnim();
            yield return new WaitForSeconds(0.17f);
            ShootAnim();
            yield return new WaitForSeconds(3f);

            StartCoroutine(QuitAnimAttack(Arm));

            StartCoroutine(HatAttackAnimation());

            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;

            yield return new WaitForSeconds(5f);

            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;

            BombAttack();
            yield return new WaitForSeconds(0.1f);
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            BombAttack();
            yield return new WaitForSeconds(1f);
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            StartCoroutine(PistolLogic());
            yield return new WaitForSeconds(2f);
            ShootAnim();
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            yield return new WaitForSeconds(0.17f);
            ShootAnim();
            yield return new WaitForSeconds(0.17f);
            ShootAnim();
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            yield return new WaitForSeconds(0.1f);
            ShootAnim();
            yield return new WaitForSeconds(4f);
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            ShootAnim();
            yield return new WaitForSeconds(3f);
            StartCoroutine(QuitAnimAttack(Arm));
            yield return new WaitForSeconds(1f);
            BombAttack();
            yield return new WaitForSeconds(0.2f);
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            BombAttack();
            yield return new WaitForSeconds(4f);

            StartCoroutine(HatAttackAnimation());

            yield return new WaitForSeconds(5f);

            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;


            StartCoroutine(PistolLogic());

            yield return new WaitForSeconds(2f);
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            ShootAnim();
            yield return new WaitForSeconds(0.1f);
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            ShootAnim();
            yield return new WaitForSeconds(0.1f);
            ShootAnim();
            yield return new WaitForSeconds(2f);
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            StartCoroutine(QuitAnimAttack(Arm));
            yield return new WaitForSeconds(0.2f);
            BombAttack();
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            yield return new WaitForSeconds(0.2f);
            BombAttack();
            if(HatColliderScript.Instance.PlayerFinallyCatch)
                break;
            yield return new WaitForSeconds(2f);
            StartCoroutine(PistolLogic());
            yield return new WaitForSeconds(1.5f);
            ShootAnim();
            yield return new WaitForSeconds(0.08f);
            ShootAnim();
            yield return new WaitForSeconds(0.08f);
            ShootAnim();
            yield return new WaitForSeconds(0.8f);
            StartCoroutine(QuitAnimAttack(Arm));

            yield return new WaitForSeconds(3f);
            BombAttack();
            yield return new WaitForSeconds(2f);
            BombAttack();
            yield return new WaitForSeconds(1f);
            BombAttack();
            yield return new WaitForSeconds(1f);
            StartCoroutine(PistolLogic());
            yield return new WaitForSeconds(1.5f);
            ShootAnim();
            yield return new WaitForSeconds(0.08f);
            ShootAnim();
            yield return new WaitForSeconds(0.08f);
            ShootAnim();
            yield return new WaitForSeconds(0.08f);
            ShootAnim();
            yield return new WaitForSeconds(0.08f);
            ShootAnim();
            yield return new WaitForSeconds(0.08f);
            ShootAnim();
            yield return new WaitForSeconds(1f);
            StartCoroutine(QuitAnimAttack(Arm));
            yield return new WaitForSeconds(1f);
            BombAttack();
            yield return new WaitForSeconds(2f);
            StartCoroutine(PistolLogic());
            yield return new WaitForSeconds(1.5f);
        }

        IsHatHeal = false;
        IsHatOut = false;
        HatColliderScript.Instance.IsHeal = true;

        yield return new WaitForSeconds(2f);

        Hat.transform.SetParent(Player.Instance.PlayerGameObject.transform);
        Hat.transform.DOLocalMove(new Vector3(0,41.94f),1f).SetEase(Ease.InOutQuint);
        Hat.transform.DOLocalRotate(new Vector3(0,0,0),1f).SetEase(Ease.InOutQuint);

        yield return new WaitForSeconds(2f);
        Speech.Instance.Say("...");
        yield return new WaitForSeconds(5f);
        Speech.Instance.Say("Ну чтож");
        StartCoroutine(QuitAnimAttack(Arm));
        yield return new WaitForSeconds(5f);
        Speech.Instance.Say("Ну, я не знаю что еще делать");
         yield return new WaitForSeconds(4f);
        Speech.Instance.Say("Давай на этом закончим.");
        yield return new WaitForSeconds(6f);
        
        string[] tempText = new string[2]{Act.Instance.TextOfCells[1],Act.Instance.TextOfCells[2]};
        Act.Instance.TextOfCells[0] = tempText[0];
        Act.Instance.TextOfCells[1] = tempText[1];
        
        GameObject[] tempObj = new[]{Act.Instance.cellObjects[0],Act.Instance.cellObjects[1]};
        Act.Instance.cellObjects[2].SetActive(false);
        Act.Instance.cellObjects = tempObj;

        SharokuScript.Instance.READY_TO_MERCY.IsReady = true;

        Fight.Instance.QuitFightExternal();
    }
    IEnumerator HatAttackAnimation()
    {
        if(IsHatAttacking == true)
            yield break;
        IsHatAttacking = true;
        IsHatOut = false;

        while(!IsStopMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(HatHealingAnimation());
        HatColliderScript.Instance.IsHeal = true;

        while (HatColliderScript.Instance.IsPlayerGrabbing == false && !HatColliderScript.Instance.PlayerFinallyCatch)
        {

            float durationTime = 3;
            Vector3 targetPos = new Vector3(0, 0);
            var temp = true;

            while (temp)
            {
                durationTime = UnityEngine.Random.Range(1, 2);
                targetPos = new Vector3(UnityEngine.Random.Range(-950, 950), UnityEngine.Random.Range(-180, 780));

                if (targetPos.y < 237.6 && targetPos.x > -588.54 && targetPos.x < 588.39)
                {
                    temp = false;
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }

            }

            if(HatColliderScript.Instance.IsPlayerGrabbing == true || HatColliderScript.Instance.PlayerFinallyCatch)
                break;

            Hat.transform
                .DOLocalMove(targetPos, durationTime)
                .SetEase(Ease.InOutQuint);
            Hat.transform.DOLocalRotate(new Vector3(0, 0, UnityEngine.Random.Range(-15, 15)), durationTime)
                .SetEase(Ease.InOutQuint);

            yield return new WaitForSeconds(durationTime);
        }

        if(HatColliderScript.Instance.PlayerFinallyCatch)
            yield break;

        IsHatHeal = false;
        IsHatAttacking = false;
        HatColliderScript.Instance.IsHeal = false;
        StartCoroutine(HatIdleAnimation());
        yield break;
    }
    IEnumerator HatIdleAnimation()
    {
        IsHatOut = true;
        while (IsHatOut && !HatColliderScript.Instance.PlayerFinallyCatch)
        {

            float durationTime = 3;
            Vector3 targetPos = new Vector3(0, 0);
            var temp = true;

            while (temp)
            {
                durationTime = UnityEngine.Random.Range(5, 7);
                
                targetPos = new Vector3(UnityEngine.Random.Range(-950, 950), UnityEngine.Random.Range(-180, 780));

                if (targetPos.y < 237.6 && targetPos.x > -588.54 && targetPos.x < 588.39)
                {
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    temp = false;
                }

            }

            if (!IsHatOut)
            {
                break;
            }

            IsStopMoving = false;

            Hat.transform
                .DOLocalMove(targetPos, durationTime)
                .SetEase(Ease.InOutQuint);
            Hat.transform.DOLocalRotate(new Vector3(0, 0, UnityEngine.Random.Range(-15, 15)), durationTime)
                .SetEase(Ease.InOutQuint);


            yield return new WaitForSeconds(durationTime);
            IsStopMoving = true;

        }
        yield break;
    }
    IEnumerator HatHealingAnimation()
    {
        IsHatHeal = true;
        StartCoroutine(HatHealDelay());
        while (IsHatHeal)
        {
            Hat.GetComponent<Image>().DOColor(Color.green, 0.5f);
            yield return new WaitForSeconds(0.5f);
            Hat.GetComponent<Image>().DOColor(Color.white, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
        Hat.GetComponent<Image>().DOColor(Color.red, 0.3f);
        yield return new WaitForSeconds(0.8f);
        Hat.GetComponent<Image>().DOColor(Color.white, 0.3f);
        yield break;
    }
    IEnumerator HatHealDelay()
    {
        yield return new WaitForSeconds(5f);
        IsHatHeal = false;
        HatColliderScript.Instance.IsPlayerGrabbing = false;
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
        if (Player.Instance.HP[0] < 15)
        {
            yield break;
        }
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

    void BombAttack(bool isLethal = true)
    {
        var temp = Instantiate(bomb, FunnyBox.Instance.gameObject.transform);
        temp.GetComponent<Bomb_Rush_Script>().IsLethal = isLethal;
    }

    IEnumerator QuitAnimAttack(Image Arm)
    {
        IsAiming = false;
        yield return new WaitForSeconds(1f);
        Arm.sprite = OriginalArmSprite;
        Arm.rectTransform.sizeDelta = new Vector2(56.66f, 245.54f);
        Arm.transform.localScale = new Vector3(1f, 1f);
        Arm.transform.eulerAngles = new Vector3(0, 0, 0);
        Destroy(PistolModelTemp);
        SharokuScript.Instance.Parts_Left_Hand.transform.SetAsFirstSibling();
        yield break;
    }
}
