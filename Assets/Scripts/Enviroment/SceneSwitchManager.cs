using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneSwitchManager : MonoBehaviour
{
    [field: SerializeField] public static SceneSwitchManager SwitchInstance { get; private set; } = null;

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup _fadeCanvasGroup;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private AnimationCurve _fadeCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("UI Elements")]
    [SerializeField] private Text _sceneNameText;
    [SerializeField] private Text _dayText;
    [SerializeField] private float _textDisplayTime = 2f;

    [Header("References")]
    [SerializeField] private EventCalendar _eventCalendar;
    [SerializeField] private DayProducer _dayProducer;

    private bool _isTransitioning = false;

    private void Awake()
    {
        if (SceneSwitchManager.SwitchInstance == null)
        {
            SceneSwitchManager.SwitchInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Инициализируем fade canvas если не установлен
        if (_fadeCanvasGroup == null)
        {
            CreateFadeCanvas();
        }
    }

    // Основной метод перехода
    public void SwitchToScene(string sceneName, int day)
    {
        if (_isTransitioning) return;

        StartCoroutine(TransitionRoutine(sceneName, day));
    }

    // Переход к следующему дню в той же сцене
    public void NextDayInCurrentScene()
    {
        int nextDay = _eventCalendar.CurrentDay + 1;
        SwitchToScene(_eventCalendar.CurrentScene, nextDay);
    }

    // Переход в следующую сцену того же дня
    public void NextSceneForCurrentDay()
    {
        var scenes = _eventCalendar.GetScenesForDay(_eventCalendar.CurrentDay);
        int currentIndex = scenes.IndexOf(_eventCalendar.CurrentScene);

        if (currentIndex >= 0 && currentIndex < scenes.Count - 1)
        {
            SwitchToScene(scenes[currentIndex + 1], _eventCalendar.CurrentDay);
        }
    }

    private IEnumerator TransitionRoutine(string sceneName, int day)
    {
        _isTransitioning = true;

        // 1. Останавливаем текущие события
        if (_dayProducer != null)
        {
            _dayProducer.StopScheduler();
        }

        // 2. Затемнение экрана
        yield return FadeOut();

        // 3. Обновляем календарь
        _eventCalendar.SetCurrentDayAndScene(day, sceneName);

        // 4. Показываем информацию о новой сцене
        yield return ShowSceneInfo(sceneName, day);

        // 5. Загружаем сцену
        yield return SceneManager.LoadSceneAsync(sceneName);

        // 7. Загружаем события для нового дня
        LoadDayEvents();


        // 9. Запускаем новые события
        if (_dayProducer != null)
        {
            _dayProducer.StartScheduler();
        }

        _isTransitioning = false;
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = _fadeCurve.Evaluate(elapsed / _fadeDuration);
            _fadeCanvasGroup.alpha = t;
            yield return null;
        }

        _fadeCanvasGroup.alpha = 1f;
    }
    
    private IEnumerator ShowSceneInfo(string sceneName, int day)
    {
        if (_sceneNameText != null)
        {
            _sceneNameText.text = $"Сцена: {sceneName}";
            _sceneNameText.gameObject.SetActive(true);
        }

        if (_dayText != null)
        {
            _dayText.text = $"День {day}";
            _dayText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(_textDisplayTime);

        if (_sceneNameText != null) _sceneNameText.gameObject.SetActive(false);
        if (_dayText != null) _dayText.gameObject.SetActive(false);
    }

    private void LoadDayEvents()
    {
        if (_dayProducer != null && _eventCalendar != null)
        {
            var events = _eventCalendar.GetCurrentEvents();
            _dayProducer.DayEvents = events;
        }
    }

    private void CreateFadeCanvas()
    {
        GameObject fadeObject = new GameObject("FadeCanvas");
        _fadeCanvasGroup = fadeObject.AddComponent<CanvasGroup>();
        _fadeCanvasGroup.alpha = 0f;

        Canvas canvas = fadeObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999;

        fadeObject.AddComponent<GraphicRaycaster>();

        // Создаем затемняющий фон
        GameObject imageObject = new GameObject("FadeImage");
        imageObject.transform.SetParent(fadeObject.transform);

        Image image = imageObject.AddComponent<Image>();
        image.color = Color.black;

        RectTransform rect = imageObject.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    // Быстрый переход без анимации (для тестов)
    public void SwitchImmediate(string sceneName, int day)
    {
        _eventCalendar.SetCurrentDayAndScene(day, sceneName);
        SceneManager.LoadScene(sceneName);

        // В новой сцене нужно будет вызвать LoadDayEvents()
    }

    // Автоматический переход при завершении дня
    public void OnDayCompleted()
    {
        // Можно вызвать автоматический переход в следующую сцену
        // или увеличить день
        NextDayInCurrentScene();
    }
}
