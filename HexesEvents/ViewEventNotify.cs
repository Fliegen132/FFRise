using TMPro;
using UnityEngine;
namespace HexesEvents
{
    
    public class ViewEventNotify : MonoBehaviour, IService
    {
        [SerializeField] private TextMeshProUGUI _notifyText;
        private float timeDelay;
        private bool active;
        public void ViewMessage(string text)
        {
            _notifyText.text = text;
            active = true;
            timeDelay = 0;
        }
        private void Update()
        {
            if (active == true)
            {
                _notifyText.gameObject.SetActive(true);
                timeDelay += Time.deltaTime;
            }
            if (timeDelay >= 1)
            {
                active = false;
                _notifyText.gameObject.SetActive(false);
                timeDelay = 0;
            }
        }
    }
}