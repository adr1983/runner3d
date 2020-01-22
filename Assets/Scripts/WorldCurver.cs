using UnityEngine;

[ExecuteInEditMode]
public class WorldCurver : MonoBehaviour
{
	
	/*importar shades para as pastas para funcionar*/
	[Range(-0.1f, 0.1f)]
	public float curveStrength = 0.01f;

    int m_CurveStrengthID;

    private void OnEnable()
    {
        m_CurveStrengthID = Shader.PropertyToID("_CurveStrength");
    }

	void Update()
	{
		Shader.SetGlobalFloat(m_CurveStrengthID, curveStrength);
	}
}
