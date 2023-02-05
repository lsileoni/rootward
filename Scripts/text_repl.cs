using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Jint;
using Jint.Native;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

enum e_CommandCodes : int
{
    CMD_CLEAR,
    CMD_PING,
    CMD_LS,
    CMD_CAT,
    CMD_JSRUN,
    CMD_PWD,
    CMD_CD,
    CMD_MKDIR,
    CMD_ED,
    CMD_HELP,
    CMD_NMAP,
    CMD_MAN,
    CMD_SUB,
    CMD_MISS,
    CMD_SSH
}

struct s_Command
{
    public string cmd;
    public string argument;
}

public class s_File
{
    public bool     reading;
    public string   filename;

    public s_File()
    {
        reading = false;
        filename = "";
    }
}
public class text_repl : MonoBehaviour
{
	public Player player;
    public TMP_Text textComponent;
    public TMP_InputField inputField;
    public ScrollRect scrollRect;
    private FileSystem fs;
    private string[] inputs;
    public int rowPos;
    private s_Command cmd;
    private string tmp;
    private JintEvaluator jintEvaluator;
    private string code;
    private string history;
    private JsValue result;
    private s_Command cur_cmd;
    private s_File edited_file;
    private Mission mission;
	private Dictionary<string, Mission> mission_table;

	private bool check_submit(Mission mission)
	{
		if (mission == null)
			return (false);
		if (mission.ip == "42")
		{
			if (mission.jint.Evaluate(fs.GetFileContent("abc.js"), inputField).ToString() == mission.expected)
				return (true);
		}
		return (false);
	}

    private int command_check(string str)
    {
        if (str.Equals("clear"))
            return ((int)e_CommandCodes.CMD_CLEAR);
        if (str.Equals("ping"))
            return ((int)e_CommandCodes.CMD_PING);
        if (str.Equals("pwd"))
            return ((int)e_CommandCodes.CMD_PWD);
        if (str.Equals("cd"))
            return ((int)e_CommandCodes.CMD_CD);
        if (str.Equals("mkdir"))
            return ((int)e_CommandCodes.CMD_MKDIR);
        if (str.Equals("ls"))
            return ((int)e_CommandCodes.CMD_LS);
        if (str.Equals("cat"))
            return ((int)e_CommandCodes.CMD_CAT);
        if (str.Equals("ed") || str.Equals("vim") || str.Equals("emacs"))
            return ((int)e_CommandCodes.CMD_ED);
        if (str.Equals("javascript") || str.Equals("js"))
            return ((int)e_CommandCodes.CMD_JSRUN);
        if (str.Equals("ed") || str.Equals("vim") || str.Equals("emacs"))
            return ((int)e_CommandCodes.CMD_ED);
        if (str.Equals("h") || str.Equals("help"))
            return ((int)e_CommandCodes.CMD_HELP);
        if (str.Equals("mission") || str.Equals("miss"))
            return ((int)e_CommandCodes.CMD_MISS);
        if (str.Equals("m") || str.Equals("man"))
            return ((int)e_CommandCodes.CMD_MAN);
        if (str.Equals("sub") || str.Equals("submit"))
            return ((int)e_CommandCodes.CMD_SUB);
        if (str.Equals("n") || str.Equals("nmap"))
            return ((int)e_CommandCodes.CMD_NMAP);
        if (str.Equals("s") || str.Equals("ssh"))
            return ((int)e_CommandCodes.CMD_SSH);
        return (-1);
    }
    private void print_help()
    {
        inputField.text += "'clear' clear\n";
        inputField.text += "'ping' ping\n";
        inputField.text += "'pwd' print working directory\n";
        inputField.text += "'cd' current directory\n";
        inputField.text += "'mkdir' make directory\n";
        inputField.text += "'ls' list files\n";
        inputField.text += "'cat' list contents\n";
        inputField.text += "'js' run javascripts\n";
        inputField.text += "'ed' 'vim' 'emacs' text editors\n";
        inputField.text += "'n' 'nmap' show list of available network devices\n";
        inputField.text += "'s' 'ssh' connect into a remote machine securely\n";
        inputField.text += "'h' 'help' help\n";
        inputField.text += "'m' 'man' manual pages for commands\n";
        inputField.text += "> ";
    }
    private void man_pages(string argument)
    {
        if (argument == "clear")
            inputField.text += "clears the terminal\nformat: clear [NO ARGUMENTS]\n> ";
        else if (argument == "ping")
            inputField.text += "pongs\nformat: ping [NO ARGUMENTS]\n> ";
        else if (argument == "pwd")
            inputField.text += "prints the current working directory\nformat: pwd [NO ARGUMENTS]\nexample: \n> pwd\n{DIRECTORY_HERE}\n> ";
        else if (argument == "cd")
            inputField.text += "stands for 'change directory'\nformat: cd [DIRECTORY NAME OR ..]\nexample: \n> cd TWO\n{CHANGES DIRECTORY TO TWO}\n> ";
        else if (argument == "mkdir")
            inputField.text += "makes a directory in the current directory\nformat: mkdir [DIRECTORY NAME]\nexample: \n> mkdir TWO\n{MAKES DIRECTORY NAMED TWO}\n> ";
        else if (argument == "ls")
            inputField.text += "lists all files and directories in current working directory\nformat: ls [NO ARGUMENTS]\nexample: \n> ls\nFiles: file.txt secret.txt\nFolders:super_secret_folder\n> ";
        else if (argument == "cat")
            inputField.text += "shows the contents of a file\nformat: cat [FILENAME]\nexample: \n> cat secret_file.txt\nsuper secret content\n> ";
        else if (argument == "js")
            inputField.text += "evaluates a javascript program\nformat: js [FILENAME]\nexample: \n> cat example.js\nconsole.log(1 + 1);\n> js example.js\n2\n> ";
        else if (argument == "ed")
            inputField.text += "standard text editor on the system\nformat: ed [FILENAME]\nexample: \nopen file.txt with ed, which contains 'one'\nedit this text to say 'two' manually with the arrow keys on your keyboard\nonce edited, save the file by pressing your left control key\n> ";
        else if (argument == "nmap")
            inputField.text += "command, which maps immediately accessible network devices\nformat: nmap [NO ARGUMENTS]\n> ";
        else if (argument == "ssh")
            inputField.text += "establishes a secure shell connection from your machine to the host\nformat: ssh [IP ADDRESS]\n> ";
        else
            inputField.text += "command does not have a manual page\n> ";
    }
    void Start()
    {
		player = new Player();
        textComponent.color = Color.green;
        inputField.text = "> ";
        inputField.MoveTextEnd(false);
        code = "";
        tmp = "";
        history = "";
        cur_cmd = new s_Command();
        result = null;
        edited_file = new s_File();
        jintEvaluator = new JintEvaluator(inputField);
        fs = new FileSystem(Application.persistentDataPath);
		mission_table = new Dictionary<string, Mission>();
		mission = null;
        inputs = inputField.text.Split('\n');
        rowPos = inputs[inputs.Length - 1].Length;
        inputField.ActivateInputField();
        inputField.MoveTextEnd(false);
        inputField.caretPosition = 0;
        inputField.stringPosition = 3;
        textComponent.ForceMeshUpdate();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Plus))
            inputField.pointSize++;
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Minus))
            inputField.pointSize--;
        if (Input.GetKeyDown(KeyCode.Return) && !edited_file.reading) {
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
                case (int)e_CommandCodes.CMD_CLEAR:
                    inputField.text = "> ";
                    break;
                case (int)e_CommandCodes.CMD_PWD:
                    inputField.text += fs.GetCurrentDirectory();
                    inputField.text += "\n> ";
                    break;
                case (int)e_CommandCodes.CMD_MAN:
                    man_pages(cur_cmd.argument);
                    break;
                case (int)e_CommandCodes.CMD_PING:
                    inputField.text += "pong\n> ";
                    break;
                case (int)e_CommandCodes.CMD_ED:
                    if (cur_cmd.argument == "")
                        inputField.text += "Empty argument\n> ";
                    else if (fs.fileExists(cur_cmd.argument))
                    {
                        history = inputField.text + "\n> ";
                        inputField.text = fs.GetFileContent(cur_cmd.argument);
                        edited_file.reading = true;
                        edited_file.filename = cur_cmd.argument;
                        //scrollRect.verticalNormalizedPosition = 1;
                        inputField.ActivateInputField();
                        inputField.MoveTextStart(false);
                        inputField.MoveToStartOfLine(false, false);
                        inputField.ProcessEvent(Event.KeyboardEvent("left"));
                        textComponent.ForceMeshUpdate();
                        inputField.DeactivateInputField();
                    }
                    else
                    {
                        history = inputField.text + "\n> ";
                        inputField.text = "";
                        edited_file.reading = true;
                        edited_file.filename = cur_cmd.argument;
                    }
                    break;
                case (int)e_CommandCodes.CMD_CD:
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
                case (int)e_CommandCodes.CMD_LS:
                    inputField.text += "Files: " + fs.GetFiles() + "\n";
                    inputField.text += "Directories: " + fs.GetDirectories();
                    inputField.text += "\n> ";
                    break;
                case (int)e_CommandCodes.CMD_CAT:
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
                    if (fs.GetFileContent(cur_cmd.argument).Length > 2048)
                    {
                        inputField.text += "File too large to be displayed!\n> ";
                        break;
                    }
                    inputField.text += fs.GetFileContent(cur_cmd.argument);
                    inputField.text += "\n> ";
                    break;
                case (int)e_CommandCodes.CMD_MKDIR:
                    if (cur_cmd.argument == "")
                    {
                        inputField.text += "Empty argument\n> ";
                        break;
                    }
                    fs.createDirectory(cur_cmd.argument);
                    inputField.text += "\n> ";
                    break;
                case (int)e_CommandCodes.CMD_JSRUN:
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
                    result = jintEvaluator.Evaluate(code, inputField);
                    if (result != null)
                    {
                        if (!(result == "null"))
                            inputField.text += result;
                    }
                    inputField.text += "\n> ";
                    break;
                case (int)e_CommandCodes.CMD_HELP:
                    print_help();
                    break;
                case (int)e_CommandCodes.CMD_MISS:
					if (!mission_table.ContainsKey(fs.GetCurrentDirectory()))
		                inputField.text += "no mission for this machine";
					else
					{
						mission = mission_table[fs.GetCurrentDirectory()];
						if (mission.completed)
							inputField.text += "mission requirements fulfilled\n";
						else
			                inputField.text += mission.statement;
					}
					inputField.text += "\n> ";
                    break;
                case (int)e_CommandCodes.CMD_SUB:
					if (!mission_table.ContainsKey(fs.GetCurrentDirectory()))
					{
		                inputField.text += "no mission for this machine\n> ";
						break;
					}
					else
						mission = mission_table[fs.GetCurrentDirectory()];

					if (check_submit(mission))
					{
						mission.completed = true;
						inputField.text += "answer: OK :D\nreward: " + mission.reward;
					}
					else
					{
						inputField.text += "answer: KO :(" + mission.statement;
					}
					inputField.text += "\n> ";
                    break;
                case (int)e_CommandCodes.CMD_NMAP:
                    string current_directory = fs.GetCurrentDirectory();
                    string[] root_directories = fs.GetRootDirectories().Split("\n");
                    if (current_directory.Contains("127.0.0.1"))
                    {
                        foreach ( string line in root_directories )
                        {
                            if (!line.Contains("192.168.1.2") && !line.Contains("192.168.1.3") && !line.Contains("192.168.1.4"))
                                inputField.text += line.Trim() + "\n";
                        }
                    }
                    else if (current_directory.Contains("192.168.1.1"))
                    {
                        foreach ( string line in root_directories )
                        {
                            if (!line.Contains("93.1.183.174")  &&
                                !line.Contains("248.185.51.148") &&
                                !line.Contains("136.13.38.91") &&
                                !line.Contains("228.109.159.41"))
                                inputField.text += line.Trim() + "\n";
                        }
                    }
                    else if (current_directory.Contains("192.168.1") && !current_directory.Contains("192.168.1.1"))
                    {
                        foreach ( string line in root_directories )
                        {
                            if (line.Contains("192.168.1.1"))
                                inputField.text += line.Trim() + "\n";
                        }
                    }
                    else
                    {
                        foreach ( string line in root_directories )
                        {
                            if (line.Contains("127.0.0.1"))
                                inputField.text += line.Trim() + "\n";
                        }
                    }
                    inputField.text += "\n> ";
                    break;
                case (int)e_CommandCodes.CMD_SSH:
                    if (cur_cmd.argument == "")
                    {
                        inputField.text += "Not enough arguments!\n> ";
                        break;
                    }
                    tmp = fs.GoToRelativeRootFilepath(cur_cmd.argument);
                    if (tmp == "success")
                        inputField.text += "Succesfully SSH'd into ip address " + cur_cmd.argument;
                    else
                        inputField.text += "Was not able to SSH into ip address " + cur_cmd.argument;
                    inputField.text += "\n> ";
                    break;
                default:
                    inputField.text += "Command not found 'h' for help\n> ";
                    break;
            }
            inputField.MoveTextEnd(false);
        }
        else if ((Input.GetKey(KeyCode.Backspace) || (Input.GetKeyDown(KeyCode.Backspace))) && !edited_file.reading) {
            inputs = inputField.text.Split('\n');
            rowPos = inputs[inputs.Length - 1].Length;
            if (rowPos == 1) {
                inputField.text += " ";
                inputField.MoveTextEnd(false);
            }
        }
        else if (((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
                || (Input.GetKeyDown(KeyCode.UpArrow)) || Input.GetKey(KeyCode.UpArrow) 
                && !edited_file.reading)) {
            inputField.MoveTextEnd(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            string code = "1 + 1";
            JsValue result = jintEvaluator.Evaluate(code, inputField);
            Debug.Log(result.ToString());
            inputField.MoveTextEnd(false);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (edited_file.reading)
            {
                Debug.Log("Input field: " + inputField.text);
                fs.writeFileContent(edited_file.filename, inputField.text);
                inputField.text = history;
                edited_file.reading = false;
                inputField.MoveTextEnd(false);
            }
        }
        textComponent.ForceMeshUpdate();
    }
}
