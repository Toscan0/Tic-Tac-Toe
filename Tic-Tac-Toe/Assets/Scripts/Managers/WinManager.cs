
public class WinManager : GenericSingleton<WinManager>
{
    // -1 == Player Lost, 0 == Draw, 1 == Player Win
    static private int playerWin;

    internal int PlayerWin
    {
        get
        {
            return playerWin;
        }

        set
        {
            playerWin = value;

            LoadSceneManager.Instance.LoadNextScene();
        }
    }
}
