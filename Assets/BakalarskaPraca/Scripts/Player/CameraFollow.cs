using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // V tomto scripte sa nastavujú hodnoty aby sme zabezpečili že kamera nasleduje hráča pri pohybe a hráč nám neujde z herného okna

    [Header("Referencie na komonenenty")]
    [SerializeField] private Transform target; 

    [Header("Vstupné parametre")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothFactor;

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    // Defaultne má naša kamera Z hodnotu - 10
    // Kedže sme si zobrali Transform nášho targetu (hráča) nová hodnota pozície kamery bude priamo na hráčovi takže Z bude 0 a my nebudeme nič vidiet
    // Preto sme si zadefinovali offset a kedže náš target ma z hodnotu 0
    // offset hodnotu z nastavíme v Inspectore na - 10 a kamera sa nachádza v pôvodnom pohlade  Z -10 s tým že bude nasledovat hráčov pohyb

    private void FollowPlayer() 
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    } 
}
