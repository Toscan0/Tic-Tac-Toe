using UnityEngine;

[RequireComponent(typeof(GameController))]
public class PlayerController : MonoBehaviour
{
    private GameController gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameController>();    
    }

    public void Button_PlayerInput(GameObject btClicked)
    {
        if (gameManager.IsPlayerTurn && gameManager.IsRunning)
        {
            var slot = btClicked.GetComponent<Slot>();

            if(slot == null)
            {
                Debug.LogError("Slot script not founded in: " + btClicked.name);
                return;
            }

            Move indexs = new Move(slot.GetRow(), slot.GetColumn());
            gameManager.PlayerMove(indexs);
        }
    }
}
