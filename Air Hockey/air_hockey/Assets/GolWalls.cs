using UnityEngine;
using System.Collections;
public class SideWalls : MonoBehaviour
{
    // Verifica colisões da puck nas paredes
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "puck")
        {
            string wallName = transform.name;
            GameManager.Score(wallName);
            hitInfo.gameObject.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }
    }
}