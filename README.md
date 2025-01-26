# Handbook

**Тестовое задание по разработке Системы справочной информации.**

Требования:

- Программа должна быть реализована с использованием технологий WPF (XAML)
- При запуске программы должен быть проинициализирован выпадающий список, с возможными типами устройств для выбора пользователем (https://2392bb8b-2589-4515-a05d-bff3882c6c75.mock.pstmn.io/devices)
- При выборе типа устройства из этого выпадающего списка необходимо нажать кнопку «Загрузить» после чего программа должна отправить запрос по одному из
следующих адресов и вывести информацию в табличной части программы (Идентификатор, Обозначение, Наименование).
  - https://2392bb8b-2589-4515-a05d-bff3882c6c75.mock.pstmn.io/pumps
  - https://2392bb8b-2589-4515-a05d-bff3882c6c75.mock.pstmn.io/cylinders
  - https://2392bb8b-2589-4515-a05d-bff3882c6c75.mock.pstmn.io/valves
- Остальные свойства оборудования, которые специфичны для выбранного типа объекта должны отображаться в отдельной панели свойств при
выделении строки в табличной части программы.
- В программе должен быть реализован интерфейс на английском и русском языке с «горячим» переключением языка с помощью кнопки в текстовой
части которой, отображается текущий выбранный язык.

Реализация:
- Программа релизована в виде трёх проектов:
  - Handbook (wpf приложение)
  - Handbook.Services (общие сервисы, в данным случае загрузка данных от внешних API)
  - Handbook.Services.Tests (unit тесты для сервиса загрузки)
- Точкой входа служит класс Program проекта Handbook. Здесь производится регистрация зависимостей через ServiceCollection и первичный запуск класса App.
- Класс App является точкой хранения ServiceProvider, и запуска показа основного окна MainWindow.
- MainWindow содержит все основные элементы, именно с ним взаимодействует пользователь.
- Для хранения текстов для интерфейса с разной локализацией используется ресурсный файл Strings.resx
  - Логику работы с ним обеспечивает класс LocalizationManager.
- Для получения данных от внешних API реализован класс ApiService.
  - Модели для сервиса и представления разделены. Для их сопоставления используется AutoMapper.
- Для unit-тестов используются библиотеки xUnit и MockHttp.

Что можно добавить:
- Заменить выводы в консоль при помощи Console.WriteLine на использование библиотеки NLog.
- Добавить больше тестов (в данный реализации были написаны только тесты к методу LoadCylinders() в качестве примера).
- Уменьшить количество связанности объектов в MainWindow, разбив элементы на подобъекты, реализовав подходы EventAggregator или Mediator.
