using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionFollow : MonoBehaviour
{
    [SerializeField] private Transform candle;

    [SerializeField, Space(5)] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform down;
    [SerializeField] private Transform up;
    private Direction currentDirection;

    private Transform currentTarget;
    [SerializeField]
    private float movementSpeed = 1;

    [SerializeField] private string candleSortingLayerName = "Light";

    [SerializeField] private SpriteRenderer playerBody;
    [SerializeField] private SpriteRenderer candleBase;
    [SerializeField] private SpriteRenderer candleLight;


    private void Awake()
    {
        PlayerController.currentDirection += Direction;
    }

    void Direction(Direction direction)
    {
        currentDirection = direction;

        switch (direction)
        {
            case global::Direction.Center:
                currentTarget = right;

                break;
            case global::Direction.Right:
                currentTarget = right;
                ;
                break;
            case global::Direction.Left:
                currentTarget = left;

                break;
            case global::Direction.Upward:
                currentTarget = up;

                break;
            case global::Direction.Downward:
                currentTarget = down;

                break;

        }
        StartCoroutine(MoveToWards());
    }

    IEnumerator MoveToWards()
    {
        if (currentDirection != global::Direction.Upward) { SetBehind(false); }
        while (Vector3.Distance(candle.position, currentTarget.position) > 0.01f)
        {
            candle.position = Vector3.MoveTowards(candle.position, currentTarget.position, movementSpeed * Time.deltaTime);
            yield return null;
        }
        if (currentDirection == global::Direction.Upward) { SetBehind(true); }
    }

    void SetBehind(bool status)
    {
        if (candleBase)
            candleBase.sortingLayerName = status ? playerBody.sortingLayerName : candleSortingLayerName;
        if (candleLight)
            candleLight.sortingLayerName = status ? playerBody.sortingLayerName : candleSortingLayerName;
    }


    private void OnDisable()
    {
        PlayerController.currentDirection -= Direction;
    }



}

