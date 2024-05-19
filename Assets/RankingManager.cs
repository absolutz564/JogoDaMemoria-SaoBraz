using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerScore
    {
        public string playerName;
        public int score;
    }

    public List<PlayerScore> topPlayers = new List<PlayerScore>();
    public int maxRankingSize = 10;
    public GameObject[] rankingEntries; // Coloque os objetos TextMeshPro para cada entrada no ranking aqui.
    public TMP_Text RankingIndex;
    private void Start()
    {
        LoadRanking();
        UpdateRankingUI();
    }

    public void AddPlayerScore(string playerName, int score)
    {
        PlayerScore playerScore = new PlayerScore { playerName = playerName, score = score };
        topPlayers.Add(playerScore);
        topPlayers.Sort((a, b) => b.score.CompareTo(a.score)); // Ordena do maior para o menor.
        if (topPlayers.Count > maxRankingSize)
        {
            topPlayers.RemoveAt(topPlayers.Count - 1); // Remove o último jogador se exceder o tamanho máximo.
        }

        SaveRanking();
        UpdateRankingUI();
    }

    private void UpdateRankingUI()
    {
        string currentUser = PlayerPrefs.GetString("CurrentUser").Trim().ToLower(); // Remove espaços em branco e converte para minúsculas

        for (int i = 0; i < rankingEntries.Length; i++)
        {
            if (i < topPlayers.Count)
            {
                // Atualiza os objetos TextMeshPro com os dados do ranking.
                rankingEntries[i].transform.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                string playerName = topPlayers[i].playerName.Trim().ToLower(); // Remove espaços em branco e converte para minúsculas
                rankingEntries[i].transform.Find("Text Name").GetComponent<TextMeshProUGUI>().text = topPlayers[i].playerName;
                rankingEntries[i].transform.Find("Text Points").GetComponent<TextMeshProUGUI>().text = topPlayers[i].score.ToString();
                rankingEntries[i].SetActive(true);

                // Verifica se o nome do jogador corresponde ao currentUser
                if (playerName == currentUser)
                {
                    rankingEntries[i].transform.Find("Text Name").GetComponent<TextMeshProUGUI>().color = Color.red; // Defina a cor do texto como vermelha

                    if (RankingIndex.text == "#0º")
                    {
                        RankingIndex.text = "#" + ((i + 1).ToString()) + "º"; // Define o valor de RankingIndex como o índice correspondente
                    }
                }
            }
            else
            {
                rankingEntries[i].SetActive(false); // Desativa objetos extras no ranking.
            }
        }
    }


    private void SaveRanking()
    {
        // Salva o ranking usando PlayerPrefs.
        for (int i = 0; i < topPlayers.Count; i++)
        {
            PlayerPrefs.SetString($"PlayerName_{i}", topPlayers[i].playerName);
            PlayerPrefs.SetInt($"PlayerScore_{i}", topPlayers[i].score);
        }
        PlayerPrefs.SetInt("RankingCount", topPlayers.Count);
        PlayerPrefs.Save();
    }

    private void LoadRanking()
    {
        // Carrega o ranking usando PlayerPrefs.
        int rankingCount = PlayerPrefs.GetInt("RankingCount", 0);
        topPlayers.Clear();

        for (int i = 0; i < rankingCount; i++)
        {
            string playerName = PlayerPrefs.GetString($"PlayerName_{i}");
            int playerScore = PlayerPrefs.GetInt($"PlayerScore_{i}");
            topPlayers.Add(new PlayerScore { playerName = playerName, score = playerScore });
        }
    }
}
