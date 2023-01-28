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

    /// <summary>���N�̃I�u�W�F�N�g</summary>
    GameObject _target;
    /// <summary>�����̃��W�b�g�{�f�B</summary>
    Rigidbody2D _rb;
    /// <summary>�����̃X�v���C�g</summary>
    SpriteRenderer _spriteRenderer;
    /// <summary>�A�j���[�^�[</summary>
    Animator _animator;

    void Start()
    {
        _target = GameObject.Find(_name);
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
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
            _animator.SetBool("Down",true);
        }
    }

    public void Hit()
    {
        _spriteRenderer.sprite = _downSprite;
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        _speed = 0;
        _animator.SetBool("Down", true);
    }
}
