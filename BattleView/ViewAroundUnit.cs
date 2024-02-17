using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewAroundUnit : MonoBehaviour, IService
{
    [SerializeField] private Image _informationWindow;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _damageText;

    [SerializeField] private Vector3 _offset;
    public void SetInfo(string info)
    {
        var a = info.Split(' ').ToArray();
        _nameText.text = $"{a[0]}";
        _hpText.text = $"Здоровье:{a[1]}/{a[2]}";
        _damageText.text = $"Урон:{a[3]}";
    }

    public void ViewWindow()
    {
        _informationWindow.gameObject.SetActive(true);
        Vector3 windowPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        _informationWindow.rectTransform.position = new Vector3(windowPosition.x * Screen.width, windowPosition.y * Screen.height, 0);
    }

    public void DisableViewWindow()
    {
        _informationWindow.gameObject.SetActive(false);
    }
    
   
}
