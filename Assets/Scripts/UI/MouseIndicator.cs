using System;
using UnityEngine;
using UnityEngine.Events;

public class MouseIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    
    [Header("Mouse Click Effect")]
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private float indicatorDuration = 0.5f;
    
    [Header("Listening To")]
    [SerializeField] private InputReader _inputReader;
    
    private AudioSource _audioSource;
    
    private void OnEnable()
    {
        _inputReader.ChoosePositionEvent += CreateIndicator;
    }
    
    private void OnDisable()
    {
        _inputReader.ChoosePositionEvent -= CreateIndicator;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void CreateIndicator(Vector2 position)
    {
        GameObject indicator = Instantiate(indicatorPrefab, position, Quaternion.identity);
        _audioSource.Play();
        
        Destroy(indicator, indicatorDuration);
    }
}