using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GitToGitiMono : MonoBehaviour
{

    public string[] m_gitFolder;
    public string[] m_gitiFolder;


    [ContextMenu("Refresh")]
    public void Refresh()
    {
        string path = Directory.GetCurrentDirectory() + "\\Assets";
        m_gitFolder = Directory.GetDirectories(path, "*.git", SearchOption.AllDirectories);
        m_gitiFolder = Directory.GetDirectories(path, "*.giti", SearchOption.AllDirectories);

    }
    [ContextMenu("Git to Giti")]
    public void ChangeGitToGiti()
    {
        Refresh();
        foreach (var item in m_gitFolder)
        {
            Directory.Move(item, item.Replace(".git", ".giti"));
        }
        Refresh();
    }
    [ContextMenu("Giti to Git")]
    public void ChangeGitiToGit()
    {
        Refresh();
        foreach (var item in m_gitiFolder)
        {
            Directory.Move(item, item.Replace(".giti", ".git"));
        }
        Refresh();


    }


    [ContextMenu("Generate Url Backup For All Git")]
    public void GenerateUrlBackupForAll() {

        foreach (var path in m_gitFolder)
        {
            if (Directory.Exists(path))
            {

                string config = path + "/config";
                GetUrl(config, out string[] urls);
                CreateUrlFile(path, urls);
            }
        }
        foreach (var path in m_gitiFolder)
        {
            if (Directory.Exists(path)) { 
                string config = path + "/config";
                GetUrl(config, out string[] urls);
                CreateUrlFile(path, urls);
            }
        }


    }

    private void CreateUrlFile(string path, string[] urls)
    {
        for (int i = 0; i < urls.Length; i++)
        {
            string config = path + "/../GitSource"+i+".url";
            File.WriteAllText(config, "[InternetShortcut]\nURL = "+urls[i]+"\n"); 
        }
    }

    private void GetUrl(string config, out string[] urls)
    {
        List<string> l = new List<string>();
        foreach (var line in config.Split("\n"))
        {
            if (line.IndexOf("https") > -1) {
               l.Add( (line.Substring(line.IndexOf("https") + 5).Trim()));
            }
        }
        urls = l.ToArray();
    }
}
