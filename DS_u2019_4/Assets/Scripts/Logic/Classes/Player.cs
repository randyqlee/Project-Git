using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // int ID that we get from ID factory
    public int ID;
    // a Character Asset that contains data about this Hero. ONLY IF NEEDED
    public CharacterAsset charAsset;
    // a script with references to all the visual game objects for this player
    public PlayerArea PArea;
}
