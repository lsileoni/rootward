using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Linq;

struct s_FileState
{
    int files_created;
}

public class FileSystem
{
    private string filepath;
    private string origin;
    private string root_name;
    private string root_path;

    public string GetCurrentDirectory()
    {
        return (filepath);
    }

    public FileSystem(string start)
    {
        origin = start;
        root_name = "YwzZsc3fNP";
        root_path = start + "/" + root_name;
        if (!Directory.Exists(root_path))
            Directory.CreateDirectory(root_path);
                if (!Directory.Exists(root_path))
            Directory.CreateDirectory(root_path);
        if (!Directory.Exists(root_path + "/127.0.0.1"))
            Directory.CreateDirectory(root_path + "/127.0.0.1");
        filepath = root_path + "/127.0.0.1";
        if (!Directory.Exists(root_path + "/192.168.1.1"))
            Directory.CreateDirectory(root_path + "/192.168.1.1");
        if (!Directory.Exists(root_path + "/192.168.1.2"))
            Directory.CreateDirectory(root_path + "/192.168.1.2");
        if (!Directory.Exists(root_path + "/192.168.1.3"))
            Directory.CreateDirectory(root_path + "/192.168.1.3");
        if (!Directory.Exists(root_path + "/192.168.1.4"))
            Directory.CreateDirectory(root_path + "/192.168.1.4");
        if (!File.Exists(root_path + "/127.0.0.1/instructions.txt"));
            createFile(root_path + "/127.0.0.1/instructions.txt", "Welcome to 127.0.0.1.\n\nThis is a machine of TEKTRONIX INCORPORATED, if you wish to escape please consult the mainframe.\n\n");

        if (!Directory.Exists(root_path + "/93.1.183.174"))
            Directory.CreateDirectory(root_path + "/93.1.183.174");
        if (!Directory.Exists(root_path + "/93.1.183.174/data"))
            Directory.CreateDirectory(root_path + "/93.1.183.174/data");
        if (!Directory.Exists(root_path + "/93.1.183.174/data/hashes"))
            Directory.CreateDirectory(root_path + "/93.1.183.174/data/hashes");
        if (!Directory.Exists(root_path + "/93.1.183.174/data/bytes"))
            Directory.CreateDirectory(root_path + "/93.1.183.174/data/bytes");
        if (!File.Exists(root_path + "/93.1.183.174/data/hashes/secret.data"))
            createFile(root_path + "/93.1.183.174/data/hashes/secret.data", "7a24f8f4863b6411c912b22dfe25e3d2e081eb51");
        if (!File.Exists(root_path + "/93.1.183.174/data/bytes/byte42.data"))
            createFile(root_path + "/93.1.183.174/data/bytes/byte42.data", "00000000");
        if (!File.Exists(root_path + "/93.1.183.174/data/bytes/byte69.data"))
            createFile(root_path + "/93.1.183.174/data/bytes/byte69.data", "01000101");

        if (!Directory.Exists(root_path + "/248.185.51.148"))
            Directory.CreateDirectory(root_path + "/248.185.51.148");
        if (!File.Exists(root_path + "/248.185.51.148/hello_world.js"))
            createFile(root_path + "/248.185.51.148/hello_world.js", "let msg = \"Hello, world!\"\nprintln(message);");
        if (!Directory.Exists(root_path + "/136.13.38.91"))
            Directory.CreateDirectory(root_path + "/136.13.38.91");
        if (!Directory.Exists(root_path + "/192.168.1.1/routing"))
            Directory.CreateDirectory(root_path + "/192.168.1.1/routing");
        if (!Directory.Exists(root_path + "/192.168.1.4/mainframe"))
            Directory.CreateDirectory(root_path + "/192.168.1.4/mainframe");
        if (!File.Exists(root_path + "/192.168.1.1/info.txt"))
            createFile(root_path + "/192.168.1.1/info.txt", "##DYNAMIX ROUTER\n#PROPERTY OF TEKTRONIX INCORPORATED\n\nCONNECTED MACHINES: 192.168.1.2 - HOSTNAME: TEKTRONIX-PC\nCONNECTED MACHINES: 192.168.1.3 - HOSTNAME: MICROWAVINATOR-9000\nCONNECTED MACHINES: 192.168.1.4 - HOSTNAME: TOASTERTRON\nCONNECTED MACHINES: 192.168.1.5 - HOSTNAME: WD6GNUGC2B");
        if (!File.Exists(root_path + "/192.168.1.1/sequence.txt"))
            createFile(root_path + "/192.168.1.1/sequence.txt", "Three challenges, three keys to hold,\nSolve them all, the secrets are told.\nThe first, a 37th twist and turn.\nThe second's a ski, so free and so bright,\nThe final is as prime as time,\nBring them all together, to turn off the light.\nA key to command, with numbers untold,");
        if (!File.Exists(root_path + "/192.168.1.1/routing/DYNAMIX.txt"))
            createFile(root_path + "/192.168.1.1/routing/DYNAMIX.txt", "A network of machines, all connected in one place\nThe central router, a master of space\nWith a name that suggests power and might\nThe answer to this riddle, will give you the right\n\nTo connect to the devices, you must solve the clue\nA word made of seven letters, it's up to you\nStart with the third letter of the router's name\nThen count four letters ahead, you'll find the same\n\nDo this for each letter, and don't be afraid\nThe passphrase is the result, when all letters are made");
        if (!File.Exists(root_path + "/192.168.1.2/TEKTRONIX.txt"))
            createFile(root_path + "/192.168.1.2/TEKTRONIX.txt", "Tektronix has many oscilloscopes,\nAnd sequences to analyze with probes,\nBut one pattern, though not shown on screens,\nIs a spiral that always will gleam.\nFrom ones, to twos, and then again to threes,\nThis sequence grows with each count, it will please,\nTo those who seek its secrets, it will tell,\nA tale of numbers that ring, like a bell.\nSo take this challenge, and give it a try,\nSee if you can make this sequence fly.\n\n1, 1, 2, 3, 5 ... 37th = ?");
        if (!File.Exists(root_path + "/192.168.1.3/toast.txt"))
            createFile(root_path + "/192.168.1.3/toast.txt", "Here's a riddle for you to ponder,\nWith a hex so cursed and blight,\nYou'll turn them into a fry with all your might.\n\nTake each pair, don't you know,\nAnd use their values to make them glow,\nWith characters that you can read,\nAnd form a message that you'll soon need.\n\nJust like a skier glides down the slope,\nSo too your answer will help you hope,\nDecode the hex and set it free.\n\n'74 6f 61 73 74 20 74 68 65 20 74 6f 61 73 74 65 72'");
        if (!File.Exists(root_path + "/192.168.1.4/mainframe/amazon.txt"))
            createFile(root_path + "/192.168.1.4/mainframe/amazon.txt", "Numbers that rule with an iron fist,\nChosen by the gods, never to be missed.\nUnder or equal 4242, they hide and play,\nAdding them up, will lead the way.\n\nSeek the ones, with no equal peers,\nTheir kingdom will last, for many years.\nThe sum of these rulers, so rare and divine,\nWill unlock the answer, for all to find.\n\nLet the quest begin, for the wise and true,\nTo uncover the secrets, hidden from view.\n2, 3, 5, 7, 11, and so on,\nFind the sum, and you'll have won.");
        if (!File.Exists(root_path + "/192.168.1.4/mainframe/olympus.txt"))
            createFile(root_path + "/192.168.1.4/mainframe/olympus.txt", "I am a fridge, cooling down the room,\nA necessity in the heat of high noon.\nBut when it's time to rest, I must comply,\nAnd follow the command, before I say goodbye.\n\nBut how to do so, is not as clear,\nA secret message, holds the answer dear.\nA string of letters, jumbled and mixed,\nWhen rearranged, the truth will be fixed.\n\nShift the letters to the left or right,\nUntil the message is clear in sight.\n\n'sgd bnllzmc hr onvdqnee'");
    }

    public void createFile(string path, string content)
    {
        if (!File.Exists(path))
        {
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
        }
    }

    public string GetMachineRoot ()
    {
		if (GetCurrentDirectory() == GetAbsoluteRootFilepath())
			return (root_name);
		return (GetRelativeRootFilepath().Split("/")[1]);

    }

    public string GetAbsoluteRootFilepath ()
    {
        return (root_path);
    }
    public string GetRelativeRootFilepath ()
    {
        return (filepath.Split(root_path).Last());
    }

    public void removeFile(string filename)
    {
        string path = filepath + "/" + filename;
        if (File.Exists(path))
            File.Delete(path);
    }

    public void writeFileContent(string filename, string content)
    {
        Debug.Log("FILENAME IN WRITEFILECONTENT: " + filename);
        Debug.Log("CONTENT IN WRITEFILECONTENT: " + content);
        string path = filepath + "/" + filename;
        Debug.Log("PATH: " + path);
        if (!File.Exists(path))
            createFile(path, content);
        else
        {
            removeFile(filename);
            createFile(path, content);
        }
    }

    public void createDirectory(string dirname)
    {
        string path = filepath + "/" + dirname;
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    public bool fileExists(string filename)
    {
        if (File.Exists(filepath + "/" + filename))
            return true;
        else
            return false;
    }

	public bool fileInRoot(string filename)
	{
        if (File.Exists(root_path + "/" + GetMachineRoot() + "/" + filename))
            return true;
        else
            return false;
	}

    public void goToDir(string dirname)
    {
        string path = filepath + "/" + dirname;
        if (!Directory.Exists(path))
            return;
        else
            filepath = path;
    }

    public void goBackDir()
    {
        if (filepath == origin)
            return;
        else
            filepath = Path.GetDirectoryName(filepath);
    }

    public string GetFileContentPath(string path)
    {
        if (!File.Exists(path))
            return "";
        else
            return (File.ReadAllText(path));
    }

    public string GetFileContent(string filename)
    {
        string path = filepath + "/" + filename;
        if (!File.Exists(path))
            return "";
        else
            return (File.ReadAllText(path));
    }

    public string   GetFiles()
    {
        string tmp = "";
        foreach (string file in Directory.GetFiles(filepath))
        {
            if (file != "")
            {
                #if UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS
                    tmp += file.Split("/").Last();
                #elif UNITY_STANDALONE_WIN
                    tmp += file.Split("\\").Last();
                #endif
                tmp += " ";
            }
        }
        return (tmp);
    }

    public string GetDirectories()
    {
        string tmp = "";
        foreach (string dir in Directory.GetDirectories(filepath))
        {
            if (dir != "")
            {
                #if UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS
                    tmp += dir.Split("/").Last();
                #elif UNITY_STANDALONE_WIN
                    tmp += dir.Split("\\").Last();
                #endif
                tmp += " ";
            }
        }

        return (tmp);
    }
    public string GetRootDirectories()
    {
        string tmp = "";
        foreach (string dir in Directory.GetDirectories(root_path))
        {
            if (dir != "")
            {
                #if UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS
                    tmp += dir.Split("/").Last() + "\n";
                #elif UNITY_STANDALONE_WIN
                    tmp += dir.Split("\\").Last() + "\n";
                #endif
                tmp += " ";
            }
        }

        return (tmp);
    }

    public string GoToRelativeRootFilepath(string ip)
    {
        if (Directory.Exists(root_path + "/" + ip))
        {
            filepath = root_path + "/" + ip;
            return ("success");
        }
        return ("failure");
    }

    public string   GetFilesWithPath()
    {
        string tmp = "";
        foreach (string file in Directory.GetFiles(filepath))
            tmp += file;
        return (tmp);
    }

    public string GetDirectoriesWithPath()
    {
        string tmp = "";
        foreach (string dir in Directory.GetDirectories(filepath))
            tmp += dir + " ";
        return (tmp);
    }
}