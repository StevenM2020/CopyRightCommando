//Script:       Level Select Script
//Author:       Steven Motz
//Date:         10/11/2022
//Purpose:      This script controls the terminal and allows the player to select levels.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public TextMeshProUGUI terminal;
    public Image missionImage;
    public List<Sprite> missionImages = new List<Sprite>();
    public RawImage terminalRawImage;
    private string currentCommand;
    private Dictionary<string, string> commands = new Dictionary<string, string> { 
        { "HELP", "shows commands" },
        { "START", "starts the game" }, 
        { "AE", "selects the mission AE" },
        { "SONI", "selects the mission Soni" },
        { "NINTENERD", "selects the mission Nintenerd" },
        { "BISNEY", "selects the mission Bisney" },
        { "ARMORY HINT", "might help you get into the armory" } };
    private bool gun = false;
    private int levelSelected = 0;
    private bool terminalOn = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (terminalOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // close terminal
            {
                terminalRawImage.enabled = false;
                player.SetActive(true);
                terminalOn = false;
                
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                terminal.text = terminal.text + "\n";
                Debug.Log(currentCommand);
                switch (currentCommand.ToLower().Trim())
                {
                    case "help":
                        toTerminal(" List of available commands:", 1);
                        foreach (KeyValuePair<string, string> command in commands)
                        {
                            toTerminal(command.Key + ": " + command.Value);
                        }

                        break;
                    case "start":
                        if (GameObject.Find("WeaponStarManager").GetComponent<WeaponStarManager>().GetNumGunsSelected() > 0) // checks num guns
                        {
                            switch (levelSelected)
                            {

                                case 1:
                                    // sent to AE
                                    break;
                                case 2:
                                    // sent to SONI
                                    break;
                                case 3:
                                    // sent to NINTENERD
                                    break;
                                case 4:
                                    // sent to BISNEY
                                    break;
                                default:
                                    toTerminal("No mission selected", 1);
                                    break;
                            }
                        }
                        else
                        {
                            toTerminal("You need to select a weapon from the armory", 1);
                        }


                        break;

                    // select levels
                    case "ae":
                        SelectLevel(1);
                        toTerminal("AE mission selected", 1);
                        break;
                    case "soni":
                        SelectLevel(2);
                        toTerminal("Soni mission selected", 1);
                        break;
                    case "nintenerd":
                        SelectLevel(3);
                        toTerminal("Nintenerd mission selected", 1);
                        break;
                    case "bisney":
                        SelectLevel(4);
                        toTerminal("Bisney mission selected", 1);
                        break;
                    case "armory hint":
                        toTerminal("Use the key pad at the end of the hall", 1);
                        break;
                    default:
                        toTerminal("Command not found", 1);
                        break;
                }
                toTerminal("", 2);
                currentCommand = "";
            }else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if(currentCommand.Length > 0)
                {
                    currentCommand = currentCommand.Substring(0, currentCommand.Length - 1);
                    terminal.text = terminal.text.Substring(0, terminal.text.Length - 1);
                }
            }
            else if (Input.anyKey) // writes any keys to the terminal
            {
                terminal.text = terminal.text + Input.inputString;
                currentCommand = currentCommand + Input.inputString;
            }
        }
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape)) // close terminal
        //    {
        //        terminalRawImage.enabled = true;
        //        terminalOn = true;
        //    }
        //}
    }
    public void OpenTerminal()
    {
        terminalRawImage.enabled = true;
        Invoke("TurnOnTerminal", .2f);
        player.SetActive(false);
    }
    void TurnOnTerminal()
    {
        terminalOn = true;
    }
    void SelectLevel(int num) // changes image and int
    {
        levelSelected = num;
        missionImage.gameObject.SetActive(true);
        missionImage.sprite = missionImages[levelSelected - 1];
    }
    void toTerminal(string str, int num = 0) // writes to the termainal
    {
        switch (num)
        {
            case 0:
                terminal.text = terminal.text + str;
                terminal.text = terminal.text + "\n";
                break;
            case 1:
                terminal.text = terminal.text +"Computer: " + str;
                terminal.text = terminal.text + "\n";
                break;
            case 2:
                terminal.text = terminal.text +"Agent: " + str;
                break;
        }
        
    }
}
