using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI_Character_Selection : MonoBehaviour
{
    [SerializeField]
    private GameObject FemaleCharacter=null;

    [SerializeField]
    private GameObject MaleCharacter=null;

    [SerializeField]
    private GameObject AssetBundleReference = null;

    [SerializeField]
    private Sprite GunText = null;

    [SerializeField]
    private SpriteRenderer headerText = null;

    private Touch UserTouch;
    private Ray RayFromTouchPosition;
    private RaycastHit HitOnObject;
    public static bool FemaleVisible;
    private bool toggleVisibility;
    private bool characterSelected = false;
    private GameObject selectedCharacter;
    private List<GameObject> gunObject = new List<GameObject>();
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        MaleCharacter.SetActive(false);
        FemaleVisible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            UserTouch = Input.GetTouch(0);
            RayFromTouchPosition = Camera.main.ScreenPointToRay(UserTouch.position);
            if(Physics.Raycast(RayFromTouchPosition,out HitOnObject))
            {
                if (HitOnObject.collider.gameObject.CompareTag("Forward") || HitOnObject.collider.gameObject.CompareTag("Reverse"))
                {
                    if (!characterSelected)
                    {
                        toggleVisibility = (FemaleVisible == true) ? false : true;
                        switch (toggleVisibility)
                        {
                            case true:
                                FemaleCharacterEnable();
                                break;
                            case false:
                                MaleCharacterEnable();
                                break;
                        }
                    }
                    else
                    {
                        index = -1;
                        gun_Enable();
                    }
                }
                if (HitOnObject.collider.gameObject.CompareTag("Confirm"))
                {
                    processUserSelection();
                }
            }
        }
    }
    void MaleCharacterEnable()
    {
        MaleCharacter.SetActive(true);
        /*This controls the behaviour of the animator component when the gameobject is disabled
         True -> Keeps the current state of the animator controller
         */
        FemaleCharacter.GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
        FemaleCharacter.SetActive(false);
        FemaleVisible = false;
    }
    void FemaleCharacterEnable()
    {
        MaleCharacter.GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
        MaleCharacter.SetActive(false);
        FemaleCharacter.SetActive(true);
        FemaleVisible = true;
    }
    void gun_Enable()
    {
        for(int i = 0; i < gunObject.Count; i++)
        {
            if(index == -1)
                index = (gunObject[i].activeSelf == true) ? (i == gunObject.Count - 1) ? 0 : 1 : -1;
        }
        gunObject[(index == 1) ? 0 : 1].SetActive(false);
        gunObject[index].SetActive(true);
    }
    void processUserSelection()
    {
        if (!characterSelected)
        {
            selectedCharacter = (FemaleVisible == true) ? FemaleCharacter : MaleCharacter;
            selectedCharacter.SetActive(false);
            characterSelected = true;

            var prefabNames = AssetBundleReference.GetComponent<LoadAssetBundles>().gunAssetBundle.GetAllAssetNames();

            for (int i = 0; i < prefabNames.Length; i++)
            {
                var prefab = AssetBundleReference.GetComponent<LoadAssetBundles>().gunAssetBundle.LoadAsset(prefabNames[i]);
                gunObject.Add((GameObject)Instantiate(prefab));
                gunObject[i].SetActive((gunObject[i].name == "Sniper(Clone)") ? false : true);
            }
            headerText.sprite = GunText;
        }
        else
        {
            StartCoroutine(LoadGamePlayScene("Game_Play"));
        }
    }
    IEnumerator LoadGamePlayScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;

        }
    }
}