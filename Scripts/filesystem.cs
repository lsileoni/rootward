using UnityEngine;
using System;
using System.IO;
using System.Text;

struct s_FileState
{
    int files_created;
}

public class FileSystem
{
    private string filepath;
    private string origin;

    public string GetCurrentDirectory()
    {
        return (filepath);
    }

    public FileSystem(string start)
    {
        filepath = start;
        origin = start;
        if (!Directory.Exists(start + "/one"))
            Directory.CreateDirectory(start + "/one");
        if (!File.Exists(start + "/one/instructions.txt"))
            createFile(start + "/one/instructions.txt", "Welcome to 127.0.0.1; nothing like home!");
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
            tmp += file + " ";
        return (tmp);
    }

    public string GetDirectories()
    {
        string tmp = "";
        foreach (string dir in Directory.GetDirectories(filepath))
            tmp += dir + " ";
        return (tmp);
    }
}