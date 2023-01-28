using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�v���C���[�̃J�[�\���𓮂�������ς����肷��</summary>
public class PlayerCursor : MonoBehaviour
{
    [SerializeField, Header("�J�[�\���B�̐e�I�u�W�F�N�g")] GameObject _cursors = default;
    [SerializeField, Header("�M�r�[���̃J�[�\��")] SpriteRenderer _beamCursor = default;
    [SerializeField, Header("�{���͈͂̃J�[�\��")] SpriteRenderer _bombCursor = default;
    [SerializeField, Header("�{������")] GameObject _bomber = default;
    Vector3 _worldPos = default;

    void Start()
    {
        //�{���J�[�\�����������Ƃ�
        //_bombCursor.SetActive(false);
        _bombCursor.color = Color.clear;
    }

    void Update()
    {
        //�J�[�\���̈ʒu���}�E�X�̈ʒu�ɂ���
        var mousePos = Input.mousePosition;
        _worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        _cursors.transform.position = _worldPos;

        if(Input.GetMouseButtonDown(0)) //�}�E�X���������Ƃ�
        {
            //�{���J�[�\�������ăr�[���J�[�\���Ԃɂ���
            _bombCursor.color = Color.clear;
            _beamCursor.color = Color.red;
        }
        //else if(Input.GetMouseButton(0)) //�}�E�X���������ςȂ��̂Ƃ�
        //{

        //}
        else if(Input.GetMouseButtonUp(0)) //�}�E�X���������Ƃ�
        {
            //�r�[���J�[�\���̐F�߂�
            _beamCursor.color = Color.white;
        }
        else if(Input.GetMouseButtonDown(1)) //�}�E�X�E�������Ƃ�
        {
            //�r�[���J�[�\�������ă{���J�[�\���o��
            _beamCursor.color = Color.clear;
            _bombCursor.color = Color.white;
        }
        //else if(Input.GetMouseButton(1)) //�}�E�X�E�������ςȂ��̂Ƃ�
        //{

        //}
        else if (Input.GetMouseButtonUp(1)) //�}�E�X�E�������Ƃ�
        {
            //�{���J�[�\�������ăr�[���J�[�\���o��
            _bombCursor.color = Color.clear;
            _beamCursor.color = Color.white;
            Instantiate(_bomber, _worldPos, Quaternion.identity);
        }

    }

}
