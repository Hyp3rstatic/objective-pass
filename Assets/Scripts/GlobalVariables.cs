using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Global Variables :)
public static class GlobalVariables
{
    public static GameObject selectedPlayer = null;
    public static bool isPlayerSelected = false;
    public static bool isPlayerHovered = false;
    public static bool isInPlay = false;
    public static float fieldMinX = -15;
    public static float fieldMaxX = 5;
    public static float fieldMinY = 0;
    public static float fieldMaxY = 30;
    public static KeyCode selectButton = KeyCode.Mouse0;
    public static KeyCode deselectButton = KeyCode.Mouse1;
    public static List<Offense> offensePlayers = new List<Offense>();
    public static GameObject CurrentQB = null;
    public static int round = 1;
    public static int currency = 0;
    public static Offense currentPasser = null;
    public static Offense currentReciever = null;
    public static List<GameObject> players = new List<GameObject>();
    public static bool passComplete = false;
    public static bool gameOver = false;
    public static List<GameObject> markedList = new List<GameObject>();
}
