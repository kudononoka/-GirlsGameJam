using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BomberAnim : MonoBehaviour
{
    [SerializeField, Header("「BOOM」って書いてあるやつ")] GameObject _boom = default;

    void Start()
    {
        _boom.transform.DOScaleX(1, 0.5f).SetEase(Ease.Linear).SetAutoKill();
        _boom.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).SetEase(Ease.Linear).SetDelay(0.5f).SetAutoKill();

        gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetAutoKill();
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).SetEase(Ease.Linear).SetDelay(0.5f)
            .OnComplete(() => Destroy(gameObject)).SetAutoKill();
    }

}
