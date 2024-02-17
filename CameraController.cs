using InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IService
{
    private bool isDragging;
    private Vector3 dragPosition;
    [SerializeField] private float speed;

    [SerializeField] private SpriteRenderer mapSpriteRenderer;
    private InputSystem _inputSystem;
    private Bounds mapBounds;

    [SerializeField] private float MouseHoldTime;

    private ViewBuyUpgrade _viewBuyUpgrade;

    private bool _gameWait = false;
    private EventSystem eventSystem;
    
    private void Start()
    {
        Debug.Log(Screen.width);
        _viewBuyUpgrade = ServiceLocator.Current.Get<ViewBuyUpgrade>();
        _inputSystem = ServiceLocator.Current.Get<InputSystem>();
        SetBounds();
        eventSystem = EventSystem.current;
        speed = Camera.main.orthographicSize;
    }
    private void LateUpdate()
    {
        if(_gameWait || ViewInventory._open)
            return;
        
        ChangeCameraSize();
        DragCamera();
        CameraBounds();
    }
    private void ChangeCameraSize()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        mw *= -1;
        Camera.main.orthographicSize += mw * 4;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 8, 20);
        speed = Camera.main.orthographicSize;
    }
    private void SetBounds()
    {
        mapBounds = mapSpriteRenderer.bounds;
    }
    private void CameraBounds()
    {
        float orthographicHeight = Camera.main.orthographicSize;
        float orthographicWidth = orthographicHeight * Camera.main.aspect;

        float leftLimit = mapBounds.min.x + orthographicWidth;
        float rightLimit = mapBounds.max.x - orthographicWidth;
        float upLimit = mapBounds.max.y - orthographicHeight;
        float downLimit = mapBounds.min.y + orthographicHeight;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, downLimit, upLimit),
            transform.position.z
        );
    }

    private void DragCamera()
    {
        if(eventSystem.IsPointerOverGameObject())
            return;
        if (Input.GetMouseButtonDown(0))
        {
            dragPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (MouseHoldTime < 0.1f)
                _inputSystem.ClickInput();
            else
            {
                _viewBuyUpgrade.SetAnimation("Close");
            }
            MouseHoldTime = 0;
            isDragging = false;
        }
        if (isDragging)
        {
            MouseHoldTime += Time.deltaTime;
            if(MouseHoldTime > 0.3)
                _viewBuyUpgrade.SetAnimation("Close");

            Vector3 currentPosition = Input.mousePosition;
            Vector3 difference = dragPosition - currentPosition;

            float moveX = difference.x;
            float moveY = difference.y;

            transform.Translate(new Vector3(moveX, moveY, 0) * speed * Time.deltaTime);

            dragPosition = currentPosition;
        }
    }

    public void EnableGameAwait()
    {
        _gameWait = true;
    }
}