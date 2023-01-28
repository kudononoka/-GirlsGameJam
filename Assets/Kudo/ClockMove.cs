using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockMove : MonoBehaviour
{
    
    /// <summary>分針or秒針の進むスピード</summary>
    [SerializeField, Header("分針進むスピード")] float _speedHour;
    [SerializeField, Header("秒針進むスピード")] float _speedMinute;　
    /// <summary>ゲーム時間の最大値</summary>
    float _timeMax;

    [SerializeField] GameObject _hour;
    [SerializeField] GameObject _minute;
    // Start is called before the first frame update
    void Start()
    {
        _timeMax = FindObjectOfType<GameManager>().TimeLimit;
        _speedHour =  210 / _timeMax;
        _speedMinute = 360 / _timeMax * 7;
    }

    // Update is called once per frame
    void Update()
    {
        _hour.transform.RotateAround(transform.position, Vector3.back, _speedHour * Time.deltaTime);
        _minute.transform.RotateAround(transform.position, Vector3.back, _speedMinute * Time.deltaTime);
    }
}
