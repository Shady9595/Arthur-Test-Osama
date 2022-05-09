using UnityEngine;

public class InputController : MonoBehaviour
{
    public float MoveDeltaX => _horizontal;
    public float MoveDeltaY => _vertical;

    private Camera _camera;
    private Ray _ray;
    private RaycastHit _hit;
    private bool _calculateInputs = false;
    private float _lastPosX, _lastPosY, _horizontal, _vertical = 0f;

    private void Start() => _camera = Camera.main;

    private void Update() => TakeInput();

    private void TakeInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            _lastPosX = Input.mousePosition.x;
            _lastPosY = Input.mousePosition.y;
            if(Physics.Raycast(_ray, out _hit, 20f))
            {
                if(_hit.transform.GetComponent<BoxMesh>())
                    _calculateInputs = true;
                else if(_hit.transform.TryGetComponent<IInteract>(out IInteract interact))
                    interact.Interact();
            }//if end
        }//if end
        if(Input.GetMouseButton(0))
        {
            if(!_calculateInputs)
                return;
            _horizontal = Mathf.Lerp(_horizontal, Input.mousePosition.x - _lastPosX, 10f * Time.deltaTime);
            _vertical   = Mathf.Lerp(_vertical  , Input.mousePosition.y - _lastPosY, 10f * Time.deltaTime);
            _lastPosX   = Input.mousePosition.x;
            _lastPosY   = Input.mousePosition.y;
        }//if end
        if(Input.GetMouseButtonUp(0))
        {
            _calculateInputs = false;
            _horizontal = _vertical = 0f;
        }//if end
    }//TakeInput() end

}//class end