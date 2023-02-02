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
        if (!File.Exists(start + "/test.js"))
        {
            createFile(start + "/test.js", "log('Hello from JS land!')");
            Debug.Log("test.js doesn't exist, creating");
        }
        else
        {
            Debug.Log("test.json exists, all good!");
        }
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
    public void createDirectory(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    public void goToDir(string dir)
    {
        if (!Directory.Exists(dir))
            return;
        else
            filepath = Path.Combine(dir, filepath);
    }

    public void goBackDir()
    {
        if (filepath == origin)
            return;
        else
            filepath = Path.GetDirectoryName(filepath);
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