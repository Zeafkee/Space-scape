using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask spaceshipLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, spaceshipLayer))
            {
                Controller spaceship = hit.collider.GetComponent<Controller>();
                if (spaceship != null)
                {
                    spaceship.StartMovement();
                }
            }
        }
    }
}
