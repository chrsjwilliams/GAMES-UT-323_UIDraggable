using UnityEngine;

[DefaultExecutionOrder(10)]
public class UIDraggerTestScript : MonoBehaviour
{
    [SerializeField] bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        InputManager.Instance.EnableDraggableUI(InputManager.Instance.draggableUI, canMove);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canMove = !canMove;
            InputManager.Instance.EnableDraggableUI(InputManager.Instance.draggableUI, canMove);
        }
    }
}
