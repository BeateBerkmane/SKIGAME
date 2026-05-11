using UnityEngine;

public class SlalomFlag : MonoBehaviour
{
    private enum Direction { Left, Right };
    [SerializeField] private Direction flagDirection;
    private bool flagPassed = false;
    [SerializeField] private Material goodMat, badMat;
    public static event
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerControlle.playerPos != null &&
            PlayerControlle.playerPos.posiotion.z < transform.position.z && 
            !flagPassed)
        {
            flagPassed = true;
            Direction passingDirection = Direction.Right;
            if (PlayerControlle.playerPos.posiotion.x < transform.position.x)
                passingDirection = Direction.Left;
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>(); 
            if (passingDirection == flagDirection)
            {
                rendered.material = goodMat;
            }
            else
            {
                rendered.material = badMat;
                RacePenalty.Invoke()
            }
            
        }
    }
}
