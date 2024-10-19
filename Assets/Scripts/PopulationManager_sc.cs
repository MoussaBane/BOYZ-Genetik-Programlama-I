using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager_sc : MonoBehaviour
{
   public GameObject personPrefab; // Person nesnesi referansı
    public int populationSize = 10; // Popülasyon büyüklüğü
    public static float elapsed = 0; // Zaman takibi
    private List<GameObject> population = new List<GameObject>(); // Popülasyon listesi
    private int trialTime = 10; // Her döngünün süresi
    private int generation = 1; // Jenerasyon sayısı
    GUIStyle guiStyle = new GUIStyle(); // GUI yazı stili

    // Başlangıçta popülasyonu oluşturur
    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-3.4f, 5.4f), 0);
            GameObject o = Instantiate(personPrefab, pos, Quaternion.identity);
            o.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            o.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            o.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            population.Add(o);
        }
    }

    // Her jenerasyonun zamanını günceller ve jenerasyonu değiştirir
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            BreedNewPopulation(); // Yeni jenerasyon oluştur
            elapsed = 0;
        }
    }

    // Yeni jenerasyonu oluşturur
    void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        population.Clear();

        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]); // Eski nesneleri yok et
        }

        generation++;
    }

    // İki ebeveynden yeni bir nesil oluşturur
    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-3.4f, 5.4f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;

        return offspring;
    }

    // Ekranda jenerasyon ve süre bilgisi gösterir
    void OnGUI()
    {
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Jenerasyon: " + generation, guiStyle);
        GUI.Label(new Rect(10, 30, 100, 20), "Zaman: " + (int)elapsed, guiStyle);
    }

}
