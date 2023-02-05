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
        if (!Directory.Exists(root_path + "/127.0.0.1"))
            Directory.CreateDirectory(root_path + "/127.0.0.1");
        filepath = root_path + "/127.0.0.1";
        if (!File.Exists(root_path + "/127.0.0.1/instructions.txt"));
            createFile(root_path + "/127.0.0.1/instructions.txt", "Welcome to 127.0.0.1, ain't nothing like home.\n\nType h for help\n\n");
        if (!Directory.Exists(root_path + "/93.1.183.174"))
            Directory.CreateDirectory(root_path + "/93.1.183.174");
        if (!Directory.Exists(root_path + "/248.185.51.148"))
            Directory.CreateDirectory(root_path + "/248.185.51.148");
        if (!Directory.Exists(root_path + "/136.13.38.91"))
            Directory.CreateDirectory(root_path + "/136.13.38.91");
        if (!Directory.Exists(root_path + "/228.109.159.41"))
            Directory.CreateDirectory(root_path + "/228.109.159.41");
    }

    public void createFile(string path, string content)
    {
        if (!File.Exists(path))
        {
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(content);
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
                    tmp += dir.Split("/").Last();
                #elif UNITY_STANDALONE_WIN
                    tmp += dir.Split("\\").Last();
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