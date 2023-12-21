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

                string config =Path.Combine( path , "config");
                GetUrl(config, out string[] urls);
                CreateUrlFile(path, urls);
            }
        }
        foreach (var path in m_gitiFolder)
        {
            if (Directory.Exists(path)) { 

                string config = Path.Combine(path , "config");
                //Debug.Log(config);
                GetUrl(config, out string[] urls);
                //Debug.Log(string.Join("\n",urls));
                CreateUrlFile(path, urls);
            }
        }


    }

    private void CreateUrlFile(string path, string[] urls)
    {
        for (int i = 0; i < urls.Length; i++)
        {
            string config =urls.Length==1 ? Path.Combine(path, "../GitURL.url") : Path.Combine(path, "../GitURL" + i + ".url");
            File.WriteAllText(config, "[InternetShortcut]\nURL = "+urls[i]+"\n"); 
        }
    }

    private void GetUrl(string config, out string[] urls)
    {
        string t = File.ReadAllText(config);
        List<string> l = new List<string>();
        foreach (var line in t.Split("\n"))
        {
            int i = line.ToLower().IndexOf("url =");
            if (i > -1) {
                //Debug.Log("||"+line);
                l.Add( (line.Substring(i + 5).Trim()));
            }
        }
        urls = l.ToArray();
    }
}
