using UnityEngine;

public class ViewException : MonoBehaviour
{
    private GameObject exception;

    private float timeDelay;
    private bool active;
    private void Awake()
    {
        exception = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (active == true)
        {
            exception.SetActive(true);
            timeDelay += Time.deltaTime;
        }
        if (timeDelay >= 1)
        {
            active = false;
            exception.SetActive(false);
            timeDelay = 0;
        }
    }

    public void ViewMessage()
    {
        if (active == false)
        {
            timeDelay = 0;
            active = true;
        }
    }

}
