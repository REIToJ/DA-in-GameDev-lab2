# DA-in-GameDev-lab2
 
# РАЗРАБОТКА ИГРОВЫХ СЕРВИСОВ
Отчет по лабораторной работе #2 выполнил(а):
- Рест Владислав Сергеевич

Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | # | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.
- ✨Magic ✨

## Цель работы
Создание интерактивного приложения и изучение принципов интеграции в него игровых сервисов.

## Задание 1
### По теме видео практических работ 1-5 повторить реализацию игры на Unity. Привести описание выполненных действий.

Ход работы:
Сначала скачиваем модели драконов из Unity Assets Store, импортируем пакет и добавляем дракона на сцену. Далее создаем префабы яйца и щита.  

![Screen1](https://user-images.githubusercontent.com/57962348/194388757-89a74ead-cb46-4d19-b9e5-2ea8253d0958.png)


Настраиваем камеру и добавляем скрипт полета дракона, предварительно изменив анимацию модели с той, что изначально стояла при импорте пакета на ту одну, что нужна нам.
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    public GameObject dragonEggPrefab;
    public float speed = 1;
    public float timeBetweenEggDrops = 1f;
    public float leftRightDistance = 10f;
    public float chanceDirection = 0.1f;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftRightDistance){
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftRightDistance){
            speed = -Mathf.Abs(speed);
        }
    }

    private void FixedUpdate() {
        if (Random.value < chanceDirection){
            speed *= -1;
        }
    }
}
```
![Screen2](https://user-images.githubusercontent.com/57962348/194388795-93b87f5b-ebf9-44c3-8440-31b0eea3647b.png)


Добавляем плейн и настраиваем текстуры земли и яйца. Берем их из скачанного пакета.

![Screen3](https://user-images.githubusercontent.com/57962348/194388813-86233ddf-373b-414b-b7de-a5f46a7d098a.png)


Прописываем скрипт для генерации и уничтожения яйца при падении, а так же для генерации системы частиц (текстуру для которых берем из другого скачанного из asset stor'а пакета). Настраиваем систему частиц.
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    public GameObject dragonEggPrefab;
    public float speed = 1;
    public float timeBetweenEggDrops = 1f;
    public float leftRightDistance = 10f;
    public float chanceDirection = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropEgg", 2f);
    }

    void DropEgg(){
        Vector3 myVector = new Vector3(0.0f, 5.0f, 0.0f );
        GameObject egg = Instantiate<GameObject>(dragonEggPrefab);
        egg.transform.position = transform.position + myVector;
        Invoke("DropEgg", timeBetweenEggDrops);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftRightDistance) {
            speed = Mathf.Abs(speed);

        } else if (pos.x > leftRightDistance) {
            speed = -Mathf.Abs(speed);
        }
    }
    private void FixedUpdate() {
        if(Random.value < chanceDirection) {
            speed *= -1;
        }    
    }
}
```
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEgg : MonoBehaviour
{
    public static float bottomY = -30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;

        Renderer rend;
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY){
            Destroy(this.gameObject);
        }
    }
}
```
![Unity_Mlcti6QL0y](https://user-images.githubusercontent.com/57962348/194388847-ce6a8e20-8cb2-469d-9bde-2e33405ac305.gif)


Пишем код для генерации энергетического щита.

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPicker : MonoBehaviour
{
    public GameObject enertyShieldPrefab;
    public int NumEnergyShield = 3;
    public float energyShieldBottomY = -6f;
    public float energyShieldRadius = 1.5f;
    // Start is called before the first frame update
    void Start()
    {   
        for (int i = 1; i <= NumEnergyShield; i++) {
            GameObject tShieldGo = Instantiate<GameObject>(enertyShieldPrefab);
            tShieldGo.transform.position = new Vector3 (0, energyShieldBottomY,0);
            tShieldGo.transform.localScale = new Vector3(1*i, 1*i, 1*i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
```


## Задание 2
### В проект, выполненный в предыдущем задании, добавить систему проверки того, что SDK подключен (доступен в режиме онлайн и отвечает на запросы);



## Задание 3
### 1 Произвести сравнительный анализ игровых сервисов Яндекс Игры и VK Game;  
### 2 Дать сравнительную характеристику сервисов, описать функционал;  
### 3 Описать их методы интеграции с Unity;  
### 4 Произвести сравнение, сделать выводы;  
### 5 Подготовить реферат по результатам выполнения пунктов 1-4 .


## Выводы

Познакомился с YandexGames, узнал чем они лучше VKGames. Создал и сбилдил первое приложение на юнити.

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |

## Powered by
