using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>プレイヤーのカーソルを動かしたり変えたりする</summary>
public class PlayerCursor : MonoBehaviour
{
    [SerializeField, Header("カーソル達の親オブジェクト")] GameObject _cursors = default;
    [SerializeField, Header("熱ビームのカーソル")] SpriteRenderer _beamCursor = default;
    [SerializeField, Header("熱ビームのコライダー")] BoxCollider2D _beamCollider = default;
    [SerializeField, Header("ボム範囲のカーソル")] SpriteRenderer _bombCursor = default;
    [SerializeField, Header("ボム爆発")] GameObject _bomber = default;

    [SerializeField, Header("今の熱量")] float _power = 100;
    [SerializeField, Header("熱量の最大値")] float _maxPower = 100;
    [SerializeField, Header("ボムに使う熱量")] float _bombPower = 30;
    [SerializeField] bool _emptyPower = false;
    [SerializeField, Header("熱量が減る係数")] float _powerDownNum = 3.0f;
    [SerializeField, Header("熱量が回復する係数")] float _powerRecoverNum = 5.0f;
    [SerializeField, Header("熱ビームの発射音")] AudioClip _beamAudioClip;
    [SerializeField, Header("熱ビームの発射音連射の時用")] AudioClip _beamburstAudioClip;
    [SerializeField, Header("ボムの爆発音")] AudioClip _bombAudioClip;
    Tweener _flash = default;

    /// <summary>今の熱量</summary>
    public float Power { get => _power; }

    /// <summary>熱量の最大値</summary>
    public float MaxPower { get => _maxPower; }

    /// <summary>オーディオソースコンポーネント</summary>
    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //ボムカーソルだけ消しとく
        _bombCursor.color = Color.clear;
        _beamCollider.enabled = false;
    }

    void Update()
    {
        //カーソルの位置をマウスの位置にする
        var mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        _cursors.transform.position = worldPos;

        if (!_emptyPower)
        {

            if (_power > 0)
            {

                if (Input.GetMouseButtonDown(0)) //マウス左押したとき
                {
                    //ボムカーソル消してビームカーソル赤にする
                    _bombCursor.color = Color.clear;
                    _beamCursor.color = Color.red;
                    _flash =_beamCursor.DOColor(new Color(1, 0.5f, 0), 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetAutoKill();
                    //発射音
                    _audioSource.PlayOneShot(_beamAudioClip);
                    //コライダーオン
                    _beamCollider.enabled = true;
                    DownPower();
                }
                else if (Input.GetMouseButton(0)) //マウス左押しっぱなしのとき
                {
                    //発射音・連射ver
                    _audioSource.PlayOneShot(_beamburstAudioClip);
                    DownPower();
                }
                else if (Input.GetMouseButtonUp(0)) //マウス左離したとき
                {
                    //ビームカーソルの色戻す
                    _beamCursor.color = Color.white;
                    _flash.Kill();
                    //コライダーオフ
                    _beamCollider.enabled = false;
                }
                else
                {
                    RecoveryPower();

                    if (Input.GetMouseButtonDown(1)) //マウス右押したとき
                    {
                        //ビームカーソル消してボムカーソル出す
                        _beamCursor.color = Color.clear;
                        _bombCursor.color = new Color(1, 0.1951895f, 0, 0.5882353f);
                    }
                    //else if (Input.GetMouseButton(1)) //マウス右押しっぱなしのとき
                    //{

                    //}
                    else if (Input.GetMouseButtonUp(1)) //マウス右離したとき
                    {

                        if (_power >= _bombPower)
                        {
                            //熱量が足りてる時
                            Instantiate(_bomber, worldPos, Quaternion.identity);
                            _power -= _bombPower;
                            //ボム爆発音
                            _audioSource.PlayOneShot(_bombAudioClip);
                        }
                        else
                        {
                            //熱量が足りなかったとき
                        }

                        //ボムカーソル消してビームカーソル出す
                        _bombCursor.color = Color.clear;
                        _beamCursor.color = Color.white;
                    }

                }

            }
            else //熱量がなくなったら
            {
                //ビームカーソルの色戻す
                _beamCursor.color = Color.white;
                _flash.Kill();
                //コライダーオフ
                _beamCollider.enabled = false;
                //なくなったよオン
                _emptyPower = true;
            }

        }
        else
        {
            RecoveryPower();

            //一定まで回復したらオフ
            if(_power > _maxPower)
            {
                _emptyPower = false;
            }

        }

        //最大値を超えてたら最大値に書き換える
        if(_power > _maxPower)
        {
            _power = _maxPower;
        }

    }

    /// <summary>熱量を回復する</summary>
    private void RecoveryPower()
    {
        _power += Time.deltaTime * _powerRecoverNum;
    }

    /// <summary>熱量が減る</summary>
    private void DownPower()
    {
        _power -= Time.deltaTime * _powerDownNum;
    }
}
