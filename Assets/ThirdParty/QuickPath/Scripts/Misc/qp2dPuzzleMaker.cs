using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class qp2dPuzzleMaker : MonoBehaviour
{
    public qpFollowMouseObject player;
    public qpGrid grid;
    public Font font;
    public Material fontmaterial;
    public Vector3 startBounds;
    public Vector3 endBounds;
    public TextMesh text;
    private List<GameObject> _collectables = new List<GameObject>();
    private List<GameObject> _walls = new List<GameObject>();
    private GameObject[,] _puzzleGrid;
    private float _collectableRow = 8.5f;
    private int _completed = 0;
    private bool _rebuilding = false;
    // Use this for initialization
    void Start()
    {
        player.renderer.material.color = new Color(0, 0, 1);
        _createPuzzle();
    }
    private void _createPuzzle()
    {
        text.text = "";
        _puzzleGrid = new GameObject[10, 10];
        _makeWallRow(1);
        _makeWallRow(3);
        _makeWallRow(5);
        _makeWallRow(7);
        grid.Bake();
        player.FindClosestNode();

        Vector2 vec = new Vector3(UnityEngine.Random.Range(0, 8.5f), _collectableRow);
        _collectableRow = (_collectableRow == 0 ? 8.5f : 0);
        _makeCollectableAt(vec);
        int[] ys = new int[] { 2, 4, 6 };
        for (int i = _completed; i > 0; i--)
        {
            int x = UnityEngine.Random.Range(0, 10);
            int y = ys[UnityEngine.Random.Range(0, ys.Length)];
            _makeCollectableAt(new Vector2(x, y));
        }
        _rebuilding = false;
    }
    private void _clearPuzzle()
    {
        _completed++;
        text.text = "Level " + _completed + " Complete";
        for (int i = _walls.Count; i > 0; i--)
        {
            GameObject.Destroy(_walls[i - 1]);
            _walls.RemoveAt(i - 1);
        }
    }
    private void _makeWallRow(int y)
    {
        List<int> holeSlots = new List<int>();
        for (int i = UnityEngine.Random.Range((int)1, (int)Mathf.Floor(_completed) + 1); i > 0; i--) holeSlots.Add(UnityEngine.Random.Range(0, 10));
        for (int i = 0; i < 10; i++)
        {
            if (!holeSlots.Contains(i))
            {
                _puzzleGrid[i, y] = _makeWallAt(new Vector2(i, y));
            }
        }
    }
    private void _makeEnemyRow(int y)
    {

    }
    private GameObject _makeCollectableAt(Vector2 vec2)
    {
        Vector3 vec = new Vector3(vec2.x, vec2.y, 0);
        GameObject collectable = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        collectable.AddComponent<Rigidbody>();
        collectable.GetComponent<Rigidbody>().isKinematic = true;
        collectable.renderer.material.color = new Color(0f, 1f, 0);
        collectable.transform.position = vec;
        collectable.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        _collectables.Add(collectable);
        return collectable;
    }
    private GameObject _makeWallAt(Vector2 vec2)
    {
        Vector3 vec = new Vector3(vec2.x, vec2.y, 0);
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.AddComponent<Rigidbody>();
        wall.tag = "Wall";
        wall.renderer.material.color = new Color(1, 0, 0);
        wall.GetComponent<Rigidbody>().isKinematic = true;
        wall.transform.position = vec;
        wall.transform.localScale = new Vector3(1.02f, .5f, .5f);
        _walls.Add(wall);
        wall.transform.parent = this.transform;
        return wall;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = _collectables.Count; i > 0; i--)
        {
            if (_collectables[i - 1].renderer.bounds.Intersects(player.renderer.bounds))
            {
                GameObject.Destroy(_collectables[i - 1]);
                _collectables.RemoveAt(i - 1);
            }
        }
        if (_collectables.Count == 0 && !_rebuilding)
        {
            _clearPuzzle();
            _rebuilding = true;
            _setTimeout(_createPuzzle, 1f);
        }
    }
    private void _setTimeout(Action method, float delay)
    {
        StartCoroutine(ienum(method, delay));
    }
    IEnumerator ienum(Action method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method();
    }
}
