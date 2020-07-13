using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class ModifierHandler : MonoBehaviour, HandlesLevelNext
{
    // References
    public GameObject g_ModifierTilemapObject;

    // Tweak options
    public ModifierImport[] g_ModifierImports;
    public float g_ModifierObjectClickColliderRadius;

    // Internal State
    public GameObject[] g_ModifierTriplets;

    private void Awake()
    {
      Tilemap g_ModifierTilemap = g_ModifierTilemapObject.GetComponent<Tilemap>();
      string[] g_ModifierSpriteNames = g_ModifierImports.Select(x => x.sprite.name).ToArray();
      List<GameObject> g_TempModifierTriplets = new List<GameObject>();

      foreach (var g_Pos in g_ModifierTilemap.cellBounds.allPositionsWithin)
      {
          Vector3Int g_LocalPlace = new Vector3Int(g_Pos.x, g_Pos.y, g_Pos.z);
          Vector3 g_Place = g_ModifierTilemap.CellToWorld(g_LocalPlace);
          if (g_ModifierTilemap.HasTile(g_LocalPlace))
          {
              int g_ModifierIdx = System.Array.IndexOf(g_ModifierSpriteNames, g_ModifierTilemap.GetSprite(g_LocalPlace).name);
              if (g_ModifierIdx >= 0)
              {
                ModifierImport g_ModifierImport = g_ModifierImports[g_ModifierIdx];

                // make and configure the modifier triplet parent
                GameObject g_ModifierTripletObject = new GameObject("ModifierTriplet");
                g_ModifierTripletObject.tag = g_ModifierImport.tag;
                g_ModifierTripletObject.layer = g_ModifierImport.layer;
                ModifierTripletController g_ModifierTripletController = g_ModifierTripletObject.AddComponent(typeof(ModifierTripletController)) as ModifierTripletController;
                g_ModifierTripletController.m_ModifierImports = g_ModifierImports;

                // make and configure a child click object
                GameObject g_ModifierClickObject = new GameObject("ClickTarget");
                g_ModifierClickObject.tag = "ClickTarget";
                g_ModifierClickObject.transform.parent = g_ModifierTripletObject.transform;
                g_ModifierTripletController.m_ModifierClickObject = g_ModifierClickObject;
                if(g_ModifierIdx < 3)
                {
                  CircleCollider2D g_ModifierObjectClickCollider = g_ModifierClickObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
                  ModifierClickObjectController g_ModifierClickObjectController = g_ModifierClickObject.AddComponent(typeof(ModifierClickObjectController)) as ModifierClickObjectController;
                  g_ModifierClickObjectController.m_ModifierTripletController = g_ModifierTripletController;
                  g_ModifierObjectClickCollider.isTrigger = true;
                  g_ModifierObjectClickCollider.radius = g_ModifierObjectClickColliderRadius;
                }

                // make and configure a child sprite object
                GameObject g_ModifierSpriteObject = new GameObject("GFX");
                g_ModifierSpriteObject.tag = g_ModifierImport.tag;
                g_ModifierSpriteObject.transform.parent = g_ModifierTripletObject.transform;
                g_ModifierTripletController.m_ModifierSpriteObject = g_ModifierSpriteObject;
                CircleCollider2D g_ModifierSpriteObjectCollider = g_ModifierSpriteObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
                g_ModifierSpriteObjectCollider.isTrigger = true;
                g_ModifierSpriteObjectCollider.radius = g_ModifierImport.colliderRadius;
                SpriteRenderer g_Renderer = g_ModifierSpriteObject.AddComponent<SpriteRenderer>();
                g_Renderer.sprite = g_ModifierImport.sprite;

                // Move the parent object to where the tile was
                g_ModifierTripletObject.transform.position = g_Place;

                // Add the triplet parent to the temp list
                g_TempModifierTriplets.Add(g_ModifierTripletObject);

                // Remove the tile that all this was generated from.
                g_ModifierTilemap.SetTile(g_LocalPlace, null);
              }
          }
      }
      // Put the temp list into the state Array
      g_ModifierTriplets = g_TempModifierTriplets.ToArray();
    }

    public void OnNextLevel()
    {
        //Debug.Log("Got here.");
        foreach(GameObject g_ModifierTripletObject in g_ModifierTriplets)
        {
            if(g_ModifierTripletObject.tag != "StartPortal" && g_ModifierTripletObject.tag != "ExitPortal")
            {
                ExecuteEvents.Execute<HandlesLevelNext>(g_ModifierTripletObject, null, (x,y)=>x.OnNextLevel());
            }
        }
    }

    public void OnRunEnter()
    {
        foreach(GameObject g_ModifierTripletObject in g_ModifierTriplets)
        {
            ExecuteEvents.Execute<Runs>(g_ModifierTripletObject, null, (x,y)=>x.OnRunEnter());
        }
    }
}
