using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(GlobalVariables.selectButton))
        {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            GlobalVariables.selectedPlayer = null;
            GlobalVariables.isPlayerSelected = false;
            GlobalVariables.isPlayerHovered = false;
            GlobalVariables.isInPlay = false;
            GlobalVariables.fieldMinX = -15;
            GlobalVariables.fieldMaxX = 5;
            GlobalVariables.fieldMinY = 0;
            GlobalVariables.fieldMaxY = 30;
            GlobalVariables.selectButton = KeyCode.Mouse0;
            GlobalVariables.deselectButton = KeyCode.Mouse1;
            GlobalVariables.offensePlayers = new List<Offense>();
            GlobalVariables.CurrentQB = null;
            GlobalVariables.round = 1;
            GlobalVariables.currency = 0;
            GlobalVariables.currentPasser = null;
            GlobalVariables.currentReciever = null;
            GlobalVariables.players = new List<GameObject>();
            GlobalVariables.passComplete = false;
            GlobalVariables.gameOver = false;
            GlobalVariables.markedList = new List<GameObject>();
        }
    }
}
