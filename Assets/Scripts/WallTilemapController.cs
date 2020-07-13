using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTilemapController : MonoBehaviour, HandlesLevelNext
{
    // References
    public AnimatedTile[] DimensionTiles;
    public ModifierHandler m_ModifierHandler;

    // Private State
    private int DimensionIdx = 0;

    public void OnNextLevel(){
        // Update Dimension Walls
        DimensionIdx++;
        Tilemap m_Tilemap = GetComponent<Tilemap>();
        foreach (var m_Pos in m_Tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int m_LocalPlace = new Vector3Int(m_Pos.x, m_Pos.y, m_Pos.z);
            if (m_Tilemap.HasTile(m_LocalPlace))
            {
                m_Tilemap.SetTile(m_LocalPlace, DimensionTiles[DimensionIdx]);
            }
        }
    }

    public bool IsLastDimension()
    {
        return (DimensionTiles.Length - DimensionIdx == 1);
    }

    public GameObject[] FindGameObjectsWithLayer(int Layer)
    {
        List<GameObject> m_ModifierObjects = new List<GameObject>();
        GameObject[] m_AllGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject gameObject in m_AllGameObjects)
        {
            if (gameObject.layer == Layer) m_ModifierObjects.Add(gameObject);
        }
        if (m_ModifierObjects.Count == 0) return null;
        return m_ModifierObjects.ToArray();
    }
}
