//
// Copyright 2022 SunnyMonster
//

using System.Collections;
using TMPro;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;
    [SerializeField] private TextMeshProUGUI _versionText;
    [Space]
    [SerializeField] private string _fpsFormat = "{0} FPS";

    private IEnumerator Start()
    {
        _versionText.text = Application.version;
        while (true)
        {
            _fpsText.text = string.Format(_fpsFormat, (int)(1f / Time.deltaTime));
            //yield return null;
            yield return new WaitForSeconds(1f);
        }
    }
}
