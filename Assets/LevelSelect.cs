using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public TextMeshProUGUI terminal;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }else if (Input.GetKeyDown(KeyCode.Return))
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
                    if(gun)
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
                case "ae":
                    levelSelected = 1;
                    toTerminal("AE mission selected", 1);
                    break;
                case "soni":
                    levelSelected = 2;
                    toTerminal("Soni mission selected", 1);
                    break;
                case "nintenerd":
                    levelSelected = 3;
                    toTerminal("Nintenerd mission selected", 1);
                    break;
                case "bisney":
                    levelSelected = 4;
                    toTerminal("Bisney mission selected", 1);
                    break;
                default:
                    toTerminal("Command not found", 1);
                    break;
            }
            toTerminal("", 2);
            currentCommand = "";

        }
        else if(Input.anyKey)
        {
            terminal.text = terminal.text + Input.inputString;
            currentCommand = currentCommand+Input.inputString;
        }
    }
    void toTerminal(string str, int num = 0)
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
