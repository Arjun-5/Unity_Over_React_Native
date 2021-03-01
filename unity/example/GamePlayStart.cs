using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GamePlayStart : MonoBehaviour
{
    public AssetBundle environmentAssetBunlde;
    public Slider slider;
    public GameObject UI_Objects;
    public GameObject UI_Control;
    string assetName = "EnvironmentFinal";
    public GameObject rotationTarget;
    public GameObject countDown_UI;
    public AudioSource getReady;
    public AudioSource Go_Play;

    [SerializeField]
    private Rigidbody maleChar;

    [SerializeField]
    private Rigidbody femaleChar;

    string url = "https://drive.google.com/u/0/uc?id=1Cbsqu8hf8BR7pEyCAl8agTTR06Y9zuBZ&export=download";
    bool isDownload = false;

    private bool gameStart = false;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rotationTarget.SetActive(false);
        StartCoroutine(LoadAssetBundle());
        UI_Control.SetActive(false);
        maleChar.isKinematic = true;
        femaleChar.isKinematic = true;
        timer = 0;
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
                environmentAssetBunlde = DownloadHandlerAssetBundle.GetContent(assetDownloadRequest);

                var prefabNames = environmentAssetBunlde.GetAllAssetNames();
                var prefab = environmentAssetBunlde.LoadAsset<GameObject>(assetName);
                Instantiate(prefab);
                maleChar.isKinematic = false;
                femaleChar.isKinematic = false;
                gameStart = true;
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

    private void Update()
    {
        if (gameStart)
        {
            timer += Time.deltaTime;

            if(timer > 3.0f)
            {
                gameStart = false;
                StartCoroutine(startGame());
            }
        }
    }
    IEnumerator startGame()
    {
        countDown_UI.GetComponent<Text>().text = "3";
        getReady.Play();
        countDown_UI.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown_UI.SetActive(false);
        countDown_UI.GetComponent<Text>().text = "2";
        getReady.Play();
        countDown_UI.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown_UI.SetActive(false);
        countDown_UI.GetComponent<Text>().text = "1";
        getReady.Play();
        countDown_UI.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown_UI.SetActive(false);
        countDown_UI.GetComponent<Text>().text = "Start";
        Go_Play.Play();
        countDown_UI.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown_UI.SetActive(false);
        rotationTarget.SetActive(true);
        UI_Control.SetActive(true);
    }
}