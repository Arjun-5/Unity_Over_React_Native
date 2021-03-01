using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ResolutionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BackGroundReferenceObject = null;
    // Start is called before the first frame update
    void Start()
    {
        float orthoSize = BackGroundReferenceObject.GetComponent<Collider>().bounds.size.x * Screen.height / Screen.width * 0.5f;
        Camera.main.orthographicSize = orthoSize;
    }
}
