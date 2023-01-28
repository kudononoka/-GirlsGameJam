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
    [SerializeField, Header("熱量")] float _power = 100;
    [SerializeField, Header("熱量の最大値")] float _maxPower = 100;
    [SerializeField, Header("ボムに使う熱量")] float _bombPower = 30;

    /// <summary>残りの熱量</summary>
    public float Power { get => _power; }

    void Start()
    {
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

        if (_power > 0)
        {

            if (Input.GetMouseButtonDown(0)) //マウス左押したとき
            {
                //ボムカーソル消してビームカーソル赤にする
                _bombCursor.color = Color.clear;
                _beamCursor.color = Color.red;
                //コライダーオン
                _beamCollider.enabled = true;
            }
            else if (Input.GetMouseButton(0)) //マウス左押しっぱなしのとき
            {
                _power -= Time.deltaTime;
            }
            else if (Input.GetMouseButtonUp(0)) //マウス左離したとき
            {
                //ビームカーソルの色戻す
                _beamCursor.color = Color.white;
                //コライダーオフ
                _beamCollider.enabled = false;
            }
            else
            {
                _power += Time.deltaTime;
            }

            if (Input.GetMouseButtonDown(1)) //マウス右押したとき
            {
                //ビームカーソル消してボムカーソル出す
                _beamCursor.color = Color.clear;
                _bombCursor.color = Color.white;
            }
            //else if(Input.GetMouseButton(1)) //マウス右押しっぱなしのとき
            //{

            //}
            else if (Input.GetMouseButtonUp(1)) //マウス右離したとき
            {
                if(_power > _bombPower)
                {
                    //熱量が足りてる時
                    Instantiate(_bomber, worldPos, Quaternion.identity);
                    _power -= _bombPower;
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
        else
        {
            _power += Time.deltaTime;
            //ビームカーソルの色戻す
            _beamCursor.color = Color.white;
            //コライダーオフ
            _beamCollider.enabled = false;
        }

        if(_power > _maxPower)
        {
            _power = _maxPower;
        }

    }

}
