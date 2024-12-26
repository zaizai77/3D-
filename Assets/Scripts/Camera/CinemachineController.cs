using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    private CinemachineFreeLook vcam;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        // 自己通过代码获取 x、y分量，比如通过摇杆获取，这里我就仍然使用 Mouse X 和 Mouse Y吧
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        //相机移动
        vcam.m_XAxis.m_InputAxisValue = -x;
        vcam.m_YAxis.m_InputAxisValue = -y;
    }
}
