using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("スポーン位置")]
    [SerializeField]Transform[] _spawnTransform;
    [Tooltip("女さんプレハブ")]
    [SerializeField] GameObject _enemy;
    [Tooltip("出てくるクールタイム")]
    [SerializeField] float _intervalTime;
    [Tooltip("クールタイム減らす量")]
    [SerializeField] float _intervalLower;
    [Tooltip("目標値")]
    [SerializeField] int _updateCount;
    [Tooltip("カウントアップ")]
    [SerializeField] int _countUp;

    /// <summary>人数カウント</summary>
    [SerializeField]int _count;
    /// <summary>計測</summary>
    float _interval;
    

    void Start()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            _spawnTransform[i] = gameObject.transform.GetChild(i);
        }
    }

    void Update()
    {
        _interval += Time.deltaTime;

        if(_count >= _updateCount && _intervalTime >= 1)
        {
            _intervalTime -= _intervalLower;
            _updateCount += _countUp;

            if(_intervalTime <= 1)
            {
                _intervalTime = 0.5f;
            }//クールタイムがマイナスにならないように最小値（0.5）を入れた
        }//クールタイムを減らす

        if(_interval >= _intervalTime)
        {
            //ポジション抽選
            int pos = Random.Range(0, gameObject.transform.childCount);
            Instantiate(_enemy, _spawnTransform[pos]);
            _interval = 0;
        }//スポーン
    }
    public int Count()
    {
        return _count;
    }

    public void CountPuls()
    {
        _count++;
    }
}
