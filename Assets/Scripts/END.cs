using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class END : MonoBehaviour
{
    public Image Background;
    public TextMeshProUGUI text;
    public GameObject TitlesObj;
    public GameObject titlesPrefab;

    private string[] KazakhMaleNames = new string[] {
    "Алихан", "Арлан", "Бакытжан", "Бегалы", "Ержан", "Жанболат", "Кайрат", "Мадияр", "Нурбек", "Олжас",
    "Ракат", "Самат", "Тлеген", "Улан", "Фархад", "Хасан", "Хусайн", "Чингис", "Шерхан", "Айдар",
    "Батыр", "Есенгельды", "Жаксыбек", "Кенжебек", "Мухтар", "Оразбек", "Руслан", "Сардар", "Тимур", "Узакбай",
    "Фарид", "Хамид", "Ханберген", "Чолпан", "Шугыла", "Азамат", "Бакыт", "Есен", "Жасулан", "Курманжан",
    "Марат", "Нурбек", "Отебай", "Рамазан", "Сейтжан", "Талгат", "Уланбек", "Фаррух", "Хасан", "Хусайн",
    "Чингизхан", "Шернияз", "Айзат", "Баглан", "Ержат", "Жандос", "Казбек", "Мустафа", "Орман", "Рустам",
    "Саят", "Турлан", "Умар", "Фархат", "Хасанбек", "Хамза", "Чолпанбай", "Шуайн", "Али", "Бекжан",
    "Данияр", "Ержан", "Жангир", "Кенбик", "Марат", "Нурлан", "Олжасбек", "Ратмир", "Сархан", "Тимурбек",
    "Улан", "Фарид", "Хасан", "Хусайн", "Чингиз", "Шерхан", "Айдаржан", "Батырхан", "Есенгельды", "Жаксылык",
    "Кенжемурат", "Мухтарбек", "Ораз", "Руслан", "Сардарбек", "Тилек", "Узакбай", "Фариддин", "Хамидулла", "Хан"
    };

    private string[] KazakhFemaleNames = new string[] {
    "Айгуль", "Алтынай", "Гулжан", "Динара", "Ерболат", "Зарина", "Каныш", "Кулайша", "Медеу", "Назик",
    "Райхан", "Сания", "Толкын", "Улжан", "Фариза", "Хадиша", "Хурмат", "Чолпан", "Шаймерден", "Ажар",
    "Бекзат", "Еркежан", "Жулдыз", "Кымбат", "Майра", "Нургуль", "Олжана", "Рухия", "Салма", "Тилек",
    "Улангуль", "Фарида", "Ханум", "Хусейна", "Чингиз", "Шолпан", "Айша", "Бекзат", "Ерболат", "Жазира",
    "Кензия", "Мадина", "Назима", "Олжас", "Раиса", "Сейлау", "Тогжан", "Улан", "Фархад", "Хасан",
    "Хурмат", "Чолпан", "Шугыла", "Айгерим", "Бакытгуль", "Ерминде", "Жансулу", "Кызгалым", "Мурагерым",
    "Нуржан", "Олжанай", "Раушань", "Салмагуль", "Тилектилеу", "Улангуль", "Фариза", "Хадижат", "Хурлыг",
    "Чолпан", "Шаймерден", "Айнур", "Бекжан", "Дана", "Еркежан", "Жазира", "Кензия", "Майрагуль",
    "Нуржамал", "Олжас", "Раиса", "Салма", "Тилек", "Улангуль", "Фариза", "Хадиша", "Хурмат", "Чолпан",
    "Шугыла", "Айжан", "Бекзат", "Ерболат", "Жулдыз", "Кымбат", "Майра", "Нургуль", "Олжана", "Рухия"
    };

    private string[] KazakhMaleFamilyNames = new string[] {
    "Алиханов", "Арланбеков", "Бактыгалиев", "Бегалышов", "Ержанов", "Жанболатов", "Кайрбеков", "Мадияров", "Нурбеков", "Олжасов",
    "Ракашев", "Саматов", "Тлегенбаев", "Улангалиев", "Фархадов", "Хасанов", "Хусаинов", "Чингизов", "Шерниязов", "Айдаров",
    "Батыров", "Есенгельдыев", "Жаксыбеков", "Кенжебеков", "Мухтаров", "Оразбеков", "Русланов", "Сардарбеков", "Тимуров", "Узакбаев",
    "Фаридов", "Хамидов", "Ханбергенов", "Чолпанбаев", "Шугылаев", "Азаматов", "Бактыбаев", "Есеналиев", "Жасуланбеков", "Курмангалиев",
    "Маратов", "Нурбекулы", "Отебаев", "Рамазанов", "Сейтжанов", "Талгатов", "Уланбеков", "Фаррухов", "Хасанов", "Хусаинов",
    "Чингизханов", "Шерниязов", "Айзатов", "Багланов", "Ержатов", "Жандосов", "Казбеков", "Мустафинов", "Орманов", "Рустамов",
    "Саятов", "Турланов", "Умаров", "Фархатов", "Хасанбеков", "Хамзаев", "Чолпанбаев", "Шуайнбеков", "Алиев", "Бекжанов",
    "Данияров", "Ержанов", "Жангиров", "Кенбиков", "Маратов", "Нурланов", "Олжасбеков", "Ратмиров", "Сарханов", "Тимурбеков",
    "Улангалиев", "Фаридов", "Хасанов", "Хусаинов", "Чингизов", "Шерниязов", "Айдаров", "Батырбеков", "Есенгельдыев", "Жаксылыков",
    "Кенжемуратов", "Мухтаров", "Оразбеков", "Русланов", "Сардарбеков", "Тилекбаев", "Узакбай", "Фариддин", "Хамидуллин", "Ханбеков"
    };

    private string[] KazakhFemaleFamilyName = new string[] {
    "Алиханова", "Арланбекова", "Бактыгалиева", "Бегалышева", "Ержанова", "Жанболатова", "Кайрбекова", "Мадиярова", "Нурбекова", "Олжасова",
    "Ракашева", "Саматова", "Тлегенбаева", "Улангалиева", "Фархадова", "Хасанова", "Хусаинова", "Чингизова", "Шерниязова", "Айдарова",
    "Батырова", "Есенгельдыева", "Жаксыбекова", "Кенжебекова", "Мухтарова", "Оразбекова", "Русланова", "Сардарбекова", "Тимурова", "Узакбаева",
    "Фаридова", "Хамидова", "Ханбергенова", "Чолпанбаева", "Шугылаева", "Азаматова", "Бактыбаева", "Есеналиева", "Жасуланбекова", "Курмангалиева",
    "Маратова", "Нурбекулы", "Отебаева", "Рамазанова", "Сейтжанова", "Талгатова", "Уланбекова", "Фаррухова", "Хасанова", "Хусаинова",
    "Чингизханова", "Шерниязова", "Айзатова", "Багланова", "Ержатова", "Жандосова", "Казбекова", "Мустафинова", "Орманова", "Рустамова",
    "Саятова", "Турланова", "Умарова", "Фархатова", "Хасанбекова ааа блять", "Хамзаева", "Чолпанбаева", "Шуайнбекова", "Алиева", "Бекжанова",
    "Даниярова", "Ержанова", "Жангирова", "Кенбикова", "Маратова", "Нурланова", "Олжасбекова", "Ратмирова", "Сарханова", "Тимурбекова",
    "Улангалиева", "Фаридова", "Хасанова", "Хусаинова", "Чингизова", "Шерниязова", "Айдарова", "Батырбекова", "Есенгельдыева", "Жаксылыкова",
    "Кенжемуратова", "Мухтарова", "Оразбекова", "Русланова", "Сардарбекова", "Тилекбаева", "Узакбай", "Фариддина", "Хамидуллина", "Ханбекова"
    };

    public string[] GameDevelopmentJobTitles = new string[] {
    "Главный Разработчик", "Ведущий Программист", "Технический Директор", "Руководитель Команды Программистов",
    "Программист Игрового Движка", "Программист Искусственного Интеллекта (AI)", "Программист Геймплея",
    "Программист Сетевой Компоненты", "Программист UI/UX", "Программист Инструментов Разработки",
    "Художник-Концепт", "Главный Художник", "Технический Художник", "3D Моделлер", "Текстурщик",
    "Аниматор", "Художник Окружения (Environment Artist)", "Художник Персонажей", "UI/UX Дизайнер",
    "Дизайнер Уровней (Level Designer)", "Геймдизайнер", "Сценарист", "Главный Сценарист",
    "Композитор", "Звукорежиссер", "Тестировщик Игр (QA Tester)", "Ведущий Тестировщик",
    "Продюсер Игры", "Исполнительный Продюсер", "Менеджер Проекта", "Координатор QA",
    "Специалист по Локализации", "Маркетолог Игр", "PR Менеджер", "Комьюнити Менеджер",
    "Аналитик Данных (Data Analyst)", "Инженер DevOps", "Системный Администратор",
    "Архитектор Игры", "Специалист по Оптимизации", "Программист VR/AR",
    "Разработчик Мобильных Игр", "Разработчик Консольных Игр", "Разработчик PC Игр",
    "Инженер Графики", "Специалист по Motion Capture", "Художник VFX (Visual Effects)",
    "Дизайнер Боевой Системы", "Дизайнер Экономики Игры", "Дизайнер Прогрессии",
    "Дизайнер Звукового Оформления", "Инженер Интеграции", "Специалист по Автоматизации Тестирования",
    "Разработчик Серверной Части (Backend Developer)", "Администратор Базы Данных",
    "Ведущий Дизайнер Уровней", "Главный Геймдизайнер", "Руководитель Художественного Отдела", "Дизайнер Интерфейса",
    "Технический Редактор", "Инженер Построения (Build Engineer)", "Специалист по Цифровой Распространению",
    "Разработчик Инструментов для Художников", "Программист Физики", "Дизайнер Мультиплеера",
    "Специалист по Машинному Обучению в Игры", "Инженер Построения Сцены (Scene Builder)",
    "Ведущий Программист AI", "Главный Технический Художник", "Руководитель Команды QA",
    "Дизайнер Награждений и Достижений", "Специалист по Аналитике Монетизации", "Разработчик Инструментов для Тестирования"
    };

    void Start()
    {

        StartCoroutine(Titles());

    }

    IEnumerator Titles()
    {
        Background.DOFade(1, 3f);
        yield return new WaitForSeconds(3f);
        Main.Instance.AllSpace.SetActive(false);

        yield return new WaitForSeconds(2f);
        Player.Instance.PlayerGameObject.transform.SetParent(Main.Instance.MainCanvas.transform);
        Player.Instance.PlayerGameObject.transform.localPosition = new Vector3(0, -150);
        Player.Instance.PlayerGameObject.SetActive(true);

        

        string temp = "Шароку ушла";
        for (int i = 0; i < temp.Length; i++)
        {
            text.text += temp[i];
            if (temp[i] != ' ')
                SoundManagerUi.Instance.PlaySound("typing");
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(3f);

        text.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        text.text = "";
        text.DOFade(1, 0.1f);

        temp = "Спасибо за игру <3";
        for (int i = 0; i < temp.Length; i++)
        {
            text.text += temp[i];
            if (temp[i] != ' ')
                SoundManagerUi.Instance.PlaySound("typing");
            yield return new WaitForSeconds(0.21f);
        }

        yield return new WaitForSeconds(3f);

        text.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        text.text = "";
        text.DOFade(1, 0.1f);

        temp = "С днем рождения, Крушитель!!";
        for (int i = 0; i < temp.Length; i++)
        {
            text.text += temp[i];
            if (temp[i] != ' ')
                SoundManagerUi.Instance.PlaySound("typing");
            yield return new WaitForSeconds(0.11f);
        }

        yield return new WaitForSeconds(2f);

        text.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        text.text = "";
        text.DOFade(1, 0.1f);

        temp = "Сорри, что не получилось поздравить вовремя :(";
        for (int i = 0; i < temp.Length; i++)
        {
            text.text += temp[i];
            if (temp[i] != ' ')
                SoundManagerUi.Instance.PlaySound("typing");
            yield return new WaitForSeconds(0.11f);
        }

        yield return new WaitForSeconds(1f);

        text.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        text.text = "";
        text.DOFade(1, 0.1f);

        temp = "Над игрой работали:";
        for (int i = 0; i < temp.Length; i++)
        {
            text.text += temp[i];
            if (temp[i] != ' ')
                SoundManagerUi.Instance.PlaySound("typing");
            yield return new WaitForSeconds(0.09f);
        }

        yield return new WaitForSeconds(1f);

        text.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        text.text = "";
        text.DOFade(1, 0.1f);

        temp = "Программист: <color=red>Yokiari</color> \n Спрайты, анимации, консультации: <color=green>Armatura</color> \n Музыкальные консультации: <color=yellow>Astramir</color>";
        for (int i = 0; i < temp.Length; i++)
        {
            text.text += temp[i];
            if (temp[i] != ' ')
                SoundManagerUi.Instance.PlaySound("typing");
            yield return new WaitForSeconds(0.09f);
        }

        yield return new WaitForSeconds(1f);

        text.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        text.text = "";
        text.DOFade(1, 0.1f);

        temp = "Надеемся вы прониклись <color=yellow>Решимостью</color>, пока играли в эту мини игру.";
        for (int i = 0; i < temp.Length; i++)
        {
            text.text += temp[i];
            if (temp[i] != ' ')
                SoundManagerUi.Instance.PlaySound("typing");
            yield return new WaitForSeconds(0.09f);
        }

        yield return new WaitForSeconds(1f);

        text.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        text.text = "";
        text.DOFade(1, 0.1f);

        temp = "Увидимся на других совместных проектах :D";
        for (int i = 0; i < temp.Length; i++)
        {
            text.text += temp[i];

            if (temp[i] != ' ')
                SoundManagerUi.Instance.PlaySound("typing");

            yield return new WaitForSeconds(0.09f);
        }

        yield return new WaitForSeconds(1f);

        text.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        text.text = "";
        text.DOFade(1, 0.1f);

        StartTitle("Над игрой так-же работали: ");
        yield return new WaitForSeconds(2.2f);

        while (true)
        {
            string text = "";
            if (UnityEngine.Random.Range(0, 100) > 50)
            {
                text = $"{KazakhMaleNames[Random.Range(0, KazakhMaleNames.Length - 1)]}"
                + $" {KazakhMaleFamilyNames[Random.Range(0, KazakhMaleFamilyNames.Length - 1)]}"
                + $" - <size=28>{GameDevelopmentJobTitles[Random.Range(0, GameDevelopmentJobTitles.Length - 1)]}</size>";
            }
            else
            {
                text = $" {KazakhFemaleNames[Random.Range(0, KazakhFemaleNames.Length - 1)]}"
                + $" {KazakhFemaleFamilyName[Random.Range(0, KazakhFemaleFamilyName.Length - 1)]}"
                + $" - <size=28>{GameDevelopmentJobTitles[Random.Range(0, GameDevelopmentJobTitles.Length - 1)]}</size>";
            }
            StartTitle(text);
            yield return new WaitForSeconds(1.5f);
        }

    }

    void StartTitle(string text)
    {
        StartCoroutine(TitlesAnim(text));
    }
    IEnumerator TitlesAnim(string text)
    {
        var temp = Instantiate(titlesPrefab, TitlesObj.transform);
        temp.GetComponent<TextMeshProUGUI>().text = text;
        temp.transform.localPosition = new Vector3(0, -605.81f);
        temp.GetComponent<TextMeshProUGUI>().DOFade(1, 0.1f);
        temp.transform.DOLocalMove(new Vector3(71, 607), 20).SetEase(Ease.Linear);
        yield return new WaitForSeconds(20f);
        Destroy(temp);
    }
}
