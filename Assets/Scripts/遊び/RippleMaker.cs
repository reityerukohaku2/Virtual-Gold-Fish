using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleMaker : MonoBehaviour
{
    public CustomRenderTexture rippleMap;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                CustomRenderTextureUpdateZone defaultZone = new CustomRenderTextureUpdateZone();
                defaultZone.needSwap = true;
                defaultZone.passIndex = 0;
                defaultZone.rotation = 0;
                defaultZone.updateZoneCenter = new Vector2(0.5f, 0.5f);
                defaultZone.updateZoneSize = new Vector2(1, 1);

                CustomRenderTextureUpdateZone updateZone = new CustomRenderTextureUpdateZone();
                updateZone.needSwap = true;
                updateZone.passIndex = 1;
                updateZone.rotation = 0;
                updateZone.updateZoneCenter = new Vector2(hit.textureCoord.x, 1.0f - hit.textureCoord.y);
                updateZone.updateZoneSize = new Vector2(0.02f, 0.04f);

                rippleMap.SetUpdateZones(new CustomRenderTextureUpdateZone[] {defaultZone, updateZone });

            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            CustomRenderTextureUpdateZone defaultZone = new CustomRenderTextureUpdateZone();
            defaultZone.needSwap = true;
            defaultZone.passIndex = 0;
            defaultZone.rotation = 0;
            defaultZone.updateZoneCenter = new Vector2(0.5f, 0.5f);
            defaultZone.updateZoneSize = new Vector2(1, 1);

            rippleMap.SetUpdateZones(new CustomRenderTextureUpdateZone[] { defaultZone });
        }
    }
}
