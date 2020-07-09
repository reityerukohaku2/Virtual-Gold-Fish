using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGenerateRipple : MonoBehaviour
{
    bool onGenerate = false;
    public CustomRenderTexture rippleMap;

    public void GenerateRipple(Vector2 XZPos)
    {
        Vector2 UV = new Vector2();
        Ray ray = new Ray(new Vector3(XZPos.x, 50, XZPos.y), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, 1 << LayerMask.NameToLayer("Water")))
        {
            if (true/*hit.collider.gameObject.tag == "Water"*/)
            {
                UV = hit.textureCoord;

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
                updateZone.updateZoneCenter = new Vector2(UV.x, 1.0f - UV.y);
                updateZone.updateZoneSize = new Vector2(0.01f, 0.01f);

                rippleMap.SetUpdateZones(new CustomRenderTextureUpdateZone[] { defaultZone, updateZone });
                onGenerate = true;
            }
        }
    }

    private void Update()
    {
        if(onGenerate)
        {
            onGenerate = false;
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
