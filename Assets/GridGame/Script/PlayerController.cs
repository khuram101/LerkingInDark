using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Center = 0, Right = 1, Left = 2, Upward = 3, Downward = 4 };
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] Transform targetPoint;

    [SerializeField] private LayerMask wallMask;
    [SerializeField] private float targetColliderRadius = 1;

    [SerializeField] private Animator playerAnimator;


    [SerializeField, Space(5)] private WallDetector upMovement;
    [SerializeField] private WallDetector downMovement;
    [SerializeField] private WallDetector leftMovement;
    [SerializeField] private WallDetector rightMovement;


    private Direction playerDirection = Direction.Right;

    public delegate void ChangeDirection(Direction direction);
    public static ChangeDirection currentDirection;


    private float delayTime = 0;

    [HideInInspector]
    public float HorizontalMovement = 0;
    [HideInInspector]
    public float VerticlalMovement = 0;
    private UnitBox CurrentTilePosition;

    void Start()
    {
        targetPoint.SetParent(null);
        UpdateDirection();
        CurrentTilePosition = GetComponent<UnitBox>();
    }


    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) <= 0.05f)
        {


            if (delayTime > 0) delayTime -= Time.deltaTime * movementSpeed * 1.3f;



            if (HorizontalMovement != 0 && delayTime <= 0)
            {
                Direction tempDirection = HorizontalMovement == 1 ? Direction.Right : Direction.Left;

                if (tempDirection != playerDirection)
                {
                    HorizontalMovement = 0;
                    playerDirection = tempDirection;
                    UpdateDirection();
                    delayTime = movementSpeed;
                }
                else
                    if ((HorizontalMovement > 0 && rightMovement.IsHitted()) || (HorizontalMovement < 0 && leftMovement.IsHitted()))
                {

                    HorizontalMovement = 0;
                    playerDirection = tempDirection;
                    UpdateDirection();
                    
                }
                else
                {
                    //Movement
                    CurrentTilePosition.tileX += (int)HorizontalMovement;
                }


                targetPoint.position += new Vector3(HorizontalMovement, 0, 0);
            }
            else
            if (VerticlalMovement != 0 && delayTime <= 0)
            {

                Direction tempDirection = VerticlalMovement == 1 ? Direction.Upward : Direction.Downward;

                if (tempDirection != playerDirection)
                {
                    VerticlalMovement = 0;
                    playerDirection = tempDirection;
                    UpdateDirection();
                    delayTime = movementSpeed;
                }
                else
                if ((VerticlalMovement > 0 && upMovement.IsHitted()) || (VerticlalMovement < 0 && downMovement.IsHitted()))
                {
                    VerticlalMovement = 0;
                    playerDirection = tempDirection;
                    UpdateDirection();
                }
                else
                {
                    //Movement
                    CurrentTilePosition.tileY += (int)VerticlalMovement;
                }
                targetPoint.position += new Vector3(0, VerticlalMovement, 0);
            }

            MoveAnimation(false);
        }
        else

        {
            MoveAnimation(true);
        }
    }


    void MoveAnimation(bool status)
    {
        playerAnimator.SetBool("Move", status);
    }


    void UpdateDirection()
    {
        currentDirection?.Invoke(playerDirection);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPoint.position, targetColliderRadius);
    }
}
