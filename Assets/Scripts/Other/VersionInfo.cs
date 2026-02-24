using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class VersionInfo : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetLatestReleaseName());
    }
    private string owner = "Yokiarii";
    private string repo = "Undertale-Fun-Remake";

    IEnumerator GetLatestReleaseName()
    {
        string url = $"https://api.github.com/repos/{owner}/{repo}/releases/latest";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("User-Agent", "MyUnityApp"); // GitHub требует этот заголовок

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                // Парсим только нужное поле
                var data = JsonUtility.FromJson<ReleaseInfo>(json);
                gameObject.GetComponent<TextMeshProUGUI>().text = Main.Version;
                if(data.tag_name != Main.Tag_Version)
                    gameObject.GetComponent<TextMeshProUGUI>().text += "\n" + "Есть новая версия: " + data.tag_name;
                else
                    gameObject.GetComponent<TextMeshProUGUI>().text += "\n" + "Это актуальная версия.";
                
            }
            else
            {
                Debug.LogError("Ошибка при запросе: " + request.error);
            }
        }
    }

    [System.Serializable]
    public class ReleaseInfo
    {
        public string tag_name;
    }
}
