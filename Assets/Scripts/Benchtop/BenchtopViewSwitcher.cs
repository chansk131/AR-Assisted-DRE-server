using UnityEngine;

public class BenchtopViewSwitcher : MonoBehaviour
{
    public GameObject pelvicAnatomy;

    private Game game;
    private GroupAnatomy groupAnatomy;

    // Use this for initialization
    void Start()
    {
        game = Game.Instance;

        game.OnModelViewChange += OnModelViewChange;
        game.OnProstateChange += OnProstateChange;

        groupAnatomy = pelvicAnatomy.GetComponent<GroupAnatomy>();

        if (groupAnatomy == null)
        {
            Debug.Log("GroupAnatomy is not attached to PelvicAnatomy GameObject");
        }
        else
        {
            // start with normal benchtop
            ShowProstate();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowProstate();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShowAnatomy();
        }
    }

    private void OnProstateChange(ProstateType prostate)
    {
        if (groupAnatomy != null)
        {
            Debug.Log(prostate.ToString());
            groupAnatomy.ShowProstate(prostate.ToString());
        }
    }

    private void OnModelViewChange(ModelView modelView)
    {
        if (groupAnatomy != null)
        {
            Debug.Log(modelView);
            switch (modelView)
            {
                case ModelView.Skin:
                    ShowSkin();
                    break;
                case ModelView.Prostate:
                    ShowProstate();
                    break;
                case ModelView.Anatomy:
                    ShowAnatomy();
                    break;
                default:
                    ShowSkin();
                    break;
            }
        }
    }

    public void ShowSkin()
    {
        if (groupAnatomy != null)
        {
            groupAnatomy.ShowNormalBenchtop();
            groupAnatomy.ShowProstate(game.Prostate.ToString());
        }
    }

    public void ShowProstate()
    {
        if (groupAnatomy != null)
        {
            groupAnatomy.ShowTransparentBenchtop();
            groupAnatomy.ShowProstate(game.Prostate.ToString());
        }
    }

    public void ShowAnatomy()
    {
        if (groupAnatomy != null)
        {
            groupAnatomy.ShowTransparentAnatomy();
            groupAnatomy.ShowProstate(game.Prostate.ToString());
        }
    }
}