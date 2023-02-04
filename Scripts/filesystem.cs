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
        {
            if (file != "")
            {
                #if UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS
                    tmp += file.Split("/").Last();
                #elif UNITY_STANDALONE_WIN
                    tmp += dir.Split("\\").Last();
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