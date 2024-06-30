using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField] Button startButton;

    [SerializeField] Button next;

    [SerializeField] GameObject intro;

    [SerializeField] GameObject levelButtons;

    [SerializeField] GameObject TimeChoose;

    [SerializeField] GameObject questionPart;

    [SerializeField] GameObject Menu;

    [SerializeField] Button resume;
    [SerializeField] Button mainMenu;
    [SerializeField] Button quit;
 
    [SerializeField] Button settings;

    [SerializeField] Button easy;
    [SerializeField] Button medium;
    [SerializeField] Button hard;

    [SerializeField] Button easyTimeButton;
    [SerializeField] Button mediumTimeButton;
    [SerializeField] Button hardTimeButton;

    [SerializeField] TMP_Text easyTimeText;
    [SerializeField] TMP_Text mediumTimeText;
    [SerializeField] TMP_Text hardTimeText;

    [SerializeField] TMP_Text question;
    [SerializeField] TMP_Text answer;

    float easyTimeNumber, mediumTimeNumber, hardTimeNumber, chosenTime;

    [SerializeField] CanvasGroup blackPanel;

    bool flag_buttons = false;
    bool flag_time = false;
    bool flag_next = false;

    bool flag_time_easy, flag_time_medium, flag_time_hard;

    int a, b, result;

    int min_a, max_a, min_b, max_b;

    enum Level
    {
        NULL,
        EASY,
        MEDIUM,
        HARD
    }

    Level level;

    // Start is called before the first frame update
    void Start()
    {
        blackPanel.alpha = 0;
        Menu.SetActive(false);
        TimeChoose.SetActive(false);
        questionPart.SetActive(false);
        levelButtons.SetActive(false);
        level = Level.NULL;
        flag_buttons = false;
        flag_time = false;
        flag_time_easy = false;
        flag_time_hard = false;
        flag_time_medium = false;
        flag_next = false;
        startButton.onClick.AddListener(startButtonTapped);

        settings.onClick.AddListener(settingsTapped);

        resume.onClick.AddListener(resumeTapped);
        mainMenu.onClick.AddListener(mainMenuTapped);
        quit.onClick.AddListener(quitTapped);

        next.onClick.AddListener(nextQuestion);

        easy.onClick.AddListener(easyButtonTapped);
        medium.onClick.AddListener(mediumButtonTapped);
        hard.onClick.AddListener(HardButtonTapped);

        easyTimeButton.onClick.AddListener(easyTimeButtonTapped);
        mediumTimeButton.onClick.AddListener(mediumTimeButtonTapped);
        hardTimeButton.onClick.AddListener(hardTimeButtonTapped);
    }

    // Update is called once per frame
    void Update()
    {
        blackOperationLevel(levelButtons,TimeChoose);

      
        if (level == Level.EASY)
        {
            min_a = 2;
            min_b = 2;

            max_a = 10;
            max_b = 10;

            easyTimeNumber = 0.5f;
            mediumTimeNumber = 1;
            hardTimeNumber = 1.5f;

            easyTimeText.text = easyTimeNumber.ToString();
            mediumTimeText.text = mediumTimeNumber.ToString();
            hardTimeText.text = hardTimeNumber.ToString();

            blackOperationTime(TimeChoose,questionPart);

        } else if (level == Level.MEDIUM)
        {
            min_a = 10;
            min_b = 2;

            max_a = 100;
            max_b = 10;

            easyTimeNumber = 1f;
            mediumTimeNumber = 2f;
            hardTimeNumber = 3f;

            easyTimeText.text = easyTimeNumber.ToString();
            mediumTimeText.text = mediumTimeNumber.ToString();
            hardTimeText.text = hardTimeNumber.ToString();

            blackOperationTime(TimeChoose, questionPart);

        } else if (level == Level.HARD)
        {
            min_a = 100;
            min_b = 2;

            max_a = 1000;
            max_b = 10;

            easyTimeNumber = 1f;
            mediumTimeNumber = 3f;
            hardTimeNumber = 5f;

            easyTimeText.text = easyTimeNumber.ToString();
            mediumTimeText.text = mediumTimeNumber.ToString();
            hardTimeText.text = hardTimeNumber.ToString();

            blackOperationTime(TimeChoose, questionPart);
        }
    }

    void startButtonTapped()
    {
        Thread.Sleep(100);
        intro.SetActive(false);
        levelButtons.SetActive(true);
    }

    void easyButtonTapped()
    {
        level = Level.EASY;
        flag_buttons = true;
    }

    void mediumButtonTapped()
    {
        level = Level.MEDIUM;
        flag_buttons = true;
    }

    void HardButtonTapped()
    {
        level = Level.HARD;
        flag_buttons = true;
    }

    void easyTimeButtonTapped()
    {
        chosenTime = easyTimeNumber;
        flag_time = true;
        flag_time_easy = true;
        answer.text = " ";
        a = Random.Range(min_a, max_a);
        b = Random.Range(min_b, max_b);
        result = a * b;
        question.text = string.Format("{0} x {1}", a, b);
        Invoke("Waiting", chosenTime);
    }

    void mediumTimeButtonTapped()
    {
        chosenTime = mediumTimeNumber;
        flag_time = true;
        flag_time_medium= true;
        answer.text = " ";
        a = Random.Range(min_a, max_a);
        b = Random.Range(min_b, max_b);
        result = a * b;
        question.text = string.Format("{0} x {1}", a, b);
        Invoke("Waiting", chosenTime);
    }

    void hardTimeButtonTapped()
    {
        chosenTime = hardTimeNumber;
        flag_time = true;
        flag_time_hard = true;
        answer.text = " ";
        a = Random.Range(min_a, max_a);
        b = Random.Range(min_b, max_b);
        result = a * b;
        question.text = string.Format("{0} x {1}", a, b);
        Invoke("Waiting", chosenTime);
    }

    void nextQuestion()
    {
        answer.text = " ";
        flag_next = true;
        a = Random.Range(min_a, max_a);
        b = Random.Range(min_b, max_b);
        result = a * b;
        question.text = string.Format("{0} x {1}", a, b);
        Invoke("Waiting", chosenTime);
    }

    void settingsTapped()
    {
        Menu.SetActive(true);
    }

    void resumeTapped()
    {
        Menu.SetActive(false);
    }

    void mainMenuTapped()
    {
        Menu.SetActive(false);
        questionPart.SetActive(false);
        TimeChoose.SetActive(false);
        levelButtons.SetActive(true);
    }

    void quitTapped()
    {
        Application.Quit();
    }

    void Waiting()
    {
        answer.text = result.ToString();
    }


    void blackOperationLevel(GameObject gameObject1, GameObject gameObject2)
    {
        if (flag_buttons && !flag_time)
        {
            blackPanel.alpha += Time.deltaTime * 5;
            if (blackPanel.alpha >= 0.95f)
            {
                blackPanel.alpha = 1.0f;
                flag_buttons = false;
                gameObject1.SetActive(false);
                gameObject2.SetActive(true);
            }
        }
        else if (!flag_buttons && !flag_time)
        {
            blackPanel.alpha -= Time.deltaTime * 5;
            if (blackPanel.alpha <= 0.05f)
            {
                blackPanel.alpha = 0.0f;
                flag_buttons = false;
            }
        }
    }

    void blackOperationTime(GameObject gameObject1, GameObject gameObject2)
    {
        if (flag_time && !flag_buttons)
        {
            blackPanel.alpha += Time.deltaTime * 5;
            if (blackPanel.alpha >= 0.95f)
            {
                blackPanel.alpha = 1.0f;
                flag_time = false;
                gameObject1.SetActive(false);
                gameObject2.SetActive(true);
            }
        }
        else if (!flag_time && !flag_buttons)
        {
            blackPanel.alpha -= Time.deltaTime * 5;
            if (blackPanel.alpha <= 0.05f)
            {
                blackPanel.alpha = 0.0f;
                flag_time = false;
            }
        }
    }



    //void blackPanelOperation()
    //{
    //    while (blackPanel.alpha <= 1.0f && flag)
    //    {
    //        blackPanel.alpha += 0.1f;
    //        if (blackPanel.alpha >= 0.95f) // Using a threshold to check for close to 1
    //        {
    //            blackPanel.alpha = 1.0f;
    //            flag = false;// Set alpha to exactly 1
    //        }
    //    }

    //    flag = true;

    //    while (blackPanel.alpha >= 0.01f && flag) // Using a threshold to check for close to 0
    //    {
    //        blackPanel.alpha -= 0.1f;
    //        if (blackPanel.alpha <= 0.05f) // Using a threshold to check for close to 0
    //        {
    //            blackPanel.alpha = 0.0f;
    //            flag = false;// Set alpha to exactly 0
    //        }
    //    }
    //}
}
