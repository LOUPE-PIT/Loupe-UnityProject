using UnityEngine;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Threading.Tasks;
using System;
using UnityEditor;

public class Model : MonoBehaviour
{
    private string zipCacheFolderPath = "/Assets/Cache";
    private string zipCacheFullPath;
    private string zipFileName = "response.zip";
    private string zipPathExtractPath = "/Extract";
    private string directoryName;
    private string currentDirectory;

    public void DownloadModel()
    {
        CreateDirectory();
        GetModel();
        Unzip();
        ExtractModel();
        MoveModel();
        DeleteCache();
    }
    private async Task WaitOneSecondAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(4));
        Debug.Log("Finished waiting.");
    }
    private void GetModel()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44377/download?id=07826933-0109-4a89-9b51-15e8177f1862"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (var stream = response.GetResponseStream())
        {
            using var file = File.Open(@"" + zipCacheFullPath + "/" + zipFileName, FileMode.Create);
            stream.CopyTo(file);
            stream.Flush();
            stream.Close();
        }
    }

    private void CreateDirectory()
    {
        string path = Directory.GetCurrentDirectory();
        path += zipCacheFolderPath;
        zipCacheFullPath = path;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private void Unzip()
    {
        Debug.Log(zipCacheFullPath + "/" + zipFileName);
        Debug.Log(zipCacheFullPath + zipPathExtractPath);

        if (!Directory.Exists(zipCacheFullPath + "/" + zipPathExtractPath))
        {
            ZipFile.ExtractToDirectory(zipCacheFullPath + "/" + zipFileName, zipCacheFullPath + zipPathExtractPath);
        }
    }

    private string CheckFilesInDirectory()
    {
        var info = new DirectoryInfo(zipCacheFullPath + "/" + zipPathExtractPath);
        var fileInfo = info.GetFiles();
        Debug.Log(fileInfo[0].Name);
        string[] names = fileInfo[0].Name.Split('.');
        directoryName = names[0];
        return fileInfo[0].Name;
    }

    private async void ExtractModel()
    {
        currentDirectory = Directory.GetCurrentDirectory();
        string fileName = CheckFilesInDirectory();
        var startInfo = new System.Diagnostics.ProcessStartInfo
        {
            WorkingDirectory = currentDirectory + "/Assets/Cache/",
            WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
            FileName = currentDirectory + "/Assets/UnityPackageExtractor/extractor.exe",
            RedirectStandardInput = true,
            UseShellExecute = false,
            Arguments = "Extract/" + fileName + " " + currentDirectory + zipCacheFolderPath
        };
        Process.Start(startInfo);
        WaitOneSecondAsync();
    }
    private void MoveModel()
    {
        if (!Directory.Exists(currentDirectory))
        {
            Directory.CreateDirectory(currentDirectory);
        }
        FileUtil.CopyFileOrDirectory(currentDirectory + "/Assets/Cache/Assets/", currentDirectory + "/Assets/3DModels/" + directoryName);
        AssetDatabase.Refresh();
    }

    private void DeleteCache()
    {
        FileUtil.DeleteFileOrDirectory(currentDirectory + "/Assets/Cache");
    }
}
