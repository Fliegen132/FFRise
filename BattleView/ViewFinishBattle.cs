using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewFinishBattle : MonoBehaviour, IService
{
    [SerializeField] private Button _btn;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _finishWindow;

    public void SetWin()
    {
        _text.text = "Победа!";
        CreateWindow();
    }

    public void SetLost()
    {
        _text.text = "Поражение!";
        CreateWindow();
    }

    private void CreateWindow()
    {
        SetListenerForBtn(ExitBattle);
        _finishWindow.SetActive(true);
    }

    public void SetListenerForBtn(Action action)
    {
        _btn.onClick.AddListener(() =>
        {
            action?.Invoke();
        });
    }

    private void ExitBattle()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
