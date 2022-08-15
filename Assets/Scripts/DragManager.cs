using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{

    private Vector2 _mousePosition;
    private AbosrbableObject _currentObject;
    private Vector2 _pullForce;

    [SerializeField] private float boostRatio;
    
    // Update is called once per frame
    void Update()
    {
        _mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main!.ScreenPointToRay(_mousePosition);
            var raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (raycastHit2D.collider == null) return;
            if (!raycastHit2D.collider.GetComponent<AbosrbableObject>().CanMove) return;
            
            _currentObject = raycastHit2D.collider.GetComponent<AbosrbableObject>();
        }
        
        if (Input.GetMouseButton(0) && _currentObject != null)
            _pullForce = Camera.main!.ScreenToWorldPoint(_mousePosition) - _currentObject.transform.position;

        if (!Input.GetMouseButtonUp(0) || _currentObject == null) return;
        
        var theta = Mathf.Atan2(_pullForce.y, _pullForce.x);
        _currentObject.Theta = theta;
        _currentObject.CanMove = false;
        _currentObject.Speed += _currentObject.Speed * boostRatio;
        _currentObject = null;

    }
}
