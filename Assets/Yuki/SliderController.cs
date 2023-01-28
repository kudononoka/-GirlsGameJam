using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{

   [SerializeField, Tooltip("カーソルのゲームオブジェクト")]  GameObject _cursor = default;
    PlayerCursor _playerCursor = default;
    Slider _slider = default;

    void Awake()
    {
        _slider = GetComponent<Slider>();
        _playerCursor = _cursor.GetComponent<PlayerCursor>();
        _slider.maxValue = _playerCursor.MaxPower;
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _playerCursor.Power;
    }
}
