using Photon.Pun;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    public void DestroyGameObject(GameObject gameObject)
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
