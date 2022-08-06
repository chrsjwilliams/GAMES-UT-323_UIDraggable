using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class UIObject : SomeParentObjectClass, IDraggable3D
{
    #region IDraggable3D Interface Implemenation
    ///////////////////////////////////////////////
    ///                                         ///
    /// IDraggable3D Interface Implementation   ///
    ///                                         ///
    ///////////////////////////////////////////////
    Finger IDraggable3D.id
    {
        get => _fingerID;
        set => _fingerID = value;
    }
    bool IDraggable3D.canMove
    {
        get => canMove;
        set => canMove = value;
    }
    Vector3 IDraggable3D.pos
    {
        get => transform.position;
        set => transform.position = value;
    }
    Quaternion IDraggable3D.rot
    {
        get => transform.rotation;
        set => transform.rotation = value;
    }
    Vector3 IDraggable3D.scale
    {
        get => transform.localScale;
        set => transform.localScale = value;
    }

    // needed for IDraggable3D interface to work
    Finger _fingerID = null;
    [SerializeField] bool canMove = true;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
