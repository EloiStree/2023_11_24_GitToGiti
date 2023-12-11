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
}
