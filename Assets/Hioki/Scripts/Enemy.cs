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
    [Tooltip("スピードアップ")]
    [SerializeField] float _speedUp;
    [Tooltip("スピードアップする数")]
    [SerializeField] int _speedUpCount;

    /// <summary>俺君のオブジェクト</summary>
    GameObject _target;
    /// <summary>自分のリジットボディ</summary>
    Rigidbody2D _rb;
    /// <summary>自分のスプライト</summary>
    SpriteRenderer _spriteRenderer;
    /// <summary>アニメーター</summary>
    Animator _animator;
    /// <summary>スポーンマネージャー</summary>
    SpawnManager _spawnM;
    /// <summary>オーディオソース/Enemyが倒れる時に使うSEが入っています</summary>
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
        }//スピード決めてる
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
            //ステートをゲームオーバーにした
            GameManager gm = FindObjectOfType<GameManager>();
            gm.State = GameManager.GameState.UnClear;

            //{//テスト用
            //    _animator.SetBool("Down", true);
            //    _speed = 0;
            //    _spawnM.CountPuls();
            //    _spriteRenderer.sprite = _downSprite;
            //}
        }
    }

    public void Hit()
    {
        //カウント
        _spawnM.CountPuls();
        //スプライト変更
        _spriteRenderer.sprite = _downSprite;
        //コライダーオフ
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        //スピード0
        _speed = 0;
        //倒れる時の音
        _audioSource.PlayOneShot(_audioSource.clip);
        //アニメーション
        _animator.SetBool("Down", true);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    
}
