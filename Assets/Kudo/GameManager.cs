using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>��������</summary>
    [SerializeField, Header("��������"), Tooltip("��������")] float _timeLimit;
    /// <summary>Game���̃^�C�}�[</summary>
    float _timer;
    /// <summary>�Q�[���̏��</summary>
    GameState _state = GameState.Game;

    [SerializeField]SpawnManager _spawnManager;

    [SerializeField, Header("�N���A���̃V�[����")] string _clearSceneName;
    [SerializeField, Header("�Q�[���I�[�o�[���̃V�[����")] string _unclearSceneName;

    public GameState State { get { return _state; } set { _state = value; }}

    [SerializeField, Header("TimerTextUI")] Text _timerText;
    [SerializeField, Header("�X�R�ATextUI")] Text _scoreText;

    int _score = 0;

    public float TimeLimit { get { return _timeLimit; } set { _timeLimit = value; } }
    // Start is called before the first frame update
    void Start()
    {
        _timer = _timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        _timerText.text = _timer.ToString("0");
        _scoreText.text = _spawnManager.Count().ToString() + "�l";

        //�^�C����0�ɂȂ�����Q�[���N���A�̏�Ԃɂ���
        if (_timer <= 0)
        {
            _state = GameState.Clear;
        }

        //�Q�[��������Ȃ�������
        if (_state != GameState.Game)
        {
            switch (_state)
            {
                case GameState.Clear:
                    SceneChange(_clearSceneName);
                    break;
                case GameState.UnClear:
                    SceneChange(_unclearSceneName);
                    break;
            }
        }
    }
    /// <summary>�Q�[���̏�ԊǗ�������enum</summary>
    public enum GameState
    {
        Game,
        /// <summary>�Q�[���N���A</summary>
        Clear,
        /// <summary>�Q�[���I�[�o�[</summary>
        UnClear,
    }

    /// <summary>�V�[���J�ڊ֐�</summary>
    /// <param name="sceneName">�J�ڂ��������V�[����</param>
    void SceneChange(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
