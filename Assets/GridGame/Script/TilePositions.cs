using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;


public class TilePositions : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;
    [SerializeField] Tilemap wallMap;
    [SerializeField] private Transform thisPlayer;
    [SerializeField] private Transform targetPlayer;
    private int mapSizeX, mapSizeY;

    public List<Vector2> tilemapPlaces;
    [SerializeField] List<Vector2> wallSpaces;
    [SerializeField]
    private List<Vector2> tilePositions = new List<Vector2>();
    private Vector2[,] tilePositionArray;


    [SerializeField] private Transform positionsPrefab;
    [SerializeField] Node[,] graph;
    [SerializeField] GameObject[,] ReferenceTiles;

    [SerializeField] private float gizmoRadius = 1;


    void Start()
    {
        TileMapDimention();
        GetTilePositions();
        StartCoroutine(nameof(GenerateReferencePositions));


    }

    void TileMapDimention()
    {
        mapSizeX = tileMap.size.x;
        mapSizeY = tileMap.size.y;
        graph = new Node[mapSizeX, mapSizeY];
        tilePositionArray = new Vector2[mapSizeX, mapSizeY];
        ReferenceTiles = new GameObject[mapSizeX, mapSizeY];
    }
    void GetTilePositions()
    {


    }



    IEnumerator GeneratePathFindingGraph()
    {

        yield return null;
        /*        for (int x = 0; x < mapSizeX; x++)
                {
                    for (int y = 0; y < mapSizeY; y++)
                    {
                        //4 way connected map
                        try
                        {
                            if (x > 0)
                                graph[x, y].neighbours.Add(graph[x - 1, y]);

                            if (x < mapSizeX - 1)
                                graph[x, y].neighbours.Add(graph[x + 1, y]);
                            if (y > 0)
                                graph[x, y].neighbours.Add(graph[x, y - 1]);

                            if (y < mapSizeY - 1)
                                graph[x, y].neighbours.Add(graph[x, y + 1]);

                        }
                        catch (System.Exception e)
                        {
                            Debug.Log("Error! " + x + " " + y + " " + e);
                        }
                    }
                }*/

    }



    public void MoveSelectedUnity(int x, int y)
    {
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unVisted = new List<Node>();

        Node source = graph[thisPlayer.GetComponent<UnitBox>().tileX, thisPlayer.GetComponent<UnitBox>().tileY];

        Node target = graph[x, y];

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unVisted.Add(v);
        }
        while (unVisted.Count > 0)
        {
            Node u = null;
            foreach (Node possibleU in unVisted)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }
            if (u == target)
            {
                break;
            }
            unVisted.Remove(u);
            foreach (Node v in u.neighbours)
            {
                float alt = dist[u] + u.DistanceTo(v);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }

        }
    }

















    #region Reference Positions For maping 2 dimentional Array
    IEnumerator GenerateReferencePositions()
    {
        Transform parentPosition = Instantiate(new GameObject()).transform;
        parentPosition.name = "TileReferencePositions";
        yield return null;
        /*        for (int x = 0; x < mapSizeX; x++)
                {
                    for (int y = 0; y < mapSizeY; y++)*/

        for (int y = 0; y < mapSizeY; y++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                Transform tilePosition = Instantiate(positionsPrefab).transform;
                tilePosition.transform.SetParent(parentPosition);
                tilePosition.GetComponent<UnitBox>().SetTileTableNumber(x, y);
                ReferenceTiles[x, y] = tilePosition.gameObject;
                ReferenceTiles[x, y].gameObject.SetActive(false);
                //yield return new WaitForSeconds(0.05f);
            }
        }
        StartCoroutine(GeneratePathFindingGraph());

    }
    void SetTilePositions(int x, int y)
    {


        try
        {
            ReferenceTiles[x, y].gameObject.SetActive(true);
            //ReferenceTiles[x, y].transform.position = new Vector3(tilePositions[counter].x, tilePositions[counter].y, 0);
            ReferenceTiles[x, y].transform.position = new Vector3(tilePositionArray[x, y].x, tilePositionArray[x, y].y, 0);
            Debug.Log(tilePositionArray[x, y]);
        }
        catch (System.Exception e)

        {
            Debug.Log("e: " + e);
        }
    }

    #endregion

    private void OnDrawGizmos()
    {
        /*        Gizmos.color = Color.white;
                for (int i = 0; i < tilemapPlaces.Count; i++)
                {
                    Gizmos.DrawWireSphere(tilemapPlaces[i], gizmoRadius);
                }
                Gizmos.color = Color.red;
                for (int i = 0; i < wallSpaces.Count; i++)
                {
                    Gizmos.DrawWireSphere(wallSpaces[i], gizmoRadius);
                }*/
    }
}
