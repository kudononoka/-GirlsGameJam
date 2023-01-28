using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>��������</summary>
    [SerializeField, Header("��������"), Tooltip("��������")] float _timeLimit;
    /// <summary>Game���̃^�C�}�[</summary>
    float _timer;
    /// <summary>�Q�[���̏��</summary>
    GameState _state = GameState.Game;

    [SerializeField, Header("�N���A���̃V�[����")] string _clearSceneName;
    [SerializeField, Header("�Q�[���I�[�o�[���̃V�[����")] string _unclearSceneName;

    public GameState State { get { return _state; } set { _state = value; }}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _state = GameState.Clear;
        }

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

    void SceneChange(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
