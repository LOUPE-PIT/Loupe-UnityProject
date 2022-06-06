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
    // Variables needed for getting a model and putting it in the right directory.

    private string zipCacheFolderPath = "/Assets/Cache";
    private string zipCacheFullPath;

    private string zipFileName = "response.zip";
    private string zipPathExtractPath = "/Extract";

    private string directoryName;
    private string currentDirectory;

    private bool getModelSucceeded = false;

    // Method that gets called on a button click
    public void DownloadModel()
    {
        CreateDirectory();
        GetModel();

        if(getModelSucceeded)
        {
            Unzip();
            ExtractModel();
            MoveModel();
            DeleteCache();
        }
        else
        {
            Debug.LogError("Unable to get zip file from the API.");
        }
    }

    // Creating the cache directory, it's meant for temporary files.
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

    // Getting a model via an API call.
    private void GetModel()
    {
        // At the moment the system fetches a url hardcoded 3D model zip. In the future, the system has to get a data after selecting the 3D model from the lst of model. Via the button click the system should know what to get.
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44377/download?id=07826933-0109-4a89-9b51-15e8177f1862"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        if(response.StatusCode == HttpStatusCode.OK)
        {
            getModelSucceeded = true;

            using (var stream = response.GetResponseStream())
            {
                using var file = File.Open(@"" + zipCacheFullPath + "/" + zipFileName, FileMode.Create);

                stream.CopyTo(file);
                stream.Flush();
                stream.Close();
            }
        }
        else
        {
            getModelSucceeded = false;
        }
    }

    // Unzipping the 3D model zip.
    private void Unzip()
    {
        if (!Directory.Exists(zipCacheFullPath + "/" + zipPathExtractPath))
        {
            ZipFile.ExtractToDirectory(zipCacheFullPath + "/" + zipFileName, zipCacheFullPath + zipPathExtractPath);
        }
    }

    // Checking the content of the zip after extracting, return the unitypackage file name to the system.
    private string CheckFilesInDirectory()
    {
        var info = new DirectoryInfo(zipCacheFullPath + "/" + zipPathExtractPath);
        var fileInfo = info.GetFiles();
        string[] names = fileInfo[0].Name.Split('.');

        directoryName = names[0];

        return fileInfo[0].Name;
    }

    // Turing the .unitypackage to directories with content.
    private async void ExtractModel()
    {
        currentDirectory = Directory.GetCurrentDirectory();

        string fileName = CheckFilesInDirectory();
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = currentDirectory + "/Assets/Cache/",
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = currentDirectory + "/Assets/UnityPackageExtractor/extractor.exe",
            RedirectStandardInput = true,
            UseShellExecute = false,
            Arguments = "Extract/" + fileName + " " + currentDirectory + zipCacheFolderPath,
            CreateNoWindow = true
        };

        Process.Start(startInfo);
        WaitOneSecondAsync();
    }

    // Delaying the system, this is needed since the extraction needs some time to be finishes. The 5 seconds should be enough for most models.
    private async Task WaitOneSecondAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
    }

    // Moving the extacted files to the model directory.
    private void MoveModel()
    {
        if (!Directory.Exists(currentDirectory))
        {
            Directory.CreateDirectory(currentDirectory);
        }

        if(!Directory.Exists(currentDirectory + "/Assets/3DModels/" + directoryName))
        {
            FileUtil.CopyFileOrDirectory(currentDirectory + "/Assets/Cache/Assets/", currentDirectory + "/Assets/3DModels/" + directoryName);
            AssetDatabase.Refresh();
        }
    }

    // Removing the cache directory, by that removing the downloaded temporary content.
    private void DeleteCache()
    {
        FileUtil.DeleteFileOrDirectory(currentDirectory + "/Assets/Cache");
    }
}
