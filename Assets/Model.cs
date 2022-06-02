using UnityEngine;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Model : MonoBehaviour
{
    private string zipCacheFolderPath = "/Assets/Cache";
    private string zipCacheFullPath;
    private string zipFileName = "response.zip";
    private string zipPathExtractPath = "/Extract";

    public void DownloadModel()
    {
        CreateDirectory();
        GetModel();
        Unzip();
        ExtractModel();

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

    private void CheckFilesInDirectory()
    {
        var info = new DirectoryInfo(zipCacheFullPath + "/" + zipPathExtractPath);
        var fileInfo = info.GetFiles();
        Debug.Log(fileInfo[0].Name);
    }

    private void ExtractModel()
    {
        string path = Directory.GetCurrentDirectory();

        var startInfo = new System.Diagnostics.ProcessStartInfo
        {
            WorkingDirectory = path + "/Assets/UnityPackageExtractor/",
            WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
            FileName = path + "/Assets/UnityPackageExtractor/extractor.exe",
            RedirectStandardInput = true,
            UseShellExecute = false,
            Arguments = "1.unitypackage" + " " + path + zipCacheFolderPath
        };
        Process.Start(startInfo);

    }
}
