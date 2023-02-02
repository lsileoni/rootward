using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Jint;
using Jint.Native;
using System.Text.RegularExpressions;
using System;

enum CommandCodes : int
{
    CMD_CLEAR,
    CMD_PING,
    CMD_LS,
    CMD_CAT,
    CMD_JSRUN,
    CMD_PWD,
    CMD_CD,
    CMD_MKDIR,
    CMD_ED
}

struct Command
{
    public string cmd;
    public string argument;
}
public class text_repl : MonoBehaviour
{
    public TMP_Text textComponent;
    public TMP_InputField inputField;
    private FileSystem fs;
    private string[] inputs;
    public int rowPos;
    private Engine engine;
    private bool editing;
    private Command cmd;
    private string tmp;
    private JintEvaluator jintEvaluator;
    private string code;
    private JsValue result;
    private Command cur_cmd;
    // Update is called once per frame
    private int command_check(string str)
    {
        if (str.Equals("clear"))
            return ((int)CommandCodes.CMD_CLEAR);
        if (str.Equals("ping"))
            return ((int)CommandCodes.CMD_PING);
        if (str.Equals("pwd"))
            return ((int)CommandCodes.CMD_PWD);
        if (str.Equals("cd"))
            return ((int)CommandCodes.CMD_CD);
        if (str.Equals("mkdir"))
            return ((int)CommandCodes.CMD_MKDIR);
        if (str.Equals("ls"))
            return ((int)CommandCodes.CMD_LS);
        if (str.Equals("cat"))
            return ((int)CommandCodes.CMD_CAT);
        if (str.Equals("javascript"))
            return ((int)CommandCodes.CMD_JSRUN);
        if (str.Equals("ed") || str.Equals("vim") || str.Equals("emacs"))
            return ((int)CommandCodes.CMD_ED);
        return (-1);
    }
    void Start()
    {
        engine = new Engine();
        code = "";
        cur_cmd = new Command();
        result = null;
        editing = false;
        jintEvaluator = new JintEvaluator();
        fs = new FileSystem(Application.persistentDataPath);
        textComponent.color = Color.green;
        inputField.text = "> ";
        inputs = inputField.text.Split('\n');
        rowPos = inputs[inputs.Length - 1].Length;
        inputField.MoveTextEnd(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Plus))
            inputField.pointSize++;
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Minus))
            inputField.pointSize--;
        if (Input.GetKeyDown(KeyCode.Return)) {
            inputs = inputField.text.Split('>');
            tmp = inputs[inputs.Length - 1].Trim();
            tmp = Regex.Replace(tmp, " {2,}", " ");
            inputs = tmp.Split(" ");
            cur_cmd.cmd = inputs[0];
            if (inputs.Length > 1)
                cur_cmd.argument = inputs[1];
            else
                cur_cmd.argument = "";
            switch (command_check(cur_cmd.cmd)) {
                case (int)CommandCodes.CMD_CLEAR:
                    inputField.text = "> ";
                    break;
                case (int)CommandCodes.CMD_PWD:
                    inputField.text += fs.GetCurrentDirectory();
                    inputField.text += "\n> ";
                    break;
                case (int)CommandCodes.CMD_PING:
                    inputField.text += "pong\n> ";
                    break;
                case (int)CommandCodes.CMD_ED:
                    if (cur_cmd.argument == "")
                    {
                        inputField.text += "Empty argument\n> ";
                        break;
                    }
                    else if (fs.fileExists(cur_cmd.argument))
                    {
                        tmp = inputField.text + "\n> ";
                        inputField.text = fs.GetFileContent(cur_cmd.argument);
                    }
                    inputField.text += "pong\n> ";
                    break;
                case (int)CommandCodes.CMD_CD:
                    if (cur_cmd.argument == "")
                    {
                        inputField.text += "Empty argument\n> ";
                        break;
                    }
                    else if (cur_cmd.argument == "..")
                        fs.goBackDir();
                    else
                        fs.goToDir(cur_cmd.argument);
                    inputField.text += "\n> ";
                    break;
                case (int)CommandCodes.CMD_LS:
                    inputField.text += "Files: " + fs.GetFiles() + "\n";
                    inputField.text += "Directories: " + fs.GetDirectories();
                    inputField.text += "\n> ";
                    break;
                case (int)CommandCodes.CMD_CAT:
                    if (cur_cmd.argument == "")
                    {
                        inputField.text += "Empty argument\n> ";
                        break;
                    }
                    if (fs.GetFileContent(cur_cmd.argument) == "")
                    {
                        inputField.text += "File not found!\n> ";
                        break;
                    }
                    inputField.text += fs.GetFileContent(cur_cmd.argument) + "\n";
                    inputField.text += "\n> ";
                    break;
                case (int)CommandCodes.CMD_MKDIR:
                    if (cur_cmd.argument == "")
                    {
                        inputField.text += "Empty argument\n> ";
                        break;
                    }
                    fs.createDirectory(cur_cmd.argument);
                    inputField.text += "\n> ";
                    break;
                case (int)CommandCodes.CMD_JSRUN:
                    if (cur_cmd.argument == "")
                    {
                        inputField.text += "Empty argument\n> ";
                        break;
                    }
                    if (!fs.fileExists(cur_cmd.argument))
                    {
                        inputField.text += "File doesn't exist\n> ";
                        break;
                    }
                    code = fs.GetFileContent(cur_cmd.argument);
                    result = jintEvaluator.Evaluate(code);
                    inputField.text += result.ToString();
                    inputField.text += "\n> ";
                    break;
                default:
                    inputField.text += "Command not found\n> ";
                    break;
            }
            inputField.MoveTextEnd(false);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace)) {
            inputs = inputField.text.Split('\n');
            rowPos = inputs[inputs.Length - 1].Length;
            if (rowPos == 1) {
                inputField.text += " ";
                inputField.MoveTextEnd(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
                inputField.MoveTextEnd(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            string code = "1 + 1";
            JsValue result = jintEvaluator.Evaluate(code);
            Debug.Log(result.ToString());
        }
        textComponent.ForceMeshUpdate();
    }
}