using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SentenceData
{
    public List<string> easySentences;
    public List<string> mediumSentences;
    public List<string> hardSentences;
}

public class TextData : MonoBehaviour
{
    public static TextData Instance { get; private set; }
    
    private SentenceData sentenceData;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSentences();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void LoadSentences()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("sentences");
        if (jsonFile != null)
        {
            sentenceData = JsonUtility.FromJson<SentenceData>(jsonFile.text);
        }
        else
        {
            sentenceData = new SentenceData
            {
                easySentences = new List<string>
                {
                    "Кот спит на окне.",
                    "Я люблю читать книги.",
                    "Солнце светит ярко.",
                    "Вода в реке холодная.",
                    "Дети играют в парке.",
                    "Небо сегодня синее.",
                    "Собака бежит быстро.",
                    "Цветы растут в саду.",
                    "Птицы поют утром.",
                    "Луна светит ночью.",
                    "Дождь идёт весь день.",
                    "Ветер дует сильно.",
                    "Снег лежит на земле.",
                    "Рыба плавает в море.",
                    "Звёзды видны вечером.",
                    "Трава зелёная летом.",
                    "Машина едет по дороге.",
                    "Дом стоит на улице.",
                    "Лес тёмный и густой.",
                    "Река течёт в горах."
                },
                
                mediumSentences = new List<string>
                {
                    "Программирование требует терпения и практики.",
                    "Весной природа просыпается и расцветает.",
                    "Технологии развиваются с каждым днем.",
                    "Музыка способна изменить настроение человека.",
                    "Путешествия расширяют кругозор и мировоззрение.",
                    "Образование открывает множество возможностей.",
                    "Искусство отражает внутренний мир художника.",
                    "Спорт укрепляет тело и дух человека.",
                    "Чтение развивает воображение и мышление.",
                    "Дружба основана на взаимном доверии.",
                    "Творчество помогает выразить себя.",
                    "Наука раскрывает тайны окружающего мира.",
                    "История учит нас на примерах прошлого.",
                    "Языки соединяют людей разных культур.",
                    "Кино объединяет искусство и технологии.",
                    "Литература передаёт опыт поколений.",
                    "Философия ищет ответы на главные вопросы.",
                    "Экология важна для будущего планеты.",
                    "Медицина спасает миллионы жизней ежегодно.",
                    "Архитектура сочетает красоту и функциональность."
                },
                
                hardSentences = new List<string>
                {
                    "Экзистенциальная философия исследует фундаментальные вопросы бытия.",
                    "Квантовая механика противоречит классическим представлениям физики.",
                    "Биотехнологические инновации революционизируют медицинскую индустрию.",
                    "Архитектурные достопримечательности отражают культурное наследие цивилизации.",
                    "Междисциплинарный подход способствует комплексному решению проблем.",
                    "Феноменологический анализ раскрывает структуру сознательного опыта.",
                    "Нейропластичность демонстрирует адаптивные возможности человеческого мозга.",
                    "Постмодернистская критика деконструирует традиционные нарративы.",
                    "Антропологические исследования выявляют универсальные культурные паттерны.",
                    "Криптографические алгоритмы обеспечивают безопасность цифровых коммуникаций.",
                    "Геополитическая конфигурация определяет международные экономические отношения.",
                    "Эпистемологические дебаты касаются природы и границ человеческого познания.",
                    "Синергетический эффект возникает при взаимодействии различных компонентов системы.",
                    "Палеонтологические находки предоставляют свидетельства эволюционных процессов.",
                    "Социологические исследования анализируют динамику общественных трансформаций.",
                    "Лингвистическая относительность влияет на когнитивные процессы восприятия.",
                    "Термодинамические принципы регулируют энергетические преобразования.",
                    "Макроэкономические индикаторы отражают состояние национальной экономики.",
                    "Психоаналитическая теория объясняет бессознательные мотивации поведения.",
                    "Конституционные гарантии защищают фундаментальные права граждан."
                }
            };
        };
    }
    
    public List<string> GetRandomSentences(GameManager.Difficulty difficulty, int count = 5)
    {
        List<string> sourceList;
        
        switch (difficulty)
        {
            case GameManager.Difficulty.Easy:
                sourceList = new List<string>(sentenceData.easySentences);
                break;
            case GameManager.Difficulty.Medium:
                sourceList = new List<string>(sentenceData.mediumSentences);
                break;
            case GameManager.Difficulty.Hard:
                sourceList = new List<string>(sentenceData.hardSentences);
                break;
            default:
                sourceList = new List<string>(sentenceData.easySentences);
                break;
        }
        
        List<string> result = new List<string>();
        for (int i = 0; i < count && sourceList.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, sourceList.Count);
            result.Add(sourceList[randomIndex]);
            sourceList.RemoveAt(randomIndex);
        }
        
        return result;
    }
}
