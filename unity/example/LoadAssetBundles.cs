using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class LoadAssetBundles : MonoBehaviour
{
    public AssetBundle gunAssetBundle;
    public Slider slider;
    public GameObject UI_Objects;
    string[] assetName = { "AK47wornout", "Sniper" };

    string url = "https://drive.google.com/u/0/uc?id=1gmL5RoYxBK-QYSzdBDbINhkPYiqYQ8St&export=download";
    bool isDownload = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAssetBundle());
    }

    IEnumerator LoadAssetBundle()
    {
        using (UnityWebRequest assetDownloadRequest = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            
            isDownload = true;

            StartCoroutine(progress(assetDownloadRequest));
            
            yield return assetDownloadRequest.SendWebRequest();
            isDownload = false;

            if (assetDownloadRequest.result == UnityWebRequest.Result.ConnectionError || assetDownloadRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(assetDownloadRequest.error);
            }
            else
            {
                slider.value = 1;
                UI_Objects.SetActive(false);
                // Get downloaded asset bundle
                gunAssetBundle = DownloadHandlerAssetBundle.GetContent(assetDownloadRequest);
            }
        }
    }
    IEnumerator progress(UnityWebRequest request)
    {
        while (isDownload)
        {
            slider.value = request.downloadProgress;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
