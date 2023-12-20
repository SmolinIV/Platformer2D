using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_player.IsWin || _player.IsDied)
            return;

        int rightDirectionDegree = 0;
        int leftDirectionDegree = 180;

        if (Input.GetKey(KeyCode.LeftArrow))
            _player.Run(leftDirectionDegree);
        else if (Input.GetKey(KeyCode.RightArrow))
            _player.Run(rightDirectionDegree);
        else
            _player.Idle();

        if (Input.GetKeyDown(KeyCode.UpArrow))
            _player.Jump();
    }
}
