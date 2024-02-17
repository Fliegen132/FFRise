using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewResources : MonoBehaviour, IService
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI rockText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI energyText;

    private ResourcesCounter _resources;
    public void Start()
    {
        _resources = ServiceLocator.Current.Get<ResourcesCounter>();
    }

    public void UpdateText()
    {
        Dictionary<string, int> a = _resources.GetAll();
        goldText.text = $"{a["gold"]}";
        woodText.text = $"{a["wood"]}";
        rockText.text = $"{a["stone"]}";
        energyText.text = $"{a["energy"]}";
    }
}
