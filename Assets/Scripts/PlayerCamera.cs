using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] public GameObject target;

    Player player;
    Volume volume;
    Vignette vignette;
    public ChromaticAberration chromaticAberration;
    public DepthOfField depthOfField;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out depthOfField);
    }

    void Update()
    {
        vignette.intensity.Override(1 - player.GetHPRatio());

        //Make camera follow the player
        if (target == null)
        {
            return;
        }

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
    
}
