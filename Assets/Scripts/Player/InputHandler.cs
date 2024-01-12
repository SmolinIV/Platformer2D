using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool IsLeftArrowPressed() => Input.GetKey(KeyCode.LeftArrow);

    public bool IsRightArrowPressed() => Input.GetKey(KeyCode.RightArrow);

    public bool IsUpArrowPressed() => Input.GetKeyDown(KeyCode.UpArrow);

    public bool IsSpacePressed() => Input.GetKeyDown(KeyCode.Space);

    public bool IsCurrentKeyPressed(KeyCode keyCode) => Input.GetKeyDown(keyCode);
}
