using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("動く速さ")]
    [SerializeField] float _speed = 5f;
    [Tooltip("俺君のオブジェクト名")]
    [SerializeField] string _name;
    [Tooltip("倒れた時用の絵")]
    [SerializeField] Sprite _downSprite;

    /// <summary>俺君のオブジェクト</summary>
    GameObject _target;
    /// <summary>自分のリジットボディ</summary>
    Rigidbody2D _rb;
    /// <summary>自分のスプライト</summary>
    SpriteRenderer _spriteRenderer;
    /// <summary>アニメーター</summary>
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
