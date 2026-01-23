using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public JudgeSystem judge;

    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text lifeText;
    public TMP_Text judgeText;

    void Update()
    {
        if (judge == null) return;

        if (scoreText) scoreText.text = $"Score: {judge.score}";
        if (comboText) comboText.text = $"Combo: {judge.combo} (Max {judge.maxCombo})";
        if (lifeText) lifeText.text = $"Life: {judge.life:0}";
        if (judgeText) judgeText.text = judge.lastJudge;
    }
}
