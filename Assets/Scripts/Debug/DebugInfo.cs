//
// Copyright 2022 SunnyMonster
//

using System.Collections;
using TMPro;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;

    private IEnumerator Start()
    {
        while (true)
        {
            _fpsText.text = $"{(int)(1f / Time.deltaTime)} FPS";
            yield return new WaitForSeconds(1f);
        }
    }
}
