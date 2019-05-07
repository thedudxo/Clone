using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager {

    //please use this instead of assigning player scripts in the editor
    //that causes problems with prefabs

    public static Player_Pickup player_Pickup; // ive only assigned this one in start() so far
    public static Player_Clone player_Clone;
    public static PlayerController playerController;
    public static CamMouseLook camMouseLook;
}
