using UnityEngine;


public class CameraController : MonoBehaviour 
{
    private Vector3 _mouseOriginPoint;
    private Vector3 _offset;
    private bool _dragging;


    public float MoveSpeed = 15.0f;

    private void Update()
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") 
            * (Camera.main.orthographicSize), 2.5f, 50f);
        

        if (Input.GetMouseButton(2))
        {
            MoveCamera();
            //_offset = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            //if (!_dragging)
            //{
            //    _dragging = true;
            //    _mouseOriginPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //}
        }
        else
        {
            _dragging = false;
        }
        if (_dragging)
            transform.position = _mouseOriginPoint - _offset;
    }
    private void MoveCamera()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 direction = transform.up * yInput + transform.right * xInput;

        transform.position += direction * MoveSpeed * Time.deltaTime;
    }
}
