using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
   
    public float r; // Kırmızı renk genetik kodu
    public float g; // Yeşil renk genetik kodu
    public float b; // Mavi renk genetik kodu
    public bool isDead = false; // Nesnenin ölüp ölmediğini takip eder
    public float timeToDie = 0; // Nesnenin ölüm zamanı
    SpriteRenderer sRenderer; // Görüntü bileşeni
    Collider2D sCollider; // Çarpışma bileşeni

    // Başlangıçta bileşenleri ilklendirir
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, g, b); // Nesnenin rengini atar
    }

    // Nesneye tıklandığında ölür
    void OnMouseDown()
    {
        isDead = true;
        timeToDie = PopulationManager_sc.elapsed; // Geçen süreyi alır
        sRenderer.enabled = false; // Görüntüyü kapatır
        sCollider.enabled = false; // Çarpışmayı kapatır
        Debug.Log("Öldü: " + timeToDie);
    }

}
