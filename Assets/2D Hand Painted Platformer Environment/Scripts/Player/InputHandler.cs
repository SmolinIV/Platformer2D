using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool IsLeftArrowPressed() => Input.GetKey(KeyCode.LeftArrow);

    public bool IsRightArrowPressed() => Input.GetKey(KeyCode.RightArrow);

    public bool IsUpArrowPressed() => Input.GetKeyDown(KeyCode.UpArrow);
}
