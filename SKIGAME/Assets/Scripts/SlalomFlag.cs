using UnityEngine;
using System;

public class SlalomFlag : MonoBehaviour
{
    private enum Direction { Left, Right };
    [SerializeField] private Direction flagDirection; 
    [SerializeField] private Material goodMat, badMat;
    private bool flagPassed = false;
    public static event Action RacePenalty;

        void Update()
    {

        if (PlayerController.playerPos != null &&
            PlayerController.playerPos.position.z < transform.position.z &&
            !flagPassed)
        {
            flagPassed = true;
            Direction passingDirection = Direction.Right;
            if (PlayerController.playerPos.position.x < transform.position.x)
                passingDirection = Direction.Left;
            MeshRenderer rendered = GetComponent<MeshRenderer>();
            if (passingDirection == flagDirection)
            {
                rendered.material = goodMat;
            }
            else
            {
                rendered.material = badMat;
                RacePenalty?.Invoke();
            }

        }
    }
}

