using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScoreLabel; //ссылка на текст
    [SerializeField] private int _scorePerSquare; //количество очков за куб
    [SerializeField] private AudioSource _bestScoreSound;
    [SerializeField] private float _scaleDuration; //время изменения скейла
    [SerializeField] private float _scaleFactor; //коэфицент и

    private const string BEST_SCORE = "BestScore"; //константа для сохранения и загрузки данных

    private int _bestScore; //лучший результат очко

    private int _currentScore; //общее количество очков

    public void AddScore()
    {
        _currentScore += _scorePerSquare;

        _currentScoreLabel.text = _currentScore.ToString();
        _currentScoreLabel.transform
            .DOPunchScale(Vector3.one * _scaleFactor, _scaleDuration, 0)
            .OnComplete(() => _currentScoreLabel.transform.DOScale(Vector3.one, 0));
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    public int GetBestScore()
    {
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            PlayerPrefs.SetInt(BEST_SCORE, _bestScore); //записываем значение по ключу
            PlayerPrefs.Save(); //сохраняем данны
            _bestScoreSound.Play();
        }

        return _bestScore;
    }

    private void Awake()
    {
        _bestScore = PlayerPrefs.GetInt(BEST_SCORE);
    }
}