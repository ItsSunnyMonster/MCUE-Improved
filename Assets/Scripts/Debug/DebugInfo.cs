/*
 *   Copyright (c) 2022 ItsSunnyMonster
 *   All rights reserved.

 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at

 *   http://www.apache.org/licenses/LICENSE-2.0

 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */

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
