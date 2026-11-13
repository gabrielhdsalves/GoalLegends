using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int team1 = 0;
    private int team2 = 0;
    private string name1 = "BAR";
    private string name2 = "RMA";

    private List<TextMeshProUGUI> textosFilhos = new List<TextMeshProUGUI>();

    // Referências individuais para facilitar o acesso
    private TextMeshProUGUI textoPlacar1;
    private TextMeshProUGUI textoNome1;
    private TextMeshProUGUI textoNome2;
    private TextMeshProUGUI textoPlacar2;

    private void Awake()
    {
        GetComponentsInChildren<TextMeshProUGUI>(true, textosFilhos);

        // Mapeia cada índice conforme a ordem na Hierarchy:
        // 0: placar1 | 1: name1 | 2: name2 | 3: placar2
        if (textosFilhos.Count >= 4)
        {
            textoPlacar1 = textosFilhos[0];
            textoNome1 = textosFilhos[1];
            textoNome2 = textosFilhos[2];
            textoPlacar2 = textosFilhos[3];

            // Define os valores iniciais na tela
            textoPlacar1.text = team1.ToString();
            textoNome1.text = name1;
            textoNome2.text = name2;
            textoPlacar2.text = team2.ToString();
        }
        else
        {
            Debug.LogWarning("Faltam objetos TextMeshProUGUI filhos no Score!");
        }
    }

    private void OnEnable()
    {
        Gol.OnGolMarcado += AtualizarPlacar;
    }

    private void OnDisable()
    {
        Gol.OnGolMarcado -= AtualizarPlacar;
    }

    private void AtualizarPlacar(int timeQueMarcou)
    {
        if (timeQueMarcou == 1)
        {
            team2++;
            if (textoPlacar2 != null) textoPlacar2.text = team2.ToString();
        }
        else if (timeQueMarcou == 2)
        {
            
            
            team1++;
            if (textoPlacar1 != null) textoPlacar1.text = team1.ToString();
        }
    }
}