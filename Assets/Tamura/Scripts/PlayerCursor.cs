using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>�v���C���[�̃J�[�\���𓮂�������ς����肷��</summary>
public class PlayerCursor : MonoBehaviour
{
    [SerializeField, Header("�J�[�\���B�̐e�I�u�W�F�N�g")] GameObject _cursors = default;
    [SerializeField, Header("�M�r�[���̃J�[�\��")] SpriteRenderer _beamCursor = default;
    [SerializeField, Header("�M�r�[���̃R���C�_�[")] BoxCollider2D _beamCollider = default;
    [SerializeField, Header("�{���͈͂̃J�[�\��")] SpriteRenderer _bombCursor = default;
    [SerializeField, Header("�{������")] GameObject _bomber = default;

    [SerializeField, Header("���̔M��")] float _power = 100;
    [SerializeField, Header("�M�ʂ̍ő�l")] float _maxPower = 100;
    [SerializeField, Header("�{���Ɏg���M��")] float _bombPower = 30;
    [SerializeField] bool _emptyPower = false;

    Tweener _flash = default;

    /// <summary>���̔M��</summary>
    public float Power { get => _power; }

    /// <summary>�M�ʂ̍ő�l</summary>
    public float MaxPower { get => _maxPower; }

    void Start()
    {
        //�{���J�[�\�����������Ƃ�
        _bombCursor.color = Color.clear;
        _beamCollider.enabled = false;
    }

    void Update()
    {
        //�J�[�\���̈ʒu���}�E�X�̈ʒu�ɂ���
        var mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        _cursors.transform.position = worldPos;

        if (!_emptyPower)
        {

            if (_power > 0)
            {

                if (Input.GetMouseButtonDown(0)) //�}�E�X���������Ƃ�
                {
                    //�{���J�[�\�������ăr�[���J�[�\���Ԃɂ���
                    _bombCursor.color = Color.clear;
                    _beamCursor.color = Color.red;
                    _flash = _beamCursor.DOColor(new Color(1, 0.5f, 0), 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
                    //�R���C�_�[�I��
                    _beamCollider.enabled = true;
                    _power--;
                }
                else if (Input.GetMouseButton(0)) //�}�E�X���������ςȂ��̂Ƃ�
                {
                    _power -= Time.deltaTime * 2.0f;
                }
                else if (Input.GetMouseButtonUp(0)) //�}�E�X���������Ƃ�
                {
                    //�r�[���J�[�\���̐F�߂�
                    _beamCursor.color = Color.white;
                    _flash.Kill();
                    //�R���C�_�[�I�t
                    _beamCollider.enabled = false;
                }
                else
                {
                    RecoveryPower();

                    if (Input.GetMouseButtonDown(1)) //�}�E�X�E�������Ƃ�
                    {
                        //�r�[���J�[�\�������ă{���J�[�\���o��
                        _beamCursor.color = Color.clear;
                        _bombCursor.color = new Color(1, 0.1951895f, 0, 0.5882353f);
                    }
                    //else if (Input.GetMouseButton(1)) //�}�E�X�E�������ςȂ��̂Ƃ�
                    //{

                    //}
                    else if (Input.GetMouseButtonUp(1)) //�}�E�X�E�������Ƃ�
                    {

                        if (_power >= _bombPower)
                        {
                            //�M�ʂ�����Ă鎞
                            Instantiate(_bomber, worldPos, Quaternion.identity);
                            _power -= _bombPower;
                        }
                        else
                        {
                            //�M�ʂ�����Ȃ������Ƃ�
                        }

                        //�{���J�[�\�������ăr�[���J�[�\���o��
                        _bombCursor.color = Color.clear;
                        _beamCursor.color = Color.white;
                    }

                }

            }
            else //�M�ʂ��Ȃ��Ȃ�����
            {
                //�r�[���J�[�\���̐F�߂�
                _beamCursor.color = Color.white;
                _flash.Kill();
                //�R���C�_�[�I�t
                _beamCollider.enabled = false;
                //�Ȃ��Ȃ�����I��
                _emptyPower = true;
            }

        }
        else
        {
            RecoveryPower();

            //���܂ŉ񕜂�����I�t
            if(_power > _maxPower)
            {
                _emptyPower = false;
            }

        }

        //�ő�l�𒴂��Ă���ő�l�ɏ���������
        if(_power > _maxPower)
        {
            _power = _maxPower;
        }

    }

    /// <summary>�M�ʂ��񕜂���</summary>
    private void RecoveryPower()
    {
        _power += Time.deltaTime * 3.5f;
    }
}
