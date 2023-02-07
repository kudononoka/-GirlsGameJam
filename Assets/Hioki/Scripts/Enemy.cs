using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("��������")]
    [SerializeField] float _speed = 5f;
    [Tooltip("���N�̃I�u�W�F�N�g��")]
    [SerializeField] string _name;
    [Tooltip("�|�ꂽ���p�̊G")]
    [SerializeField] Sprite _downSprite;
    [Tooltip("�X�s�[�h�A�b�v")]
    [SerializeField] float _speedUp;
    [Tooltip("�X�s�[�h�A�b�v���鐔")]
    [SerializeField] int _speedUpCount;

    /// <summary>���N�̃I�u�W�F�N�g</summary>
    GameObject _target;
    /// <summary>�����̃��W�b�g�{�f�B</summary>
    Rigidbody2D _rb;
    /// <summary>�����̃X�v���C�g</summary>
    SpriteRenderer _spriteRenderer;
    /// <summary>�A�j���[�^�[</summary>
    Animator _animator;
    /// <summary>�X�|�[���}�l�[�W���[</summary>
    SpawnManager _spawnM;
    /// <summary>�I�[�f�B�I�\�[�X/Enemy���|��鎞�Ɏg��SE�������Ă��܂�</summary>
    AudioSource _audioSource;


    void Start()
    {
        _target = GameObject.Find(_name);
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _spawnM = GetComponentInParent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnM.Count() >= _speedUpCount)
        {
            _speed = _speed * _speedUp;
            _speedUpCount += 4;
        }//�X�s�[�h���߂Ă�
    }

    void Update()
    {
        Vector2 vector = _target.transform.position - transform.position;
        _rb.velocity = vector.normalized * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == _target)
        {
            //�X�e�[�g���Q�[���I�[�o�[�ɂ���
            GameManager gm = FindObjectOfType<GameManager>();
            gm.State = GameManager.GameState.UnClear;

            //{//�e�X�g�p
            //    _animator.SetBool("Down", true);
            //    _speed = 0;
            //    _spawnM.CountPuls();
            //    _spriteRenderer.sprite = _downSprite;
            //}
        }
    }

    public void Hit()
    {
        //�J�E���g
        _spawnM.CountPuls();
        //�X�v���C�g�ύX
        _spriteRenderer.sprite = _downSprite;
        //�R���C�_�[�I�t
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        //�X�s�[�h0
        _speed = 0;
        //�|��鎞�̉�
        _audioSource.PlayOneShot(_audioSource.clip);
        //�A�j���[�V����
        _animator.SetBool("Down", true);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    
}
