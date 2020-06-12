using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="SpriteDB")]
public class customerRandomDataBase : ScriptableObject
{
    public List<Sprite> spritesDB;//Contains the list of all sprites that the AI can choose from
}
