using UnityEngine;

public class GroupAnatomy : MonoBehaviour
{
    public GameObject bone;
    public GameObject urinary;
    public GameObject repro;
    public GameObject muscle;
    public GameObject colon;
    public GameObject prostate;

    public GameObject realProstates;
    public GameObject DREProstates;
    public GameObject realRectum;
    public GameObject DRERectum;

    public GameObject skin;
    public GameObject coccyx;

    public Material benchtopNormal;
    public Material benchtopTransparent;

    private GameObject[] anatomyExpandable;
    private GameObject[] tooltips;

    private Renderer skinRenderer;
    public Renderer SkinRenderer {
        get
        {
            if (skinRenderer == null)
            {
                skinRenderer = skin.GetComponent<Renderer>();
            }
            return skinRenderer;
        }
        set
        {
            skinRenderer = value;
        }
    }
    
    private Renderer dreRectumRenderer;
    public Renderer DRERectumRenderer
    {
        get
        {
            if (dreRectumRenderer == null)
            {
                dreRectumRenderer = DRERectum.GetComponent<Renderer>();
            }
            return dreRectumRenderer;
        }
        set
        {
            dreRectumRenderer = value;
        }
    }

    private Renderer coccyxRenderer;
    public Renderer CoccyxRenderer
    {
        get
        {
            if (coccyxRenderer == null)
            {
                coccyxRenderer = coccyx.GetComponent<Renderer>();
            }
            return coccyxRenderer;
        }
        set
        {
            coccyxRenderer = value;
        }
    }

    private MaterialSetter DRERectumMaterialSetter;
    private MaterialSetter realRectumMaterialSetter;

    private void Awake()
    {
        anatomyExpandable = GameObject.FindGameObjectsWithTag("AnatomyAnimation");
        tooltips = GameObject.FindGameObjectsWithTag("ToolTip");

        skinRenderer = skin.GetComponent<Renderer>();
        DRERectumRenderer = DRERectum.GetComponent<Renderer>();
        coccyxRenderer = coccyx.GetComponent<Renderer>();

        DRERectumMaterialSetter = DRERectum.GetComponent<MaterialSetter>();
        realRectumMaterialSetter = realRectum.GetComponent<MaterialSetter>();
    }

    public void ShowNormalBenchtop()
    {
        SetBenchtopVisiblewithMaterial(true, benchtopNormal);
    }

    public void ShowTransparentBenchtop()
    {
        SetBenchtopVisiblewithMaterial(true, benchtopTransparent);
    }

    public void ShowTransparentAnatomy()
    {
        SetBenchtopVisiblewithMaterial(false, benchtopTransparent);
    }

    public void ShowProstate(string name)
    {
        foreach (Transform child in DREProstates.transform)
        {
            GameObject prostate = child.gameObject;
            prostate.SetActive(prostate.name == name);
        }
        foreach (Transform child in realProstates.transform)
        {
            GameObject prostate = child.gameObject;
            prostate.SetActive(prostate.name == name);
        }
    }

    public void ResetAnatomyPosition()
    {
        bone.transform.localPosition = Vector3.zero;
        urinary.transform.localPosition = Vector3.zero;
        repro.transform.localPosition = Vector3.zero;
        muscle.transform.localPosition = Vector3.zero;
        skin.transform.localPosition = Vector3.zero;
        coccyx.transform.localPosition = Vector3.zero;
        colon.transform.localPosition = Vector3.zero;
        prostate.transform.localPosition = Vector3.zero;

        bone.transform.localScale = Vector3.one;
        urinary.transform.localScale = Vector3.one;
        repro.transform.localScale = Vector3.one;
        muscle.transform.localScale = Vector3.one;
        skin.transform.localScale = Vector3.one;
        coccyx.transform.localScale = Vector3.one;
        colon.transform.localScale = Vector3.one;
        prostate.transform.localScale = Vector3.one;

        bone.transform.localEulerAngles = Vector3.zero;
        urinary.transform.localEulerAngles = Vector3.zero;
        repro.transform.localEulerAngles = Vector3.zero;
        muscle.transform.localEulerAngles = Vector3.zero;
        skin.transform.localEulerAngles = Vector3.zero;
        coccyx.transform.localEulerAngles = Vector3.zero;
        colon.transform.localEulerAngles = Vector3.zero;
        prostate.transform.localEulerAngles = Vector3.zero;
    }

    private void SetBenchtopVisiblewithMaterial(bool isVisible, Material material)
    {

        SkinRenderer.material = material;
        DRERectumRenderer.material = material;
        CoccyxRenderer.material = material;

        SetVisibleDRE(isVisible);
        SetVisibleAnatomy(!isVisible);

        //ToggleTooltip(false);
        ToggleTooltip(!isVisible);
    }

    private void SetVisibleDRE(bool show)
    {
        DRERectumMaterialSetter.SetMaterialClear(!show);
        DREProstates.SetActive(show);
    }

    private void SetVisibleAnatomy(bool show)
    {
        SetChildrenVisibility(bone, show);
        SetChildrenVisibility(urinary, show);
        SetChildrenVisibility(repro, show);
        SetChildrenVisibility(muscle, show);
        realRectumMaterialSetter.SetMaterialClear(!show);
        realProstates.SetActive(show);
    }

    private void ToggleTooltip(bool active)
    {
        if (tooltips != null && tooltips.Length > 0)
        {
            foreach (GameObject tooltip in tooltips)
            {
                if (tooltip.activeSelf != active)
                {
                    tooltip.SetActive(active);
                }
            }
        }
        else
        {
            Debug.Log("No tooltips found");
        }
    }

    private void SetChildrenVisibility(GameObject organ, bool show)
    {
        foreach (Transform childOrgan in organ.transform)
        {
            if (childOrgan.gameObject.tag != "ToolTip")
            {
                childOrgan.gameObject.GetComponent<MaterialSetter>().SetMaterialClear(!show);
            }
        }
    }
}

