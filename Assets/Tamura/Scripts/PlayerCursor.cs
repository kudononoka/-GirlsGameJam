using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>プレイヤーのカーソルを動かしたり変えたりする</summary>
public class PlayerCursor : MonoBehaviour
{
    [SerializeField, Header("カーソル達の親オブジェクト")] GameObject _cursors = default;
    [SerializeField, Header("熱ビームのカーソル")] SpriteRenderer _beamCursor = default;
    [SerializeField, Header("ボム範囲のカーソル")] SpriteRenderer _bombCursor = default;
    [SerializeField, Header("ボム爆発")] GameObject _bomber = default;
    Vector3 _worldPos = default;

    void Start()
    {
        //ボムカーソルだけ消しとく
        //_bombCursor.SetActive(false);
        _bombCursor.color = Color.clear;
    }

    void Update()
    {
        //カーソルの位置をマウスの位置にする
        var mousePos = Input.mousePosition;
        _worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        _cursors.transform.position = _worldPos;

        if(Input.GetMouseButtonDown(0)) //マウス左押したとき
        {
            //ボムカーソル消してビームカーソル赤にする
            _bombCursor.color = Color.clear;
            _beamCursor.color = Color.red;
        }
        //else if(Input.GetMouseButton(0)) //マウス左押しっぱなしのとき
        //{

        //}
        else if(Input.GetMouseButtonUp(0)) //マウス左離したとき
        {
            //ビームカーソルの色戻す
            _beamCursor.color = Color.white;
        }
        else if(Input.GetMouseButtonDown(1)) //マウス右押したとき
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
            //ボムカーソル消してビームカーソル出す
            _bombCursor.color = Color.clear;
            _beamCursor.color = Color.white;
            Instantiate(_bomber, _worldPos, Quaternion.identity);
        }

    }

}
