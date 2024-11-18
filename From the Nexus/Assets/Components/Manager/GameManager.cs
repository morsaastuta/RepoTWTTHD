using Glossary;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform savedPointer;

    bool saved = false;

    #region INPUT ACTION REFERENCES
    [Header("Inputs")]
    [SerializeField] public InputActionReference move;
    [SerializeField] public InputActionReference jump;
    [SerializeField] public InputActionReference interact;
    [SerializeField] public InputActionReference attack;
    [SerializeField] public InputActionReference react;
    [SerializeField] public InputActionReference burst;
    [SerializeField] public InputActionReference pause;
    [SerializeField] public InputActionReference aim;
    #endregion

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnEnable()
    {
        SceneManager.sceneLoaded += LoadPointer;
    }

    protected virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadPointer;
    }

    public void SavePointer(Transform pointer)
    {
        saved = true;
        savedPointer.position = new Vector3(pointer.position.x, pointer.position.y, 0);
    }

    public void LoadPointer(Scene scene, LoadSceneMode mode)
    {
        if (saved)
        {
            Debug.Log("happening at all");
            LevelManager.instance.PointTo(savedPointer, true);
        }
    }

    public void ClearPointer()
    {
        saved = false;
    }
}
