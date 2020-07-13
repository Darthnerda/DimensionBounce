// ﻿using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.Tilemaps;
//
// public class ModifierGenerator : MonoBehaviour
// {
//
//     [SerializeField] private Modifier[] m_Modifiers;
//     [SerializeField] private float m_ClickColliderRadius;
//     private string[] m_ModifierSpriteNames;
//
//     // Start is called before the first frame update
//     private void Awake()
//     {
//
//       m_ModifierSpriteNames = m_Modifiers.Select(x => x.sprite.name).ToArray();
//       Tilemap m_Tilemap = GetComponent<Tilemap>();
//
//       foreach (var m_Pos in m_Tilemap.cellBounds.allPositionsWithin)
//       {
//           Vector3Int m_LocalPlace = new Vector3Int(m_Pos.x, m_Pos.y, m_Pos.z);
//           Vector3 m_Place = m_Tilemap.CellToWorld(m_LocalPlace);
//           if (m_Tilemap.HasTile(m_LocalPlace))
//           {
//               int m_ModifierIdx = System.Array.IndexOf(m_ModifierSpriteNames, m_Tilemap.GetSprite(m_LocalPlace).name);
//               //Debug.Log("Attempted this shit. Idx: " + m_ModifierIdx + " " + m_Tilemap.GetSprite(m_LocalPlace).name);
//               if (m_ModifierIdx >= 0)
//               {
//                 string m_Tag = m_Modifiers[m_ModifierIdx].tag;
//
//                 GameObject m_ModifierObject = new GameObject(m_Tag + "GFX");
//
//                 GameObject m_ModifierClickObject = new GameObject(m_Tag + "ClickTarget");
//                 m_ModifierClickObject.transform.parent = m_ModifierObject.transform;
//                 CircleCollider2D m_ModifierObjectClickCollider = m_ModifierClickObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
//                 m_ModifierObjectClickCollider.isTrigger = true;
//                 m_ModifierObjectClickCollider.radius = m_ClickColliderRadius;
//                 m_ModifierClickObject.AddComponent(typeof(ModifierClicked));
//
//                 CircleCollider2D m_ModifierObjectCollider = m_ModifierObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
//                 m_ModifierObjectCollider.isTrigger = true;
//                 m_ModifierObjectCollider.radius = m_Modifiers[m_ModifierIdx].colliderRadius;
//
//                 m_ModifierObject.transform.position = m_Place;
//                 m_ModifierObject.tag = m_Tag;
//                 m_ModifierObject.layer = 9;
//
//                 SpriteRenderer m_Renderer = m_ModifierObject.AddComponent<SpriteRenderer>();
//                 m_Renderer.sprite = m_Modifiers[m_ModifierIdx].sprite;
//
//                 m_Tilemap.SetTile(m_LocalPlace, null);
//               }
//           }
//       }
//     }
//
// }
