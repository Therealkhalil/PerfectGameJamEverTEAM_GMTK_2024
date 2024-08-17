using UnityEngine.SceneManagement;
using UnityEngine;

/*Summary*/
// This script is used to respawn the player when they collide with an obstacle.
public class DeathRespawn : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
