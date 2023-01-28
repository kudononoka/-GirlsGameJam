using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("�X�|�[���ʒu")]
    [SerializeField]Transform[] _spawnTransform;
    [Tooltip("������v���n�u")]
    [SerializeField] GameObject _enemy;
    [Tooltip("�o�Ă���N�[���^�C��")]
    [SerializeField] float _intervalTime;
    [Tooltip("�N�[���^�C�����炷��")]
    [SerializeField] float _intervalLower;
    [Tooltip("�ڕW�l")]
    [SerializeField] int _updateCount;
    [Tooltip("�J�E���g�A�b�v")]
    [SerializeField] int _countUp;

    /// <summary>�l���J�E���g</summary>
    [SerializeField]int _count;
    /// <summary>�v��</summary>
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
            }//�N�[���^�C�����}�C�i�X�ɂȂ�Ȃ��悤�ɍŏ��l�i0.5�j����ꂽ
        }//�N�[���^�C�������炷

        if(_interval >= _intervalTime)
        {
            //�|�W�V�������I
            int pos = Random.Range(0, gameObject.transform.childCount);
            Instantiate(_enemy, _spawnTransform[pos]);
            _interval = 0;
        }//�X�|�[��
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
