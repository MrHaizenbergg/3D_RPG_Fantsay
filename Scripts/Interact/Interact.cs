using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Interact : MonoBehaviour
{
    public Interactable focus;

    [Header("Interact Settings")]
    [Tooltip("Distance of ray to interact")]
    public float rayDistance;
    [Tooltip("Layers to interact (default as obstacle)")]
    public LayerMask interactLayers;
    public LayerMask enemyLayers;
    [Tooltip("Tags for interact")]
    public string interactTag;
    public string enemyTag;
    [Tooltip("Inventory script")]
    public Inventory inventory;
    [Header("UI Settings")]
    [Tooltip("UI interactButton for mobile only")]
    public Image interactButton;
    public Image enemyButton;
    public Text interactText;
    public Text enemyText;
    private PlayerController player;
    public PlayerCombat combat;

    private Interactable interactable;

    private bool isMobile;

    private void Awake()
    {
        if (Application.isMobilePlatform)
            isMobile = true;
        else
            isMobile = false;
    }

    private void Start()
    {
        player = PlayerManager.instance.player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.playerMoving)
        {
            RemoveFocus();
        }

        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        if (CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, out hit, rayDistance, enemyLayers))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    interactButton.enabled = false;
                    enemyButton.enabled = true;

                    if (isMobile)
                    {
                        interactText.enabled = false;
                        enemyText.enabled = false;
                    }
                    else
                        enemyText.enabled = true;

                    combat.Attack();
                }
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, out hit, rayDistance, interactLayers))
            {
                if (hit.transform.gameObject.tag == "Interact")
                {
                    interactButton.enabled = true;
                    enemyButton.enabled = false;

                    if (isMobile)
                    {
                        interactText.enabled = false;
                        enemyText.enabled = false;
                    }
                    else
                        interactText.enabled = true;

                    interactable = hit.collider.GetComponent<Interactable>();

                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }
            }
        }
        else
        {
            enemyButton.enabled = false;
            enemyText.enabled = false;
            interactButton.enabled = false;
            interactText.enabled = false;
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
        }
        newFocus.OnFocused(player.transform);
    }
    private void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }
}
