using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Jint;
using Jint.Native;
using System.Text.RegularExpressions;

enum CommandCodes : int
{
    CMD_CLEAR,
    CMD_PING,
    CMD_LS,
    CMD_CAT,
    CMD_CATFOR,
    CMD_JSRUN,
    CMD_FORRUN,
    CMD_PWD,
    CMD_CD
}

struct Command
{
    string cmd;
    string argument;
}
public class text_repl : MonoBehaviour
{
    public TMP_Text textComponent;
    public TMP_InputField inputField;
    private FileSystem fs;
    private string[] inputs;
    public int rowPos;
    private Engine engine;
    private Command cmd;
    private string tmp;
    private JintEvaluator jintEvaluator;
    private string code;
    private JsValue result;
    // Update is called once per frame
    private int command_check(string str)
    {
        if (str.Contains("clear"))
            return ((int)CommandCodes.CMD_CLEAR);
        if (str.Contains("ping"))
            return ((int)CommandCodes.CMD_PING);
        if (str.Contains("pwd"))
            return ((int)CommandCodes.CMD_PWD);
        if (str.Contains("cd"))
            return ((int)CommandCodes.CMD_CD);
        if (str.Contains("ls"))
            return ((int)CommandCodes.CMD_LS);
        if (str.Contains("cat for_loop.js"))
            return ((int)CommandCodes.CMD_CATFOR);
        if (str.Contains("cat hello.js"))
            return ((int)CommandCodes.CMD_CAT);
        if (str.Contains("javascript hello.js"))
            return ((int)CommandCodes.CMD_JSRUN);
        if (str.Contains("javascript for_loop.js"))
            return ((int)CommandCodes.CMD_FORRUN);
        return (-1);
    }
    void Start()
    {
        engine = new Engine();
        code = "";
        result = null;
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
        if (Input.GetKeyDown(KeyCode.Return)) {
            inputs = inputField.text.Split('>');
            tmp = inputs[inputs.Length - 1].Trim();
            switch (command_check(inputs[inputs.Length - 1])) {
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
                case (int)CommandCodes.CMD_LS:
                    //inputField.text += fs.GetFiles() + "\n";
                    inputField.text += fs.GetDirectories();
                    break;
                case (int)CommandCodes.CMD_CAT:
                    inputField.text += "log('Hello from JS land!')\n> ";
                    break;
                case (int)CommandCodes.CMD_CATFOR:
                    inputField.text += "var result = '';\nfor (var i = 0; i < 10; i++) {\n\tresult += i + ' ';\n}\nresult;\n> ";
                    break;
                case (int)CommandCodes.CMD_JSRUN:
                    code = "'Hello from JS land!'";
                    result = jintEvaluator.Evaluate(code);
                    inputField.text += result.ToString();
                    inputField.text += "\n> ";
                    break;
                case (int)CommandCodes.CMD_FORRUN:
                    code = "var result = ''; for (var i = 0; i < 10; i++) { result += i + ' '; } result;";
                    result = jintEvaluator.Evaluate(code);
                    inputField.text += result.ToString();
                    inputField.text += "\n> ";
                    break;
                default:
                    inputField.text += "> ";
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